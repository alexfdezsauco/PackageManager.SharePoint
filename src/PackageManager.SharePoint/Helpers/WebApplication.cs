// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebApplicationHelper.cs" company="SANDs">
//   Copyright © 2016 SANDs. All rights reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace PackageManager.SharePoint.Helpers
{
    using Microsoft.SharePoint;
    using Microsoft.SharePoint.Administration;

    /// <summary>
    /// The web application helper class.
    /// </summary>
    public static class WebApplicationHelper
    {
        /// <summary>
        ///     The resolve current web app with elevated privileges.
        /// </summary>
        /// <returns>
        ///     The <see cref="SPWebApplication" />.
        /// </returns>
        public static SPWebApplication CurrentWithElevatedPrivileges()
        {
            SPWebApplication application = null;
            SPSecurity.RunWithElevatedPrivileges(() =>
                {
                    using (var site = new SPSite(SPContext.Current.Site.ID))
                    {
                        application = site.WebApplication;
                    }
                });

            return application;
        }
    }
}