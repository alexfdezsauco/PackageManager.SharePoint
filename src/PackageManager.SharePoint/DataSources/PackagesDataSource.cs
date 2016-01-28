// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PackagesDataSource.cs" company="SANDs">
//   Copyright © 2016 SANDs. All rights reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace PackageManager.SharePoint.DataSources
{
    using System.Data;

    using PackageManager.SharePoint.Services;
    using PackageManager.SharePoint.Services.Interfaces;

    /// <summary>
    ///     The packages data source.
    /// </summary>
    public sealed class PackagesDataSource
    {
        /// <summary>
        ///     The package repository.
        /// </summary>
        private readonly ISolutionPackageRepository packageRepository = new SolutionPackageRepository();

        /// <summary>
        ///     The select packages method.
        /// </summary>
        /// <returns>
        ///     The <see cref="DataTable" />.
        /// </returns>
        public DataTable SelectPackages()
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("id");
            dataTable.Columns.Add("version");
            dataTable.Columns.Add("installed");
            dataTable.Columns.Add("installedVersion");

            foreach (var package in this.packageRepository.All())
            {
                dataTable.Rows.Add(package.Id, package.Version.ToString(), package.Installed, package.Installed ? package.InstalledVersion.ToString() : null);
            }

            return dataTable;
        }
    }
}