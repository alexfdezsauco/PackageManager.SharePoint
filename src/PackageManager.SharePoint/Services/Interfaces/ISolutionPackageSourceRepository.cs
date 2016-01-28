// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISolutionPackageSourceRepository.cs" company="SANDs">
//   Copyright © 2016 SANDs. All rights reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace PackageManager.SharePoint.Services.Interfaces
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     The solutionPackageSourceRepository interface.
    /// </summary>
    internal interface ISolutionPackageSourceRepository
    {
        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="solutionPackageSource">
        /// The package source.
        /// </param>
        void Add(SolutionPackageSource solutionPackageSource);

        /// <summary>
        ///     The all.
        /// </summary>
        /// <returns>
        ///     The <see cref="IEnumerable" />.
        /// </returns>
        IEnumerable<SolutionPackageSource> All();

        /// <summary>
        /// The find by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="SolutionPackageSource"/>.
        /// </returns>
        SolutionPackageSource FindById(Guid id);

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="solutionPackageSource">
        /// The package source.
        /// </param>
        void Update(SolutionPackageSource solutionPackageSource);

        /// <summary>
        /// Deletes a package source.
        /// </summary>
        /// <param name="id">
        /// The package source id.
        /// </param>
        void Delete(Guid id);
    }
}