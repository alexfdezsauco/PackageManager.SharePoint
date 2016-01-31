$buildScriptDir = (Split-Path $MyInvocation.MyCommand.Path)
$VersionNumber = (GitVersion /showvariable NuGetVersionV2)

$workspaceDir = Join-Path $buildScriptDir "..\.."
$srcDir = Join-Path $workspaceDir "src"
$outDir = Join-Path $workspaceDir "output"
$deploymentDir = Join-Path $workspaceDir "deployment"

$projectFile = Join-Path $srcDir "PackageManager.SharePoint\PackageManager.SharePoint.csproj"

$nuspecFileName = "PackageManager.SharePoint.WSP.nuspec"
$nuspecTemplateFile = Join-Path $deploymentDir (Join-Path "NuGet\PackageManager.SharePoint.WSP\" $nuspecFileName)
$nugetPackBaseDir = Join-Path $outDir "nuget-pkg\PackageManager.SharePoint.WSP"
$nugetPackOutDir = Join-Path $outDir "nuget\PackageManager.SharePoint.WSP"
$nuspecOutputFile = Join-Path $nugetPackBaseDir $nuspecFileName

$wspFile = Join-Path $outDir "bin\Debug\PackageManager.SharePoint.wsp"
$wspOutputDir = Join-Path $nugetPackBaseDir "content"

msbuild $projectFile /t:Package 

foreach($dirToRemove in ($nugetPackBaseDir, $nugetPackOutDir))
{
	if(Test-Path $dirToRemove)
	{
		Remove-Item -Path $dirToRemove -Recurse -Force
	}
}

foreach($dirToCreate in ($nugetPackOutDir, $nugetPackBaseDir, $wspOutputDir))
{
	New-Item -Path $dirToCreate -ItemType Directory -Force
}

$xml = New-Object Xml
$xml.Load($nuspecTemplateFile)
$xml.package.metadata.version = $VersionNumber.ToString()
$xml.Save($nuspecOutputFile)

Copy-Item -Path $wspFile -Destination $wspOutputDir

$NuGetExec = Join-Path $srcDir ".nuget\nuget.exe"
& $NuGetExec pack $nuspecOutputFile -BasePath $nugetPackBaseDir -OutputDirectory $nugetPackOutDir