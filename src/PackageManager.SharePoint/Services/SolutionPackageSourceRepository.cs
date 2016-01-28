// --------------------------------------------------------------------------------------------------------------------
// <copyright file="solutionPackageSourceRepository.cs" company="SANDs">
//   Copyright © 2016 SANDs. All rights reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace PackageManager.SharePoint.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.SharePoint;
    using Microsoft.SharePoint.Administration;

    using Newtonsoft.Json;

    using PackageManager.SharePoint.Services.Interfaces;

    /// <summary>
    ///     The package source repository.
    /// </summary>
    public class SolutionPackageSourceRepository : ISolutionPackageSourceRepository
    {
        /// <summary>
        ///     Gets the package manager settings.
        /// </summary>
        private PackageManagerSettings PackageManagerSettings
        {
            get
            {
                SPPersistedObject parent = SPFarm.Local;
                var packageManagerSettings = parent.GetChild<PackageManagerSettings>(PackageManagerSettings.PackageSourceProperytyName);
                if (packageManagerSettings == null)
                {
                    SPContext.Current.Web.AllowUnsafeUpdates = true;
                    packageManagerSettings = new PackageManagerSettings(parent);
                    packageManagerSettings.Update(true);
                    SPContext.Current.Web.AllowUnsafeUpdates = false;
                }

                return packageManagerSettings;
            }
        }

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="solutionPackageSource">
        /// The package source.
        /// </param>
        public void Add(SolutionPackageSource solutionPackageSource)
        {
            var storedPackageSources = this.GetStoredPackageSources();
            storedPackageSources.Add(solutionPackageSource);
            this.SavePackageSources(storedPackageSources);
        }

        /// <summary>
        ///     The all.
        /// </summary>
        /// <returns>
        ///     The <see cref="IEnumerable" />.
        /// </returns>
        public IEnumerable<SolutionPackageSource> All()
        {
            return this.GetStoredPackageSources();
        }

        /// <summary>
        /// Finds a package source by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="SolutionPackageSource"/>.
        /// </returns>
        public SolutionPackageSource FindById(Guid id)
        {
            return this.GetStoredPackageSources().FirstOrDefault(source => source.Id == id);
        }

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="solutionPackageSource">
        /// The package source.
        /// </param>
        public void Update(SolutionPackageSource solutionPackageSource)
        {
            var storedPackageSources = this.GetStoredPackageSources();
            var idx = storedPackageSources.FindIndex(source => source.Id == solutionPackageSource.Id);
            if (idx != -1)
            {
                storedPackageSources[idx] = solutionPackageSource;
                this.SavePackageSources(storedPackageSources);
            }
        }

        /// <summary>
        /// Deletes a package source.
        /// </summary>
        /// <param name="id">
        /// The package source id.
        /// </param>
        public void Delete(Guid id)
        {
            var storedPackageSources = this.GetStoredPackageSources();
            var idx = storedPackageSources.FindIndex(source => source.Id == id);
            if (idx != -1)
            {
                storedPackageSources.RemoveAt(idx);
                this.SavePackageSources(storedPackageSources);
            }
        }

        /// <summary>
        ///     Gets the stored package source.
        /// </summary>
        /// <returns>
        ///     The <see cref="List{SolutionPackageSource}" />.
        /// </returns>
        private List<SolutionPackageSource> GetStoredPackageSources()
        {
            this.InitializeWithDefaultRepositoryIfRequiered();
            return JsonConvert.DeserializeObject<List<SolutionPackageSource>>(PackageManagerSettings.PackageSources);
        }

        /// <summary>
        ///     The initialize with default repository if requiered.
        /// </summary>
        private void InitializeWithDefaultRepositoryIfRequiered()
        {
            if (string.IsNullOrWhiteSpace(this.PackageManagerSettings.PackageSources) || this.PackageManagerSettings.PackageSources == "[]")
            {
                this.SavePackageSources(new List<SolutionPackageSource> { new SolutionPackageSource(Guid.NewGuid(), "https://www.nuget.org/api/v2/", "NuGet Gallery", true) });
            }
        }

        /// <summary>
        /// The save package source.
        /// </summary>
        /// <param name="packageSources">
        /// The package sources.
        /// </param>
        public void SavePackageSources(List<SolutionPackageSource> packageSources)
        {
            var packageManagerSettings = PackageManagerSettings;
            packageManagerSettings.PackageSources = JsonConvert.SerializeObject(packageSources);
            packageManagerSettings.Update(true);
        }
    }
}