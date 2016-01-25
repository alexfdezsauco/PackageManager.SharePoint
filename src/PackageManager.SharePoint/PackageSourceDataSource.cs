// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PackageSourceDataSource.cs" company="">
//   
// </copyright>
// <summary>
//   The package source data source.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace PackageManager.SharePoint
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
        private readonly IPackageSourceRepository packageSourcesRepository = new PackageSourceRepository();

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

            var storedPackageSource = this.packageSourcesRepository.All();
            foreach (var packageSource in storedPackageSource)
            {
                dataTable.Rows.Add(packageSource.Id.ToString(), packageSource.Name, packageSource.Source, packageSource.IsEnabled);
            }

            return dataTable;
        }

        /// <summary>
        ///  Deletes the package source.
        /// </summary>
        /// <param name="id">
        /// The package source id.
        /// </param>
        public void DeletePackageSource(Guid id)
        {
            this.packageSourcesRepository.Delete(id);
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
            var storedPackageSource = this.packageSourcesRepository.FindById(id);
            if (storedPackageSource != null)
            {
                storedPackageSource.Name = name;
                storedPackageSource.Source = source;
                storedPackageSource.IsEnabled = enabled;
                this.packageSourcesRepository.Update(storedPackageSource);
            }
        }
    }
}