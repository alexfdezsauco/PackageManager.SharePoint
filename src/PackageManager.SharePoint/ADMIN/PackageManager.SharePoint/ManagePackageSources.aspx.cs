// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ManagePackageSource.aspx.cs" company="SANDs">
//   Copyright © 2016 SANDs. All rights reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace PackageManager.SharePoint.Layouts.PackageManager.SharePoint
{
    using System;

    using global::PackageManager.SharePoint.DataSources;

    using Microsoft.SharePoint.WebControls;

    /// <summary>
    ///     The manage package source page.
    /// </summary>
    public partial class ManagePackageSourcesPage : LayoutsPageBase
    {
        /// <summary>
        /// The page_ load.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        ///     The create child controls.
        /// </summary>
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            this.PackageSourcesDataSource.TypeName = typeof(PackageSourcesDataSource).AssemblyQualifiedName;
        }
    }
}