﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SolutionPackageInstallerJob.cs" company="SANDs">
//   Copyright © 2016 SANDs. All rights reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace PackageManager.SharePoint.Jobs
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    using Microsoft.SharePoint.Administration;

    using Newtonsoft.Json;

    using NuGet;

    using PackageManager.SharePoint.Extensions;
    using PackageManager.SharePoint.Helpers;
    using PackageManager.SharePoint.Services;

    using Constants = PackageManager.SharePoint.Constants;

    /// <summary>
    ///     The install or update package job.
    /// </summary>
    public sealed class SolutionPackageInstallerJob : JobDefinitionBase
    {
        /// <summary>
        ///     The job name pattern.
        /// </summary>
        private const string JobNamePattern = "Install-Or-Update-PackageJob-{0}";

        /// <summary>
        ///     The job title pattern.
        /// </summary>
        private const string SolutionPackageInstallerJobTitle = "Installing or updating solution packages";

        /// <summary>
        ///     The packages to be installed by this job.
        /// </summary>
        [Persisted]
        private string serializedPackages;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SolutionPackageInstallerJob" /> class.
        /// </summary>
        public SolutionPackageInstallerJob()
        {
            this.InitializePackages();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SolutionPackageInstallerJob"/> class.
        /// </summary>
        /// <param name="command">
        /// The command.
        /// </param>
        public SolutionPackageInstallerJob(string command)
            : this(string.Format(CultureInfo.InvariantCulture, JobNamePattern, Guid.NewGuid()), WebApplicationHelper.CurrentWithElevatedPrivileges(), null, SPJobLockType.Job)
        {
            // TODO: Add validation for command?
            this.Title = SolutionPackageInstallerJobTitle;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SolutionPackageInstallerJob"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="service">
        /// The service.
        /// </param>
        /// <param name="server">
        /// The server.
        /// </param>
        /// <param name="lockType">
        /// The lock type.
        /// </param>
        private SolutionPackageInstallerJob(string name, SPService service, SPServer server, SPJobLockType lockType)
            : base(name, service, server, lockType)
        {
            this.InitializePackages();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SolutionPackageInstallerJob"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="webApplication">
        /// The web application.
        /// </param>
        /// <param name="server">
        /// The server
        /// </param>
        /// <param name="lockType">
        /// The lock type.
        /// </param>
        private SolutionPackageInstallerJob(string name, SPWebApplication webApplication, SPServer server, SPJobLockType lockType)
            : base(name, webApplication, server, lockType)
        {
            this.InitializePackages();
        }

        /// <summary>
        ///     Gets the package.
        /// </summary>
        public ObservableCollection<PackageInfo> Packages { get; private set; }

        /// <summary>
        ///     The initialize packages.
        /// </summary>
        private void InitializePackages()
        {
            this.Packages = string.IsNullOrWhiteSpace(this.serializedPackages) ? new ObservableCollection<PackageInfo>() : JsonConvert.DeserializeObject<ObservableCollection<PackageInfo>>(this.serializedPackages);
            this.Packages.CollectionChanged += (sender, args) => { this.serializedPackages = JsonConvert.SerializeObject(this.Packages); };
        }

        /// <summary>
        /// Executes the job
        /// </summary>
        /// <param name="targetInstanceId">
        /// The target instance id.
        /// </param>
        public override void Execute(Guid targetInstanceId)
        {
            this.InitializePackages();

            var packageRequestService = new SolutionPackageRequestService();

            var packageRequests = new List<SolutionPackageRequest>();
            foreach (var packageInfo in this.Packages)
            {
                packageRequests.AddRange(packageRequestService.EnumPackageRequests(packageInfo.Id, new VersionSpec(SemanticVersion.Parse(packageInfo.Version))));
            }

            var path = Path.Combine(Environment.ExpandEnvironmentVariables("%TMP%\\SharePoint.PackageManager"), targetInstanceId.ToString());
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var solutions = SPFarm.Local.Solutions.ToList();
            var solutionPackageRequests = packageRequests.Distinct().SortByDependencies().ToList();

            var i = 0;
            var count = solutionPackageRequests.Count;
            foreach (var packageRequest in solutionPackageRequests)
            {
                var package = packageRequest.Package;
                var solution = solutions.Find(s => s.Name.ToLower().Equals(package.Id.ToLower()));
                if (solution == null || solution.GetVersion().CompareTo(package.Version) != 0)
                {
                    var packageManager = new PackageManager(packageRequest.Repository, path);
                    packageManager.InstallPackage(package, true, true);
                    var solutionFile = Path.Combine(Path.Combine(Path.Combine(path, package.GetFullName().Replace(" ", ".")), "content"), package.Id);
                    if (File.Exists(solutionFile))
                    {
                        if (solution != null)
                        {
                            solution.Upgrade(solutionFile);
                        }
                        else
                        {
                            solution = SPFarm.Local.Solutions.Add(solutionFile);
                        }

                        solution.SetVersion(package.Version);
                    }
                }
            

                this.UpdateProgress(Convert.ToInt32(++i / (count * 1.0d) * 100));
            }
        }
    }
}