// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ManageFarmPackages.aspx.cs" company="">
//   
// </copyright>
// <summary>
//   The manage farm packages page.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace PackageManager.SharePoint.Layouts.PackageManager.SharePoint
{
    using System;

    using Microsoft.SharePoint.WebControls;

    using NuGet;

    /// <summary>
    ///     The manage farm packages page.
    /// </summary>
    public partial class ManageFarmPackagesPage : LayoutsPageBase
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
            this.PackagesDataSource.TypeName = typeof(PackagesDataSource).AssemblyQualifiedName;
        }
    }
}