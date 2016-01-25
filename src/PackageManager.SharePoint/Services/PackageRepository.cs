// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PackageRepository.cs" company="">
//   
// </copyright>
// <summary>
//   The package repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace PackageManager.SharePoint.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.SharePoint.Administration;

    using NuGet;

    using PackageManager.SharePoint.Services.Interfaces;

    using IPackageRepository = PackageManager.SharePoint.Services.Interfaces.IPackageRepository;
    using PackageSource = PackageManager.SharePoint.PackageSource;

    /// <summary>
    ///     The package repository.
    /// </summary>
    public class PackageRepository : IPackageRepository
    {
        /// <summary>
        /// The package source repository.
        /// </summary>
        private readonly IPackageSourceRepository packageSourceRepository = new PackageSourceRepository();

        /// <summary>
        /// The all.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<Package> All()
        {
            var solutions = SPFarm.Local.Solutions.ToList();
            foreach (PackageSource packageSource in this.packageSourceRepository.All())
            {
                var packageRepository = PackageRepositoryFactory.Default.CreateRepository(packageSource.Source);
                var packages = packageRepository.GetPackages().Where(package => package.Id.ToLower().EndsWith(".wsp") && package.IsLatestVersion);
                foreach (var package in packages)
                {
                    var solution = solutions.Find(s => s.Name.ToLower().Equals(package.Id.ToLower()));
                    if (solution != null)
                    {
                        var installedVersion = string.IsNullOrWhiteSpace((string)solution.Properties["Version"]) ? new SemanticVersion("0.0.0.0") : new SemanticVersion((string)solution.Properties["Version"]);
                        yield return new Package(package, true, installedVersion);
                    }
                    else
                    {
                        yield return new Package(package, false, null);
                    }
                }
            }
        }
    }
}