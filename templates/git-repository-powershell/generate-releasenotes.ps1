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
#$dirs | ForEach-Object { $path = $_.FullName.replace($root, ""); echo $path }

$changed = @()
$releases = @()
$unmerged_releases = @()
foreach ($dir in $dirs){
	$path = $dir.FullName.replace($root, "")
	echo $path
	echo "=================="
	Get-TimeStamp
	echo "repo: $($dir.Name)"
	cd $path
	Invoke-Exe git -args "checkout develop"
	Invoke-Exe git -args "pull"
	
	Invoke-Exe git -args "fetch --prune"
	Invoke-Exe git -args "remote prune origin"
	
	$releaseBranch = (git branch -r --sort=-committerdate | sls "release/" | select-object -first 1)
	$rbranch = $null
	$rcommits = $null
	if ($releaseBranch) { 
		$rb = $releaseBranch.Line.Trim()
		$rb = $rb.replace("origin/", "")
		Invoke-Exe git -args "checkout $rb"
		Invoke-Exe git -args "pull"	
	
		Invoke-Exe git -args "checkout develop"
		Invoke-Exe git -args "pull"

		$commits = (git log --pretty=oneline origin/master..$($releaseBranch.Line.Trim()) | wc -l)
		echo "last release branch: $releaseBranch with $commits commits not merged to master"
		if ($commits -gt 0) {
			$unmerged_releases += "repo: $path -- last release branch: $releaseBranch with $commits commits not merged to master"
			$rbranch = $releaseBranch
			$rcommits = $commits		

			 $commits = (git --no-pager log --pretty=format:'| %h | <span style="white-space:nowrap;">%ad</span> | <span style="white-space:nowrap;">%aN</span> | %d %s' --date=short master.. | tac)

			 "" | Out-File ${PSScriptRoot}\RELEASENOTES.md -Encoding utf8 -Append
			 "# $($dir.Name) - $releaseBranch" | Out-File ${PSScriptRoot}\RELEASENOTES.md -Encoding utf8 -Append
			 "" | Out-File ${PSScriptRoot}\RELEASENOTES.md -Encoding utf8 -Append
			 "|Commit|Date|Author|Message|" | Out-File ${PSScriptRoot}\RELEASENOTES.md -Encoding utf8 -Append
			 "|---|---|---|---|" | Out-File ${PSScriptRoot}\RELEASENOTES.md -Encoding utf8 -Append
			 $commits | Out-File ${PSScriptRoot}\RELEASENOTES.md -Encoding utf8 -Append
			 "****" | Out-File ${PSScriptRoot}\RELEASENOTES.md -Encoding utf8 -Append
			 "" | Out-File ${PSScriptRoot}\RELEASENOTES.md -Encoding utf8 -Append				
		}
		$commits = (git log --pretty=oneline origin/develop..$($releaseBranch.Line.Trim()) | wc -l)
		echo "last release branch: $releaseBranch with $commits commits not merged to develop"
		if ($commits -gt 0) {
			$releases += "repo: $path -- last release branch: $releaseBranch with $commits commits not merged to develop"
		}
	}
	
	echo "------------------"
	#git --no-pager log --pretty=format:'%Cred%h%Creset -%C(yellow)%d%Creset %s %Cgreen(%cr) %C(bold blue)<%an>%Creset' --abbrev-commit master.. #| out-file -filepath changelog.txt -append
	
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

