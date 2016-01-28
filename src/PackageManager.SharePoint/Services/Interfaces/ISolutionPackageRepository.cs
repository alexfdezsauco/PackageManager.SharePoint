// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IXPackageRepository.cs" company="SANDs">
//   Copyright © 2016 SANDs. All rights reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace PackageManager.SharePoint.Services.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    ///     The solution package repository interface.
    /// </summary>
    public interface ISolutionPackageRepository
    {
        /// <summary>
        ///     Enumerates all available solution packages.
        /// </summary>
        /// <returns>
        ///     The <see cref="IEnumerable{SolutionPackage}" />.
        /// </returns>
        IEnumerable<SolutionPackage> All();

        /// <summary>
        /// Enumerates the installed <see cref="SolutionPackage"/>
        /// </summary>
        /// <returns>
        /// An enumeration of <see cref="SolutionPackage"/>.
        /// </returns>
        IEnumerable<SolutionPackage> Installed();
    }
}