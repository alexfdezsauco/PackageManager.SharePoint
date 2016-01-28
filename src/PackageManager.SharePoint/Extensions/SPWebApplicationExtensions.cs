// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SPWebApplicationExtensions.cs" company="SANDs">
//   Copyright © 2016 SANDs. All rights reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace PackageManager.SharePoint.Extensions
{
    using System.Linq;

    using Microsoft.SharePoint.Administration;

    /// <summary>
    /// The sp web application extensions.
    /// </summary>
    public static class SPWebApplicationExtensions
    {
        /// <summary>
        /// The is job scheduled or running.
        /// </summary>
        /// <param name="this">
        /// The this.
        /// </param>
        /// <typeparam name="TSPJobDefinition">
        /// </typeparam>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsJobScheduledOrRunning<TSPJobDefinition>(this SPWebApplication @this)
        {
            return @this.JobDefinitions.FirstOrDefault(job => job is TSPJobDefinition) != null || @this.RunningJobs.OfType<SPRunningJob>().FirstOrDefault(job => job.JobDefinition is TSPJobDefinition) != null;
        }
    }
}