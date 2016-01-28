namespace PackageManager.SharePoint.Services.Interfaces
{
    /// <summary>
    /// </summary>
    public interface ISolutionPackageInstallerService
    {
        /// <summary>
        /// </summary>
        void UpdateAll();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packageId"></param>
        /// <param name="version"></param>
        /// <param name="commandName"></param>
        void InstallOrUpdate(string packageId, string version, string commandName);
    }
}