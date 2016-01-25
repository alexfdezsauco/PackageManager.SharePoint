// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PackageManagerSettings.cs" company="">
//   
// </copyright>
// <summary>
//   The package manager settings.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace PackageManager.SharePoint.Services
{
    using Microsoft.SharePoint.Administration;

    /// <summary>
    ///     The package manager settings.
    /// </summary>
    public class PackageManagerSettings : SPPersistedObject
    {
        /// <summary>
        ///     The package source property settings.
        /// </summary>
        public const string PackageSourceProperytyName = "PackageManager.Settings";

        /// <summary>
        /// The package sources.
        /// </summary>
        [Persisted]
        private string packageSources;

        /// <summary>
        /// Initializes a new instance of the <see cref="PackageManagerSettings"/> class.
        /// </summary>
        /// <param name="parent">
        /// The parent.
        /// </param>
        public PackageManagerSettings(SPPersistedObject parent)
            : base(PackageSourceProperytyName, parent)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PackageManagerSettings" /> class.
        /// </summary>
        public PackageManagerSettings()
        {
        }

        /// <summary>
        /// Gets or sets the package sources.
        /// </summary>
        public string PackageSources
        {
            get
            {
                return this.packageSources;
            }

            set
            {
                this.packageSources = value;
            }
        }

        /// <summary>
        ///     The has additional update access.
        /// </summary>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        protected override bool HasAdditionalUpdateAccess()
        {
            return true;
        }
    }
}