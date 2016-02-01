Add-PSSnapin Microsoft.SharePoint.PowerShell
$solutionName = "PackageManager.SharePoint.wsp"

try
{
	Add-PSSnapin Microsoft.SharePoint.PowerShell
	Disable-SPFeature fb315e1d-f7c1-47ea-a134-02c0a181e3a0 -Url (Get-SPWebApplication -IncludeCentralAdministration | Where-Object { $_.IsAdministrationWebApplication } | Select-Object -ExpandProperty Url)
	Remove-SPSolution packagemanager.sharepoint.wsp -Force
}
finally
{
	Remove-PSSnapin Microsoft.SharePoint.PowerShell
}
