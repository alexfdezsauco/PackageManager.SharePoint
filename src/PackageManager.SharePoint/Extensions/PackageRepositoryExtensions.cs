namespace PackageManager.SharePoint.Extensions
{
    using System.Linq;

    using NuGet;

    using Constants = PackageManager.SharePoint.Constants;

    public static class PackageRepositoryExtensions
    {
        /// <summary>
        ///     The initialize solution packages query.
        /// </summary>
        /// <param name="this">
        ///     The package repository.
        /// </param>
        /// <returns>
        ///     The <see cref="IQueryable" />.
        /// </returns>
        public static IQueryable<IPackage> QueryForAbsoluteLatestVersionOfSolutionPackages(this IPackageRepository @this)
        {
            IQueryable<IPackage> solutionPackages = null;
            try
            {
                solutionPackages = @this.GetPackages().Where(package => package.Id.ToLower().EndsWith(Constants.SolutionPackagePostFix) && package.IsAbsoluteLatestVersion);
            }
            catch
            {
            }

            return solutionPackages;
        }
    }
}