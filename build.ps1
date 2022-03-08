rm -Force -Recurse artifacts

dotnet restore .\templates
dotnet pack .\templates\templates.csproj -o .\artifacts\ --no-build

rm -Force -Recurse temp
dotnet new --uninstall cortside.templates
dotnet new --install .\artifacts\*.nupkg

dotnet new cortside-api --output ./temp --name Foo.Bar

dotnet build ./temp/src
dotnet test ./temp/src

rm -Force -Recurse temp
