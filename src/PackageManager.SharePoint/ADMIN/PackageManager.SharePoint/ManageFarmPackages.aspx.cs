// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ManageFarmPackages.aspx.cs" company="SANDs">
//   Copyright © 2016 SANDs. All rights reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace PackageManager.SharePoint.Layouts.PackageManager.SharePoint
{
    using System;
    using System.Linq;
    using System.Web.UI.WebControls;

    using global::PackageManager.SharePoint.DataSources;
    using global::PackageManager.SharePoint.Extensions;
    using global::PackageManager.SharePoint.Helpers;
    using global::PackageManager.SharePoint.Jobs;

    using Microsoft.SharePoint;
    using Microsoft.SharePoint.WebControls;

    /// <summary>
    ///     The manage farm packages page.
    /// </summary>
    public partial class ManageFarmPackagesPage : LayoutsPageBase
    {
        /// <summary>
        /// Indicates whether a instance of <see cref="InstallOrUpdateSolutionPackagesJob"/> job scheduled or running.
        /// </summary>
        public bool isJobScheduledOrRunning;

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
            this.CheckJobStatus();
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
            if (!this.isJobScheduledOrRunning && (e.CommandName.Equals("Install") || e.CommandName.Equals("Update")))
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

                // This call is a patch.
                this.CheckJobStatus();
                this.Refresh();
            }

            e.Handled = true;
        }

        /// <summary>
        /// Checks the job status.
        /// </summary>
        private void CheckJobStatus()
        {
            this.isJobScheduledOrRunning = WebApplicationHelper.CurrentWithElevatedPrivileges().IsJobScheduledOrRunning<InstallOrUpdateSolutionPackagesJob>();
            if (this.isJobScheduledOrRunning)
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "ShowJobIsScheduledOrRunningStatusMessage", "<script>ExecuteOrDelayUntilScriptLoaded(showJobIsScheduledOrRunningStatusMessage,'sp.js');</script>");
            }
        }

        /// <summary>
        ///     Disable UI Interaction
        /// </summary>
        private void Refresh()
        {
            foreach (GridViewRow row in this.PackageSourceSPGridView.Rows)
            {
                foreach (var button in row.Cells[4].Controls.OfType<Button>())
                {
                    button.Enabled = !this.isJobScheduledOrRunning;
                }
            }
        }
    }
}