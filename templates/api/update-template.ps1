# make sure latest version of cortside.templates is installed
dotnet new --install cortside.templates

# update powershell scripts from root
dotnet new cortside-api-powershell --force --name Acme.ShoppingCart --company Acme --product ShoppingCart

# update .editorconfig
dotnet new cortside-api-editorconfig --force --name Acme.ShoppingCart --company Acme --product ShoppingCart

# update dockerfile and supporting shell and powershell scripts
if (Test-Path -path "deploy") {
	dotnet new cortside-api-deploy --force --name Acme.ShoppingCart --company Acme --product ShoppingCart
}
