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
    return "[{0:MM/dd/yy} {0:HH:mm:ss}]" -f (Get-Date)
}

$dirs = @()
$dirs += dir auth | ?{$_.PSISContainer}
$dirs += dir origination | ?{$_.PSISContainer}
$dirs += dir partner | ?{$_.PSISContainer}
$dirs += dir servicing | ?{$_.PSISContainer}
$dirs += dir shared | ?{$_.PSISContainer}

$root = "$($PSScriptRoot)\"
$dirs | ForEach-Object { $path = $_.FullName.replace($root, ""); echo $path }

$changed = @()
foreach ($dir in $dirs){
	$path = $dir.FullName.replace($root, "")
	echo $path
	echo "=================="
	echo Get-TimeStamp
	echo "repo: $($dir.Name)"
	cd $path
	Invoke-Exe git -args "checkout develop"
	Invoke-Exe git -args "pull"
	
	# generate git fame table
	$pwd = $(pwd).Path.Replace("\", "/")
	echo $pwd
	git-fame -ewMCt --since='3.months' --incl='\.(ts|js|scss|html|json|cs|csproj|config)$' > git-fame.txt	
	cat git-fame.txt	

	echo "------------------"
	echo ""
	echo "=================="
	cd ../..
}

echo ""
echo "******************"
$changed | ForEach-Object {$_}
echo "******************"
echo Get-TimeStamp
