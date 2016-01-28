namespace PackageManager.SharePoint.Extensions
{
    using Microsoft.SharePoint.Administration;

    using NuGet;

    using Constants = PackageManager.SharePoint.Constants;

    /// <summary>
    /// The sp solution extensions.
    /// </summary>
    public static class SPSolutionExtensions
    {
        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <param name="this">
        /// The spsolution
        /// </param>
        /// <returns>
        /// The version.
        /// </returns>
        public static SemanticVersion GetVersion(this SPSolution @this)
        {
            SemanticVersion installedVersion;
            if (!@this.Properties.Contains(Constants.VersionPropertyName) || !SemanticVersion.TryParse(@this.Properties[Constants.VersionPropertyName].ToString(), out installedVersion))
            {
                installedVersion = new SemanticVersion(Constants.ZeroVersion);
            }

            return installedVersion;
        }

        /// <summary>
        /// Sets the solution version
        /// </summary>
        /// <param name="this">
        /// The sp solution
        /// </param>
        /// <param name="version">
        /// The version.
        /// </param>
        public static void SetVersion(this SPSolution @this, SemanticVersion version)
        {
            @this.Properties[Constants.VersionPropertyName] = version.ToString();
            @this.Update();
        }
    }
}