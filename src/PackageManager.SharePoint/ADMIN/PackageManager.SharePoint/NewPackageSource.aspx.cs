// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NewPackageSource.aspx.cs" company="SANDs">
//   Copyright © 2016 SANDs. All rights reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace PackageManager.SharePoint.Layouts.PackageManager.SharePoint
{
    using System;
    using System.Globalization;

    using global::PackageManager.SharePoint.Services;
    using global::PackageManager.SharePoint.Services.Interfaces;

    using Microsoft.SharePoint.WebControls;

    /// <summary>
    ///     The new package source.
    /// </summary>
    public partial class NewPackageSource : LayoutsPageBase
    {
        /// <summary>
        ///     The package source repository.
        /// </summary>
        private IPackageSourceRepository packageSourceRepository;

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
            this.packageSourceRepository = new PackageSourceRepository();
        }

        /// <summary>
        /// The button 2_ on click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void Button2_OnClick(object sender, EventArgs e)
        {
            this.CloseDialog(DialogResult.Cancel);
        }

        /// <summary>
        /// The button 1_ on click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void Button1_OnClick(object sender, EventArgs e)
        {
            this.packageSourceRepository.Add(new PackageSource(Guid.NewGuid(), this.SourceTextBox.Text, this.NameTextBox.Text, true));
            this.CloseDialog(DialogResult.OK);
        }

        /// <summary>
        /// The close dialog.
        /// </summary>
        /// <param name="dialogResult">
        /// The dialog result.
        /// </param>
        private void CloseDialog(DialogResult dialogResult)
        {
            this.Context.Response.Write(string.Format(CultureInfo.InvariantCulture, "<script type='text/javascript'>window.frameElement.commonModalDialogClose({0}, '{1}');</script>", (int)dialogResult, string.Empty));
            this.Context.Response.Flush();
            this.Context.Response.End();
        }
    }
}