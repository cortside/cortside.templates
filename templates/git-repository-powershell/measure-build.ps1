$ErrorActionPreference = "Stop"

Function check-result {
    if ($LastExitCode -ne 0) { Invoke-BuildError "ERROR: Exiting with error code $LastExitCode"	}
    return $true
}

Function Invoke-BuildError {
Param(
	[parameter(Mandatory=$true)][string] $text
)
	if ($env:TEAMCITY_VERSION) {
        Write-Error "##teamcity[message text='$text']" # so number of failed tests shows in builds list instead of this text
		Write-Error "##teamcity[buildStatus status='ERROR']"
		[System.Environment]::Exit(1) 
	} else {
        Write-Error $text 
        exit
	}
}

Function Invoke-Exe {
    Param(
        [parameter(Mandatory = $true)][string] $cmd,
        [parameter(Mandatory = $true)][string] $args
	
    )
    Write-Output "Executing: `"$cmd`" --% $args"
    & "$cmd" "--%" "$args";
    $result = check-result
    if (!$result) {
        throw "ERROR executing EXE"
        exit 1
    }
}

function Get-TimeStamp {
    $timestamp = "[{0:MM/dd/yy} {0:HH:mm:ss}]" -f (Get-Date)
	echo $timestamp
}

$dirs = @()
$dirs += dir auth | ?{$_.PSISContainer}
$dirs += dir origination | ?{$_.PSISContainer}
$dirs += dir partner | ?{$_.PSISContainer}
$dirs += dir servicing | ?{$_.PSISContainer}
$dirs += dir shared | ?{$_.PSISContainer}
$dirs += dir other | ?{$_.PSISContainer}
$dirs += dir cortside | ?{$_.PSISContainer}
$dirs += dir tools | ?{$_.PSISContainer}

$root = "$($PSScriptRoot)\"

$changed = @()
$releases = @()
$unmerged_releases = @()

# turn off firewall so that tests don't get stopped
netsh advfirewall set allprofiles state off

echo "=================="

foreach ($dir in $dirs){
	$path = $dir.FullName.replace($root, "")
	echo $path
	echo "=================="
	Get-TimeStamp
	echo "repo: $($dir.Name)"
	cd $path
	#Invoke-Exe git -args "checkout develop"
	#Invoke-Exe git -args "pull"

	#$exists = (Test-Path -path "build.ps1") -or (Test-Path -path "src/*.sln")
	$exists = Test-Path -path "src/*.sln"

	# clean before build
	if ($exists) {
		$measure = measure-command { C:\work\clean\servicing\rebate-api\clean.ps1 }	
		echo "clean: $($measure.TotalSeconds)"
	}

	# build api
	if ($exists) {
		$measure = measure-command { dotnet build src }	
		echo "build: $($measure.TotalSeconds)"
	}

	# test api
	if ($exists) {
		$measure = measure-command { dotnet test src }	
		echo "tests: $($measure.TotalSeconds)"
	}
	
	# clean to free disk after
	if ($exists) {
		$measure = measure-command { C:\work\clean\servicing\rebate-api\clean.ps1 }	
		echo "clean: $($measure.TotalSeconds)"
	}
	
	echo "------------------"
	#git --no-pager log --pretty=format:'%Cred%h%Creset -%C(yellow)%d%Creset %s %Cgreen(%cr) %C(bold blue)<%an>%Creset' --abbrev-commit master.. #| out-file -filepath changelog.txt -append
	
	echo "------------------"
	echo ""
	echo "=================="
	cd ../..
}

Get-TimeStamp

