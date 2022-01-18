dotnet pack .\templatepack.csproj -o .\artifacts\

#dotnet nuget push artifacts\**\*.nupkg -s "https://www.myget.org/F/{youraccount}/api/v2/package" -k {yourkey}
