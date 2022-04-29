#Docker variables
$version = "1.0-local"
$dotnetsdk = "6.0"
$dotnetframework = "6.0"

#Build variables
$projectname = "Acme.ShoppingCart" # needed for sln files
$image = $projectname.ToLower() # needs to be lowercase
$acr = "cortside.azurecr.io"
$v = "${version}"

#SonarQube
$sonarscannerversion = "5.4.1"
$localsonartoken = "7945ad74cb74c64ad4257de571fec7720495bf13"
$localsonarhost = "http://10.105.10.19:9000"
