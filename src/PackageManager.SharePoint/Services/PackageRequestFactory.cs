// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PackageRequestFactory.cs" company="SANDs">
//   Copyright © 2016 SANDs. All rights reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace PackageManager.SharePoint.Services
{
    using NuGet;

    /// <summary>
    ///     The package request factory.
    /// </summary>
    public static class PackageRequestFactory
    {
        /// <summary>
        /// Creates a package request.
        /// </summary>
        /// <param name="packageId">
        /// The package id.
        /// </param>
        /// <param name="version">
        /// The version.
        /// </param>
        /// <returns>
        /// The <see cref="PackageRequest"/>.
        /// </returns>
        public static PackageRequest CreatePackageRequest(string packageId, IVersionSpec version)
        {
            var packageSourceRepository = new PackageSourceRepository();

            PackageRequest packageRequest = null;
            foreach (var packageSource in packageSourceRepository.All())
            {
                if (packageSource.IsEnabled)
                {
                    IPackage package = null;
                    var packageRepository = PackageRepositoryFactory.Default.CreateRepository(packageSource.Source);
                    try
                    {
                        package = packageRepository.FindPackage(packageId, version, true, true);
                    }
                    catch
                    {
                    }

                    if (package != null)
                    {
                        packageRequest = new PackageRequest(package, packageRepository);
                        break;
                    }
                }
            }

            return packageRequest;
        }

        /// <summary>
        /// Creates package request.
        /// </summary>
        /// <param name="packageId">
        /// The package id.
        /// </param>
        /// <param name="version">
        /// The version.
        /// </param>
        /// <returns>
        /// The <see cref="PackageRequest"/>.
        /// </returns>
        public static PackageRequest CreatePackageRequest(string packageId, SemanticVersion version)
        {
            return CreatePackageRequest(packageId, new VersionSpec(version));
        }
    }
}