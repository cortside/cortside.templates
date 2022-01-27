dotnet pack .\templates\templates.csproj -o .\artifacts\ --no-build

#dotnet nuget push artifacts\**\*.nupkg -s "https://www.myget.org/F/{youraccount}/api/v2/package" -k {yourkey}
