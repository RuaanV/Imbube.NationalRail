properties {
	$productVersion = "1.0"
	$buildNumber = "0";
	$versionFile = ".\version.txt"
	$product = "Imbube - National Rail App"
	$company = "Imbube"
	
}

task default -depends Setup

task  Setup -depends InstallDependentPackages, GenerateCommonAssemblyInfo, BuildOnNet40{}
 
task BuildOnNet40 {
.\build.bat
 } 
 
 task InstallDependentPackages {
 	dir -recurse -include ('packages.config') |ForEach-Object {
	$packageconfig = [io.path]::Combine($_.directory,$_.name)

	write-host $packageconfig 

	.\tools\NuGet\NuGet.exe install $packageconfig -o packages 
	}
 }
 
 task GenerateCommonAssemblyInfo {
    $versionFileFullPath = Resolve-Path $versionFile
    $productVersion = Get-Content $versionFileFullPath;
	$buildNumber = 0
	if($env:BUILD_NUMBER -ne $null) {
    	$buildNumber = $env:BUILD_NUMBER
	}
	
	Write-Output "Build Number: $buildNumber"
	
	$productVersion = $productVersion + "." + $buildNumber
 	Write-Output "File Version: $versionFile FullPath: $versionFileFullPath"
Generate-Assembly-Info true "release" "Windows Phone National Rail Tracking App by Imbube" $company $company "Copyright © Imbube 20012-2012" $productVersion $productVersion ".\CommonAssemblyInfo.cs" 
 }
 
 function Generate-Assembly-Info
{
param(
	[string]$clsCompliant = "true",
	[string]$configuration, 
	[string]$description, 
	[string]$company, 
	[string]$product, 
	[string]$copyright, 
	[string]$version,
	[string]$fileVersion,
	[string]$file = $(throw "file is a required parameter.")
)
  $asmInfo = "using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

[assembly: AssemblyVersionAttribute(""$version"")]
[assembly: AssemblyFileVersionAttribute(""$fileVersion"")]
[assembly: AssemblyCopyrightAttribute(""$copyright"")]
[assembly: AssemblyProductAttribute(""$product"")]
[assembly: AssemblyCompanyAttribute(""$company"")]
[assembly: AssemblyConfigurationAttribute(""$configuration"")]
[assembly: AssemblyInformationalVersionAttribute(""$fileVersion"")]
#if NET35
[assembly: AllowPartiallyTrustedCallersAttribute()]
#endif
[assembly: ComVisibleAttribute(false)]
[assembly: CLSCompliantAttribute(true)]
"

	$dir = [System.IO.Path]::GetDirectoryName($file)
	
	if ([System.IO.Directory]::Exists($dir) -eq $false)
	{
		Write-Host "Creating directory $dir"
		[System.IO.Directory]::CreateDirectory($dir)
	}
	Write-Host "Generating assembly info file: $file"
	Write-Output $asmInfo > $file
}
 