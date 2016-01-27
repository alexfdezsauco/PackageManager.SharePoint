// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPackageSourceRepository.cs" company="SANDs">
//   Copyright © 2016 SANDs. All rights reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace PackageManager.SharePoint.Services.Interfaces
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     The PackageSourceRepository interface.
    /// </summary>
    internal interface IPackageSourceRepository
    {
        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="packageSource">
        /// The package source.
        /// </param>
        void Add(PackageSource packageSource);

        /// <summary>
        ///     The all.
        /// </summary>
        /// <returns>
        ///     The <see cref="IEnumerable" />.
        /// </returns>
        IEnumerable<PackageSource> All();

        /// <summary>
        /// The find by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="PackageSource"/>.
        /// </returns>
        PackageSource FindById(Guid id);

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="packageSource">
        /// The package source.
        /// </param>
        void Update(PackageSource packageSource);

        /// <summary>
        /// Deletes a package source.
        /// </summary>
        /// <param name="id">
        /// The package source id.
        /// </param>
        void Delete(Guid id);
    }
}