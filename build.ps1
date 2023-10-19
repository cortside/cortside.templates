if (Test-Path artifacts) {
	rm -Force -Recurse artifacts
}

dotnet restore .\templates
dotnet pack .\templates\templates.csproj -o .\artifacts\ --no-build

if (Test-Path temp) {
	rm -Force -Recurse temp
}
dotnet new uninstall cortside.templates
dotnet new install .\artifacts\*.nupkg

dotnet new cortside-api --output ./temp/api --name Foo.Bar --company Foo --product Bar
dotnet build ./temp/api/src
dotnet test ./temp/api/src

dotnet new cortside-ids4 --output ./temp/ids4 --name Foo.IdentityServer --company Foo
dotnet build ./temp/ids4/src
dotnet test ./temp/ids4/src

dotnet new cortside-web --output ./temp/web --name Foo.Bar --company Foo --product Bar

if (Test-Path temp) {
	rm -Force -Recurse temp
}
