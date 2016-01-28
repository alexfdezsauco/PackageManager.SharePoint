// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PackageSourceDataSource.cs" company="SANDs">
//   Copyright © 2016 SANDs. All rights reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace PackageManager.SharePoint.DataSources
{
    using System;
    using System.Data;

    using PackageManager.SharePoint.Services;
    using PackageManager.SharePoint.Services.Interfaces;

    /// <summary>
    ///     The package source data source.
    /// </summary>
    public class PackageSourcesDataSource
    {
        /// <summary>
        ///     The package repository.
        /// </summary>
        private readonly ISolutionPackageSourceRepository solutionPackageSourcesRepository = new SolutionPackageSourceRepository();

        /// <summary>
        ///     The select package sources.
        /// </summary>
        /// <returns>
        ///     The <see cref="DataTable" />.
        /// </returns>
        public DataTable SelectPackageSources()
        {
            var dataTable = new DataTable();

            dataTable.Columns.Add("id", typeof(Guid));
            dataTable.Columns.Add("name");
            dataTable.Columns.Add("source");
            dataTable.Columns.Add("enabled", typeof(bool));

            var storedPackageSource = this.solutionPackageSourcesRepository.All();
            foreach (var packageSource in storedPackageSource)
            {
                dataTable.Rows.Add(packageSource.Id.ToString(), packageSource.Name, packageSource.Source, packageSource.IsEnabled);
            }

            return dataTable;
        }

        /// <summary>
        /// Deletes the package source.
        /// </summary>
        /// <param name="id">
        /// The package source id.
        /// </param>
        public void DeletePackageSource(Guid id)
        {
            this.solutionPackageSourcesRepository.Delete(id);
        }

        /// <summary>
        /// The update package source.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="source">
        /// The source
        /// </param>
        /// <param name="enabled">
        /// The enabled.
        /// </param>
        public void UpdatePackageSource(Guid id, string name, string source, bool enabled)
        {
            var storedPackageSource = this.solutionPackageSourcesRepository.FindById(id);
            if (storedPackageSource != null)
            {
                storedPackageSource.Name = name;
                storedPackageSource.Source = source;
                storedPackageSource.IsEnabled = enabled;
                this.solutionPackageSourcesRepository.Update(storedPackageSource);
            }
        }
    }
}