[cmdletBinding()]
Param()

Push-Location "$PSScriptRoot/src/Acme.WebApiStarter.Api.WebApi"

cmd /c start cmd /k "title Rebate.Api Api & dotnet run"

Pop-Location
