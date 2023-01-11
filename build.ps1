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
dotnet new cortside-web --output ./temp/web --name Foo.Bar --company Foo --product Bar

dotnet build ./temp/api/src
dotnet test ./temp/api/src

if (Test-Path temp) {
	rm -Force -Recurse temp
}
