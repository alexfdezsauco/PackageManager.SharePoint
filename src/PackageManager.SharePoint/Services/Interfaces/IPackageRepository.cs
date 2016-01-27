// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IXPackageRepository.cs" company="SANDs">
//   Copyright � 2016 SANDs. All rights reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace PackageManager.SharePoint.Services.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    ///     The PackageRepository interface.
    /// </summary>
    public interface IPackageRepository
    {
        /// <summary>
        ///     The all.
        /// </summary>
        /// <returns>
        ///     The <see cref="IEnumerable" />.
        /// </returns>
        IEnumerable<Package> All();
    }
}