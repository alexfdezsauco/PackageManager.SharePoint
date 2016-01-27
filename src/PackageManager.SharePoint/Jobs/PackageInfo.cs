// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PackageUpdateInfo.cs" company="SANDs">
//   Copyright © 2016 SANDs. All rights reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace PackageManager.SharePoint.Jobs
{
    /// <summary>
    ///     The package info.
    /// </summary>
    public class PackageInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PackageInfo"/> class.
        /// </summary>
        public PackageInfo()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PackageInfo"/> class.
        /// </summary>
        /// <param name="id">
        /// The package id.
        /// </param>
        /// <param name="version">
        /// The version.
        /// </param>
        public PackageInfo(string id, string version)
        {
            this.Id = id;
            this.Version = version;
        }

        /// <summary>
        ///     Gets or sets the package id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Gets or sets the version.
        /// </summary>
        public string Version { get; set; }
    }
}