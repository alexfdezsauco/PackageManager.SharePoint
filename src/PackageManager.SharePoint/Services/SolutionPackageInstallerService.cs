// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SolutionPackageInstallerService.cs" company="SANDs">
//   Copyright © 2016 SANDs. All rights reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace PackageManager.SharePoint.Services
{
    using System;

    using Microsoft.SharePoint;

    using PackageManager.SharePoint.Jobs;
    using PackageManager.SharePoint.Services.Interfaces;

    /// <summary>
    /// The SolutionPackageInstallerService class.
    /// </summary>
    public class SolutionPackageInstallerService : ISolutionPackageInstallerService
    {
        /// <summary>
        /// </summary>
        /// <param name="packageId"></param>
        /// <param name="version"></param>
        /// <param name="commandName"></param>
        public void InstallOrUpdate(string packageId, string version, string commandName)
        {
            var solutionPackageInstallerJob = new SolutionPackageInstallerJob(commandName);
            solutionPackageInstallerJob.Packages.Add(new PackageInfo(packageId, version));
            solutionPackageInstallerJob.Schedule = new SPOneTimeSchedule(DateTime.Now.AddSeconds(2));
            solutionPackageInstallerJob.Update();
        }

        /// <summary>
        /// Updates all installed package.
        /// </summary>
        public void UpdateAll()
        {
            var solutionPackageRepository = new SolutionPackageRepository();
            var solutionPackageInstallerJob = new SolutionPackageInstallerJob(Commands.Update);
            foreach (var solutionPackage in solutionPackageRepository.Installed())
            {
                if (solutionPackage.Installed.CompareTo(solutionPackage.AvailableVersion) != -1)
                {
                    solutionPackageInstallerJob.Packages.Add(new PackageInfo(solutionPackage.Id, solutionPackage.AvailableVersion.ToString()));
                }
            }

            solutionPackageInstallerJob.Schedule = new SPOneTimeSchedule(DateTime.Now.AddSeconds(2));
            solutionPackageInstallerJob.Update();
        }
    }
}