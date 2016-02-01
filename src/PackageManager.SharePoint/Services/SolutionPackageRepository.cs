// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SolutionPackageRepository.cs" company="SANDs">
//   Copyright © 2016 SANDs. All rights reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace PackageManager.SharePoint.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.SharePoint.Administration;

    using NuGet;

    using PackageManager.SharePoint.Extensions;
    using PackageManager.SharePoint.Services.Interfaces;

    using Constants = PackageManager.SharePoint.Constants;

    /// <summary>
    ///     The package repository.
    /// </summary>
    public class SolutionPackageRepository : ISolutionPackageRepository
    {
        /// <summary>
        ///     The package source repository.
        /// </summary>
        private readonly ISolutionPackageSourceRepository solutionPackageSourceRepository = new SolutionPackageSourceRepository();

        /// <summary>
        ///     Enumerate all available solution packages.
        /// </summary>
        /// <returns>
        ///     The <see cref="IEnumerable{SolutionPackage}" />.
        /// </returns>
        public IEnumerable<SolutionPackage> All()
        {
            var solutions = SPFarm.Local.Solutions.ToList();
            var repository = new AggregateRepository(this.solutionPackageSourceRepository.All().Where(source => source.IsEnabled).Select(source => PackageRepositoryFactory.Default.CreateRepository(source.Source)));
            var solutionPackageGroups = repository.QueryForAbsoluteLatestVersionOfSolutionPackages().ToList().GroupBy(package => package.Id);
            foreach (var solutionPackageGroup in solutionPackageGroups)
            {
                IPackage solutionPackage = solutionPackageGroup.FindByVersion(new VersionSpec(solutionPackageGroup.Max(p => p.Version))).First();
                var solution = solutions.Find(s => s.Name.ToLower().Equals(solutionPackage.Id.ToLower()));
                if (solution != null)
                {
                    yield return new SolutionPackage(solutionPackage, solution.GetVersion());
                }
                else
                {
                    yield return new SolutionPackage(solutionPackage);
                }
            }
        }

        /// <summary>
        ///     Enumerates the installed <see cref="SolutionPackage" />
        /// </summary>
        /// <returns>
        ///     An enumeration of <see cref="SolutionPackage" />.
        /// </returns>
        public IEnumerable<SolutionPackage> Installed()
        {
            var solutions = SPFarm.Local.Solutions.ToList();
            var repository = new AggregateRepository(this.solutionPackageSourceRepository.All().Where(source => source.IsEnabled).Select(source => PackageRepositoryFactory.Default.CreateRepository(source.Source)));
            var solutionPackages = repository.QueryForAbsoluteLatestVersionOfSolutionPackages();
            if (solutionPackages != null)
            {
                foreach (var package in solutionPackages)
                {
                    var solution = solutions.Find(s => s.Name.ToLower().Equals(package.Id.ToLower()));
                    if (solution != null)
                    {
                        SemanticVersion installedVersion;
                        if (!solution.Properties.Contains(Constants.VersionPropertyName) || !SemanticVersion.TryParse(solution.Properties[Constants.VersionPropertyName].ToString(), out installedVersion))
                        {
                            installedVersion = new SemanticVersion(Constants.ZeroVersion);
                        }

                        yield return new SolutionPackage(package, installedVersion);
                    }
                }
            }
        }

    }
}