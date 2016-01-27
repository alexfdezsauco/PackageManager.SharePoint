// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BindHelper.cs" company="SANDs">
//   Copyright © 2016 SANDs. All rights reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace PackageManager.SharePoint.Helpers
{
    using NuGet;

    /// <summary>
    /// The bind helper.
    /// </summary>
    public static class BindHelper
    {
        /// <summary>
        /// The greater than.
        /// </summary>
        /// <param name="version1">
        /// The version 1.
        /// </param>
        /// <param name="version2">
        /// The version 2.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool GreaterThan(object version1, object version2)
        {
            SemanticVersion semanticVersion1;
            SemanticVersion semanticVersion2;
            return SemanticVersion.TryParse(version1.ToString(), out semanticVersion1) && SemanticVersion.TryParse(version2.ToString(), out semanticVersion2) && semanticVersion1.CompareTo(semanticVersion2) > 0;
        }

        /// <summary>
        /// The is installed.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsInstalled(object value)
        {
            bool result;
            return value != null && bool.TryParse(value.ToString(), out result) && result;
        }
    }
}