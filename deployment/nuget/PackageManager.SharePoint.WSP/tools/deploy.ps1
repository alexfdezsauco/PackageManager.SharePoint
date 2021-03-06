$deployScriptDirectory = (Split-Path $MyInvocation.MyCommand.Path)
$solutionName = "PackageManager.SharePoint.wsp"
$solutionFileName = Join-Path $deployScriptDirectory "..\content\$solutionName"

Add-PSSnapin Microsoft.SharePoint.PowerShell

function Get-PackageVersion()
{
	$version = "0.0.0.0"
	$nuspecFile = (Get-ChildItem -Path (Join-Path $deployScriptDirectory "\..\") -Filter "$solutionName.nuspec")
	if($nuspecFile -ne $null)
	{
		$xml = New-Object XML
		$xml.Load($nuspecFile.FullName)
		$version = $xml.package.metadata.version
	}
	
	return $version
}

function Update-SPSolutionVersion()
{
	$version = Get-PackageVersion
	$solution = (Get-SPSolution | Where-Object { $_.Name -eq $solutionName })
	if($solution -ne $null)
	{
		$solution.Properties["Version"] = $version
		$solution.Update()
	}
}

function WaitFor-SPSolutionDeploymentJob()
{
    $job = $null
    do
    {
        $job = Get-SPTimerJob | Where-Object { $_.Name -like "*$solutionName*" }
        if(-not ($job -eq $null))
        {
            Start-Sleep -Seconds 2
        }
    }
    while(-not ($job -eq $null))
}

try
{
	Add-SPSolution -LiteralPath $solutionFileName
	Install-SPSolution $solutionName -GACDeployment -Force
	WaitFor-SPSolutionDeploymentJob 
	Update-SPSolutionVersion
	Enable-SPFeature fb315e1d-f7c1-47ea-a134-02c0a181e3a0 -Url (Get-SPWebApplication -IncludeCentralAdministration | Where-Object { $_.IsAdministrationWebApplication } | Select-Object -ExpandProperty Url)
}
finally
{
	Remove-PSSnapin Microsoft.SharePoint.PowerShell
}