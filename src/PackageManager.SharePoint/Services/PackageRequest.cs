// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PackageRequest.cs" company="SANDs">
//   Copyright © 2016 SANDs. All rights reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace PackageManager.SharePoint.Services
{
    using NuGet;

    /// <summary>
    ///     The package request.
    /// </summary>
    public class PackageRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PackageRequest"/> class.
        /// </summary>
        /// <param name="package">
        /// The package.
        /// </param>
        /// <param name="repository">
        /// The repository.
        /// </param>
        public PackageRequest(IPackage package, IPackageRepository repository)
        {
            this.Package = package;
            this.Repository = repository;
        }

        /// <summary>
        ///     Gets the package.
        /// </summary>
        public IPackage Package { get; private set; }

        /// <summary>
        ///     Gets the repository.
        /// </summary>
        public IPackageRepository Repository { get; private set; }
    }
}