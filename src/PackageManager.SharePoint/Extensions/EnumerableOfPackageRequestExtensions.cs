// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumarableOfPackageRequestExtensions.cs" company="SANDs">
//   Copyright © 2016 SANDs. All rights reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace PackageManager.SharePoint.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NuGet;

    using PackageManager.SharePoint.Services;

    /// <summary>
    ///     The <see cref="IEnumerable{T}" /> extension method class.
    /// </summary>
    public static class EnumerableOfPackageRequestExtensions
    {
        /// <summary>
        /// Groups the package requests by package id but returns the request for higher version only.
        /// </summary>
        /// <param name="packageRequests">
        /// The package requests
        /// </param>
        /// <returns>
        /// Distinct enumeration of package requests with higher version only.
        /// </returns>
        public static IEnumerable<PackageRequest> Distinct(this IEnumerable<PackageRequest> packageRequests)
        {
            var packageRequestStorage = new SortedDictionary<string, PackageRequest>();
            foreach (var packageRequest in packageRequests)
            {
                var packageId = packageRequest.Package.Id;
                if (!packageRequestStorage.Keys.Contains(packageId))
                {
                    packageRequestStorage[packageId] = packageRequest;
                }
                else
                {
                    var storedPackageRequest = packageRequestStorage[packageId];
                    if (storedPackageRequest.Package.Version.CompareTo(packageRequest.Package.Version) == -1)
                    {
                        packageRequestStorage[packageId] = packageRequest;
                    }
                }
            }

            return packageRequestStorage.Values;
        }

        /// <summary>
        /// Sorts package request by dependencies
        /// </summary>
        /// <param name="packageRequests">
        /// Package requests
        /// </param>
        /// <returns>
        /// Sorted enumeration of package request.
        /// </returns>
        public static IEnumerable<PackageRequest> SortByDependencies(this IEnumerable<PackageRequest> packageRequests)
        {
            var packageRequestList = packageRequests.ToList();
            var sortedPackageRequestList = new List<PackageRequest>(packageRequestList);
            for (var i = 0; i < packageRequestList.Count; i++)
            {
                var currentPackageRequest = packageRequestList[i];
                var newIdx = sortedPackageRequestList.FindIndex(request => request.Package.Id.ToLower().Equals(currentPackageRequest.Package.Id.ToLower()));

                var dependencies = new List<PackageDependency>();
                foreach (var dependencySet in currentPackageRequest.Package.DependencySets)
                {
                    dependencies.AddRange(dependencySet.Dependencies);
                }

                if (dependencies.Count == 0)
                {
                    sortedPackageRequestList.RemoveAt(newIdx);
                    sortedPackageRequestList.Insert(0, currentPackageRequest);
                }
                else
                {
                    var idx = int.MinValue;
                    foreach (var dependency in dependencies)
                    {
                        idx = Math.Max(idx, sortedPackageRequestList.FindIndex(request => request.Package.Id.ToLower().Equals(dependency.Id.ToLower())));
                    }

                    sortedPackageRequestList.RemoveAt(newIdx);
                    if (idx == -1)
                    {
                        sortedPackageRequestList.Insert(0, currentPackageRequest);
                    }
                    else if (newIdx < idx)
                    {
                        sortedPackageRequestList.Insert(idx, currentPackageRequest);
                        i = -1;
                    }
                    else
                    {
                        sortedPackageRequestList.Insert(idx + 1, currentPackageRequest);
                    }
                }
            }

            return sortedPackageRequestList;
        }
    }
}