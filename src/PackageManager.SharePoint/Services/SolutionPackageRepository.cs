// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SolutionPackageRepository.cs" company="SANDs">
//   Copyright © 2016 SANDs. All rights reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace PackageManager.SharePoint.Services
{
    using System;
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
        ///     The <see cref="IEnumerable" />.
        /// </returns>
        public IEnumerable<SolutionPackage> All()
        {
            var solutions = SPFarm.Local.Solutions.ToList();
            foreach (var packageSource in this.solutionPackageSourceRepository.All())
            {
                if (packageSource.IsEnabled)
                {
                    var solutionPackages = InitializeSolutionPackagesQuery(packageSource);
                    if (solutionPackages != null)
                    {
                        foreach (var package in solutionPackages)
                        {
                            var solution = solutions.Find(s => s.Name.Equals(package.Id, StringComparison.InvariantCultureIgnoreCase));
                            if (solution != null)
                            {
                                yield return new SolutionPackage(package, solution.GetVersion());
                            }
                            else
                            {
                                yield return new SolutionPackage(package);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Enumerates the installed <see cref="SolutionPackage"/>
        /// </summary>
        /// <returns>
        /// An enumeration of <see cref="SolutionPackage"/>.
        /// </returns>
        public IEnumerable<SolutionPackage> Installed()
        {
            var solutions = SPFarm.Local.Solutions.ToList();
            foreach (var packageSource in this.solutionPackageSourceRepository.All())
            {
                if (packageSource.IsEnabled)
                {
                    var solutionPackages = InitializeSolutionPackagesQuery(packageSource);
                    if (solutionPackages != null)
                    {
                        foreach (var package in solutionPackages)
                        {
                            var solution = solutions.Find(s => s.Name.Equals(package.Id, StringComparison.InvariantCultureIgnoreCase));
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

        private static IQueryable<IPackage> InitializeSolutionPackagesQuery(SolutionPackageSource packageSource)
        {
            var packageRepository = PackageRepositoryFactory.Default.CreateRepository(packageSource.Source);
            IQueryable<IPackage> solutionPackages = null;
            try
            {
                solutionPackages = packageRepository.GetPackages().Where(package => package.Id.EndsWith(Constants.SolutionPackagePostFix, StringComparison.InvariantCultureIgnoreCase) && package.IsLatestVersion);
            }
            catch
            {
            }
            return solutionPackages;
        }
    }
}