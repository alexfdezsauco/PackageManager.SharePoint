// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPackageRequestService.cs" company="SANDs">
//   Copyright © 2016 SANDs. All rights reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace PackageManager.SharePoint.Services.Interfaces
{
    using System.Collections.Generic;

    using NuGet;

    /// <summary>
    /// The PackageRequestService interface.
    /// </summary>
    public interface IPackageRequestService
    {
        /// <summary>
        /// </summary>
        /// <param name="packageId">
        /// </param>
        /// <param name="version">
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        IEnumerable<PackageRequest> EnumPackageRequests(string packageId, IVersionSpec version);
    }
}