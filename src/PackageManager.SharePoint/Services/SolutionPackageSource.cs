// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SolutionPackageSource.cs" company="SANDs">
//   Copyright © 2016 SANDs. All rights reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace PackageManager.SharePoint.Services
{
    using System;

    /// <summary>
    ///     The package source.
    /// </summary>
    public class SolutionPackageSource
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SolutionPackageSource" /> class.
        /// </summary>
        public SolutionPackageSource()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SolutionPackageSource"/> class.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="isEnabled">
        /// The enabled.
        /// </param>
        public SolutionPackageSource(Guid id, string source, string name, bool isEnabled)
        {
            this.Id = id;
            this.Source = source;
            this.Name = name;
            this.IsEnabled = isEnabled;
        }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Gets or sets the source.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether enabled.
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}