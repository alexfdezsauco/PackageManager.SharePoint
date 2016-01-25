// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Package.cs" company="">
//   
// </copyright>
// <summary>
//   The package.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace PackageManager.SharePoint
{
    using NuGet;

    /// <summary>
    ///     The package.
    /// </summary>
    public class Package
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Package"/> class.
        /// </summary>
        /// <param name="package">
        /// The package.
        /// </param>
        /// <param name="installed">
        /// The b.
        /// </param>
        /// <param name="installedVersion">
        /// </param>
        public Package(IPackage package, bool installed, SemanticVersion installedVersion)
        {
            this.Id = package.Id;
            this.Version = package.Version;
            this.Installed = installed;
            this.InstalledVersion = installedVersion;
        }

        /// <summary>
        ///     Gets a value indicating whether installed.
        /// </summary>
        public bool Installed { get; private set; }

        /// <summary>
        /// Gets or sets the installed version.
        /// </summary>
        public SemanticVersion InstalledVersion { get; set; }

        /// <summary>
        ///     Gets the id.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        ///     Gets the version.
        /// </summary>
        public SemanticVersion Version { get; private set; }
    }
}