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
$dirs | ForEach-Object { $path = $_.FullName.replace($root, ""); echo $path }

$changed = @()
$releases = @()
$unmerged_releases = @()
foreach ($dir in $dirs){
	if ($dir -Match "cortside") {
		start-sleep -seconds 15
	}
	$path = $dir.FullName.replace($root, "")
	echo $path
	echo "=================="
	Get-TimeStamp
	echo "repo: $($dir.Name)"
	cd $path
	Invoke-Exe git -args "remote prune origin"
	Invoke-Exe git -args "checkout master"
	Invoke-Exe git -args "pull"
	Invoke-Exe git -args "remote prune origin"
	Invoke-Exe git -args "checkout develop"
	Invoke-Exe git -args "pull"
	if ($dir.Name -ne "xdatawarehouse-api") {
		Invoke-Exe -cmd git -args "merge master"
		Invoke-Exe -cmd git -args "push"	
	}	
	
	$releaseBranch = (git branch -r --sort=-committerdate | sls "release/" | select-object -first 1)
	$rbranch = $null
	if ($releaseBranch) { 
		$commits = (git log --pretty=oneline origin/master..$($releaseBranch.Line.Trim()) | wc -l)	
		if ($commits -gt 0) {
			echo "last release branch: $releaseBranch with $commits commits not merged to master"
			$rbranch = $releaseBranch
		}
	}
	
	if ($dir.Name -ne "xpartnerportal-api") {
	if ($rbranch) {
		$rbranch = $rbranch.ToString().Replace("origin/", "")
		Invoke-Exe git -args "checkout $rbranch"
		Invoke-Exe git -args "pull"
		Invoke-Exe -cmd git -args "merge master"
		Invoke-Exe -cmd git -args "push"
		Invoke-Exe git -args "checkout develop"
		Invoke-Exe git -args "pull"
		Invoke-Exe -cmd git -args "merge $rbranch"
		Invoke-Exe -cmd git -args "push"		
	}
	}
	
	echo "------------------"
	echo ""
	echo "=================="
	cd ../..
}
Get-TimeStamp

