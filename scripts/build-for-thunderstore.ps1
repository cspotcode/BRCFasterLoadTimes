#Requires -Version 7.4
$ErrorActionPreference = 'Stop'
$PSNativeCommandUseErrorActionPreference = $true

# Release build
dotnet clean -c Release
dotnet build -c Release

# Zip up your dll, icon, readme, and manifest
$dll = $( dotnet msbuild -property:Configuration=Release -t:LogTargetPath -nologo ).trim()
Compress-Archive -Force -DestinationPath ./thunderstore-upload.zip -Path manifest.json,$dll,README.md,icon.png