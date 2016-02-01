# PackageManager.SharePoint

The SharePoint Package Manager is a NuGet-based distribution and deployment system for SharePoint's solutions.

Basically, it's an extension of the SharePoint Central Administration site that automates the process for install or update solution packages from package sources (regular NuGet repositories).

![SharePoint Package Manager](http://4.bp.blogspot.com/-oIucFUIhgdU/VqubL5JbmtI/AAAAAAAAAlk/-_0Tks4O_UE/s1600/2016-01-29_120155.png)

## Why NuGet?

Why not? NuGet is the widely accepted and popular package manager among .net developers.

## What is a solution package?

A solution package is a regular NuGet package with its name ending in '.wsp' and with following structure:

![Solution Package Structure](http://2.bp.blogspot.com/-1aLHisqszN0/VqvSJ5BFjeI/AAAAAAAAAmc/jrSXWSHdduU/s1600/2016-01-29_155531.png)

Notice how the folder 'content' contains a wsp file with the same name of the solution package and that’s it.

A solution package can also have declared dependencies, but only between solution packages.

Maybe the naming convention looks weak, but there is only one package in the gallery with name Digst.OioIdws.Wsp, so, for me, is enough. Eventually, the package manager could also track non-solution packages through an ignore list.

## How install?

1) Download the raw [PackageManager.SharePoint.WSP.nupkg](https://www.nuget.org/packages/PackageManager.SharePoint.WSP) from NuGet Gallery.

2) Unpack the nupkg.

3) Open a PowerShell terminal and run:

	> cd %UNPACK_DIR%
	> .\tools\deploy.ps1

## How update?	

SharePoint Package Manager can be updated through the  SharePoint Package Manager itself.