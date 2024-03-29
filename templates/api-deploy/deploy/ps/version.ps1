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
$sonarscannerversion = "5.5.3"
$sonarkey = "acme_acme.shoppingcart"
$localsonartoken = "sometoken"
$localsonarhost = "somehost"
