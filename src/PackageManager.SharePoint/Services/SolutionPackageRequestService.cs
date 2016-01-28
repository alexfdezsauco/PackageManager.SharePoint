// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SolutionPackageRequestService.cs" company="SANDs">
//   Copyright © 2016 SANDs. All rights reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace PackageManager.SharePoint.Services
{
    using System;
    using System.Collections.Generic;

    using NuGet;

    using PackageManager.SharePoint.Services.Interfaces;

    using Constants = PackageManager.SharePoint.Constants;

    /// <summary>
    ///     The package request service class.
    /// </summary>
    public class SolutionPackageRequestService : ISolutionPackageRequestService
    {
        /// <summary>
        /// Enumerates the required package requests.
        /// </summary>
        /// <param name="packageId">
        /// The package id.
        /// </param>
        /// <param name="version">
        /// The version.
        /// </param>
        /// <returns>
        /// An enumeration of package requests.
        /// </returns>
        public IEnumerable<SolutionPackageRequest> EnumPackageRequests(string packageId, IVersionSpec version)
        {
            if (packageId.EndsWith(Constants.SolutionPackagePostFix, StringComparison.CurrentCultureIgnoreCase))
            {
                var packageRequest = SolutionPackageRequestFactory.CreatePackageRequest(packageId, version);
                if (packageRequest != null)
                {
                    yield return packageRequest;

                    foreach (var dependencySet in packageRequest.Package.DependencySets)
                    {
                        foreach (var dependency in dependencySet.Dependencies)
                        {
                            foreach (var request in this.EnumPackageRequests(dependency.Id, dependency.VersionSpec))
                            {
                                yield return request;
                            }
                        }
                    }
                }
            }
        }
    }
}