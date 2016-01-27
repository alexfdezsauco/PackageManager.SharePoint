// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ManageFarmPackages.aspx.cs" company="SANDs">
//   Copyright © 2016 SANDs. All rights reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace PackageManager.SharePoint.Layouts.PackageManager.SharePoint
{
    using System;
    using System.Web.UI.WebControls;

    using global::PackageManager.SharePoint.DataSources;
    using global::PackageManager.SharePoint.Jobs;

    using Microsoft.SharePoint;
    using Microsoft.SharePoint.WebControls;

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

        /// <summary>
        /// The package source sp grid view_ on row command.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void PackageSourceSPGridView_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Install") || e.CommandName.Equals("Update"))
            {
                var strings = e.CommandArgument.ToString().Split(';');
                var packageId = strings[0].Trim();
                var version = strings[1].Trim();
                var installOrUpdatePackageJob = new InstallOrUpdateSolutionPackagesJob(e.CommandName)
                                                    {
                                                        Schedule = new SPOneTimeSchedule(DateTime.Now.AddSeconds(2))
                                                    };

                installOrUpdatePackageJob.Packages.Add(new PackageInfo(packageId, version));
                installOrUpdatePackageJob.Update();
            }

            e.Handled = true;
        }
    }
}