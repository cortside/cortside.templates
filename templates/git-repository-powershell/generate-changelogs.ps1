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
$dirs += dir tools | ?{$_.PSISContainer}
$dirs += dir cortside | ?{$_.PSISContainer}

$root = "$($PSScriptRoot)\"
$dirs | ForEach-Object { $path = $_.FullName.replace($root, ""); echo $path }

$changed = @()
$releases = @()
$unmerged_releases = @()
foreach ($dir in $dirs){
	$mergeRepo = $false
	if ($dir.Name -ne "partnerportal.mobile" -and $dir.Name -ne "onlineapplication.mobile" -and $dir.Name -ne "cortside.domainevent") {
		$mergeRepo = $true
	}

	$path = $dir.FullName.replace($root, "")
	echo $path
	echo "=================="
	Get-TimeStamp
	echo "repo: $($dir.Name)  [ merge=$($mergeRepo) ]"
	cd $path
	Invoke-Exe git -args "checkout master"
	Invoke-Exe git -args "pull"
	
	Invoke-Exe git -args "fetch --prune"
	Invoke-Exe git -args "remote prune origin"
	
	Invoke-Exe git -args "checkout develop"
	Invoke-Exe git -args "pull"
	if ($mergeRepo) {
#		echo "merge master to develop"
#		Invoke-Exe -cmd git -args "merge master"
#		Invoke-Exe -cmd git -args "push"	
	}
	
	if (Test-Path -path "cleanup.ps1") {
		& ./cleanup.ps1
	}
	if (Test-Path -path "clean.ps1") {
		& ./clean.ps1
	}
	
	$releaseBranch = (git branch -r --sort=-committerdate | sls "release/" | select-object -first 1)
	$rbranch = $null
	$rcommits = $null
	if ($releaseBranch) { 
		$rb = $releaseBranch.Line.Trim()
		$rb = $rb.replace("origin/", "")
		Invoke-Exe git -args "checkout $rb"
		Invoke-Exe git -args "pull"	
		
		if ($mergeRepo) {
#			echo "merge master to release"
#			Invoke-Exe -cmd git -args "merge master"
#			Invoke-Exe -cmd git -args "push"		
		}
		
		Invoke-Exe git -args "checkout develop"
		Invoke-Exe git -args "pull"
		if ($mergeRepo) {
#			echo "merge release to develop"
#			Invoke-Exe -cmd git -args "merge $rb"
#			Invoke-Exe -cmd git -args "push"	
		}

		$commits = (git log --pretty=oneline origin/master..$($releaseBranch.Line.Trim()) | wc -l)
		echo "last release branch: $releaseBranch with $commits commits not merged to master"
		if ($commits -gt 0) {
			$unmerged_releases += "repo: $path -- last release branch: $releaseBranch with $commits commits not merged to master"
			$rbranch = $releaseBranch
			$rcommits = $commits
		}
		$commits = (git log --pretty=oneline origin/develop..$($releaseBranch.Line.Trim()) | wc -l)
		echo "last release branch: $releaseBranch with $commits commits not merged to develop"
		if ($commits -gt 0) {
			$releases += "repo: $path -- last release branch: $releaseBranch with $commits commits not merged to develop"
		}
	}
	
	echo "------------------"
	git --no-pager log --pretty=format:'%Cred%h%Creset -%C(yellow)%d%Creset %s %Cgreen(%cr) %C(bold blue)<%an>%Creset' --abbrev-commit master.. #| out-file -filepath changelog.txt -append
	echo "------------------"
	git --no-pager diff master.. --name-only
	
	$bcommits = (git --no-pager log --pretty=oneline origin/master.. | wc -l)
	$files = (git --no-pager diff master.. --name-only | grep -v package-lock.json | grep -v package.json | grep -v version.json | wc -l)
	if ($files -gt "0") {
		$s = "repo: $path with $files files changed in $bcommits commits"
		if ($rbranch)  {
			$s += " (release branch $releaseBranch has $rcommits commits)"
		}
		$changed += $s	
	}	
	
	echo "------------------"
	echo ""
	echo "=================="
	cd ../..
}

echo ""
echo "repo changes"
echo "******************"
$changed | ForEach-Object {$_}
echo "******************"


echo ""
echo "unmerged release branches"
echo "******************"
$unmerged_releases | ForEach-Object {$_}
echo "******************"

echo ""
echo "release branches ahead of develop"
echo "******************"
$releases | ForEach-Object {$_}
echo "******************"

Get-TimeStamp

