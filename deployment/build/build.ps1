$buildScriptDir = (Split-Path $MyInvocation.MyCommand.Path)
$versionNumber = (GitVersion /showvariable NuGetVersionV2)

$workspaceDir = Join-Path $buildScriptDir "..\.."
$srcDir = Join-Path $workspaceDir "src"
$outDir = Join-Path $workspaceDir "output"
$deploymentDir = Join-Path $workspaceDir "deployment"

$projectFile = Join-Path $srcDir "PackageManager.SharePoint\PackageManager.SharePoint.csproj"

$nuspecFileName = "PackageManager.SharePoint.WSP.nuspec"
$nuspecTemplateFile = Join-Path $deploymentDir (Join-Path "NuGet\PackageManager.SharePoint.WSP\" $nuspecFileName)
$toolsTemplateDir = Join-Path $deploymentDir "NuGet\PackageManager.SharePoint.WSP\tools"
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

$nuspecXml = New-Object System.Xml.XmlDocument
$nuspecXml.Load($nuspecTemplateFile)
$nuspecXml.package.metadata.version = $versionNumber.ToString()
$nuspecXml.Save($nuspecOutputFile)

Copy-Item -Path $wspFile -Destination $wspOutputDir
Copy-Item -Path $toolsTemplateDir -Filter *.ps1 -Destination $nugetPackBaseDir -Recurse

$NuGetExec = Join-Path $srcDir ".nuget\nuget.exe"
& $NuGetExec pack $nuspecOutputFile -BasePath $nugetPackBaseDir -OutputDirectory $nugetPackOutDir