param(
    [string]
    $srcDir = $Env:BUILD_SOURCESDIRECTORY
)

Write-Host "Getting major, minor, and revision from git. Calculate build number as Julian date."
# get major, minor, revision, commitID, branch name, from git
$currentLocation = Get-Location
Set-Location -Path $srcDir

$gitCommand = git describe --tags --long
$isMatch = $gitCommand -match "v(\d{1,2})\.(\d{1,2})-(\d+)-(\w+)"
$major = $matches[1]
$minor = $matches[2]
$revision = $matches[3]
$commitID = $matches[4]

$gitCommand = git status --branch --short
$isMatch = $gitCommand -match "##\s+([a-zA-Z0-9]+)\.\.\..*"
$branchName = $matches[1]

Set-Location $currentLocation
Write-Host "major="$major
Write-Host "minor="$revision
Write-Host "revision="$revision
Write-Host "commitID="$commitID
Write-Host "branch name="$branchName

# get Julian Date 
$year = Get-Date -format yy
$julianYear = $year.Substring(0)
$dayOfYear = (Get-Date).DayofYear
$julianDate = $julianYear + "{0:D3}" -f $dayOfYear
Write-Host "Julian Date="$julianDate

# update ProductInfo.cs
$file = Join-Path $srcDir "Agridea.ProductInfo\ProductInfo.cs"
$version = "$major.$minor.0.0"
$fileVersion = "$major.$minor.$julianDate.$revision"
$InformationalVersion = "$major.$minor $branchName $commitID"
$buildDate = Get-Date -format "yyyy.MM.dd HH:mm:ss"
Write-Host "Updating $file with ..."
Write-Host "version="$fileVersion ","
Write-Host "file version="$fileVersion ","
Write-Host "informational version="$InformationalVersion ","
Write-Host "build date="$buildDate

(Get-Content $file) |
%{$_ -replace 'Version\s+=\s+"([0-9\.]*)"', "Version = ""$fileVersion""" } |
%{$_ -replace 'FileVersion\s+=\s+"([0-9\.]*)"', "FileVersion = ""$fileVersion""" } |
%{$_ -replace 'InformationalVersion\s+=\s+"([0-9a-zA-Z\.\s]*)"', "InformationalVersion = ""$InformationalVersion""" } |
%{$_ -replace 'BuildDate\s+=\s+"([0-9\.\s:]*)"', "BuildDate = ""$buildDate""" } |
Set-Content $file -Force
