./clean.ps1

./templates/api-powershell/convert-encoding.ps1 -filePaths ((gci *.ps1 -Recurse) | % { $_.FullName })
./templates/api-powershell/convert-encoding.ps1 -filePaths ((gci *.cs -Recurse) | % { $_.FullName })
./templates/api-powershell/convert-encoding.ps1 -filePaths ((gci *.csproj -Recurse) | % { $_.FullName })
./templates/api-powershell/convert-encoding.ps1 -filePaths ((gci *.json -Recurse) | % { $_.FullName })
./templates/api-powershell/convert-encoding.ps1 -filePaths ((gci *.sln -Recurse) | % { $_.FullName })
./templates/api-powershell/convert-encoding.ps1 -filePaths ((gci *.sql -Recurse) | % { $_.FullName })

git status
