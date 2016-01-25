// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PackagesDataSource.cs" company="">
//   
// </copyright>
// <summary>
//   The packages data source.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PackageManager.SharePoint
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading;

    using NuGet;

    using PackageManager.SharePoint.Services;

    using IPackageRepository = PackageManager.SharePoint.Services.Interfaces.IPackageRepository;

    /// <summary>
    /// The packages data source.
    /// </summary>
    public sealed class PackagesDataSource
    {
        /// <summary>
        /// The package repository.
        /// </summary>
        private readonly IPackageRepository packageRepository = new PackageRepository();

        /// <summary>
        /// The select packages method.
        /// </summary>
        /// <returns>
        /// The <see cref="DataTable"/>.
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
                dataTable.Rows.Add(package.Id, package.Version.ToString(), package.Installed, package.InstalledVersion != null ? package.InstalledVersion.ToString() : null);
            }

            return dataTable;
        }
    }
}