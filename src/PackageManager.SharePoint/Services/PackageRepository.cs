// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PackageRepository.cs" company="SANDs">
//   Copyright © 2016 SANDs. All rights reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace PackageManager.SharePoint.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.SharePoint.Administration;

    using NuGet;

    using PackageManager.SharePoint.Services.Interfaces;

    using IPackageRepository = PackageManager.SharePoint.Services.Interfaces.IPackageRepository;

    /// <summary>
    ///     The package repository.
    /// </summary>
    public class PackageRepository : IPackageRepository
    {
        /// <summary>
        ///     The package source repository.
        /// </summary>
        private readonly IPackageSourceRepository packageSourceRepository = new PackageSourceRepository();

        /// <summary>
        ///     The all.
        /// </summary>
        /// <returns>
        ///     The <see cref="IEnumerable" />.
        /// </returns>
        public IEnumerable<Package> All()
        {
            var solutions = SPFarm.Local.Solutions.ToList();
            foreach (var packageSource in this.packageSourceRepository.All())
            {
                if (packageSource.IsEnabled)
                {
                    var packageRepository = PackageRepositoryFactory.Default.CreateRepository(packageSource.Source);
                    IQueryable<IPackage> packages = null;
                    try
                    {
                        packages = packageRepository.GetPackages().Where(package => package.Id.ToLower().EndsWith(".wsp") && package.IsLatestVersion);
                    }
                    catch
                    {
                    }

                    if (packages != null)
                    {
                        foreach (var package in packages)
                        {
                            var solution = solutions.Find(s => s.Name.ToLower().Equals(package.Id.ToLower()));
                            if (solution != null)
                            {

                                SemanticVersion installedVersion;
                                if (!solution.Properties.Contains(Consts.VersionPropertyName) || !SemanticVersion.TryParse(solution.Properties[Consts.VersionPropertyName].ToString(), out installedVersion))
                                {
                                    installedVersion = new SemanticVersion(Consts.ZeroVersion);
                                }

                                yield return new Package(package, installedVersion);
                            }
                            else
                            {
                                yield return new Package(package);
                            }
                        }
                    }
                }
            }
        }
    }
}