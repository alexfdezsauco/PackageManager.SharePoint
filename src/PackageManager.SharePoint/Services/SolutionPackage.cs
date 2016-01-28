// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SolutionPackage.cs" company="SANDs">
//   Copyright © 2016 SANDs. All rights reserved
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PackageManager.SharePoint.Services
{
    using NuGet;

    /// <summary>
    ///     The package.
    /// </summary>
    public class SolutionPackage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SolutionPackage"/> class.
        /// </summary>
        /// <param name="package">
        /// The package.
        /// </param>
        /// <param name="installedVersion">
        /// </param>
        public SolutionPackage(IPackage package, SemanticVersion installedVersion = null)
        {
            this.Id = package.Id;
            this.AvailableVersion = package.Version;
            this.Installed = installedVersion != null;
            this.InstalledVersion = installedVersion;
        }

        /// <summary>
        ///     Gets a value indicating whether installed.
        /// </summary>
        public bool Installed { get; private set; }

        /// <summary>
        ///     Gets or sets the installed version.
        /// </summary>
        public SemanticVersion InstalledVersion { get; set; }

        /// <summary>
        ///     Gets the id.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        ///     Gets the version.
        /// </summary>
        public SemanticVersion AvailableVersion { get; private set; }
    }
}