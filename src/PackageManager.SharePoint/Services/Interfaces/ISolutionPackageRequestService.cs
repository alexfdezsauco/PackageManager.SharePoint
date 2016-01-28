// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISolutionPackageRequestService.cs" company="SANDs">
//   Copyright © 2016 SANDs. All rights reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace PackageManager.SharePoint.Services.Interfaces
{
    using System.Collections.Generic;

    using NuGet;

    /// <summary>
    /// The SolutionPackageRequestService interface.
    /// </summary>
    public interface ISolutionPackageRequestService
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
        IEnumerable<SolutionPackageRequest> EnumPackageRequests(string packageId, IVersionSpec version);
    }
}