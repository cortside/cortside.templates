cat .\changelog.txt | grep "^|" | grep -v ":--" | grep -v distribution | grep -v "|----" | sort | tr A-Z a-z | out-file .\fame.txt -encoding utf8

((Get-Content -path fame.txt -Raw) -replace 'cort@eschaefer.net','cschaefer@cortsideusa.com') | Set-Content -Path fame.txt -encoding utf8
((Get-Content -path fame.txt -Raw) -replace 'mbrichards@cortside.local','mbrichards@cortsideusa.com') | Set-Content -Path fame.txt -encoding utf8
((Get-Content -path fame.txt -Raw) -replace 'mauricioher.94@hotmail.com','maraica@cortsideusa.com') | Set-Content -Path fame.txt -encoding utf8

(gc fame.txt) | ? {$_.trim() -ne "" } | set-content fame.txt

$tloc=0
$tcom=0
$tfil=0
Get-Content .\fame.txt | ?{$tloc += $_.split('|')[2]; $tcom+=$_.split('|')[3];$tfil+=$_.split('|')[4];}


$data = (get-content .\fame.txt) -replace " *\| *", "|" | ConvertFrom-Csv -Delimiter "|" -Header f0,user,f1,f2,f3,f4
$data | Group-Object -Property user | Select-Object -Unique -Property `
  Name, `
  @{Label = "loc";Expression={($PSItem.group | Measure-Object -Property f1 -sum).sum}}, `
  @{Label = "com";Expression={($PSItem.group | Measure-Object -Property f2 -sum).sum}}, `
  @{Label = "fil";Expression={($PSItem.group | Measure-Object -Property f3 -sum).sum}}, `
  @{Label = "loc%";Expression={(($PSItem.group | Measure-Object -Property f1 -sum).sum / $tloc).ToString("P")}}, `
  @{Label = "com%";Expression={(($PSItem.group | Measure-Object -Property f2 -sum).sum / $tcom).ToString("P")}}, `
  @{Label = "fil%";Expression={(($PSItem.group | Measure-Object -Property f3 -sum).sum / $tfil).ToString("P")}} `
  | sort-object -property loc -descending | ./ConvertTo-ASCIITable.ps1
  
  
exit 0  






cat fame.txt | sed 's/\s//g' | tr A-Z a-z | cut -d '|' -f 2 | sort | uniq > authors.txt


#for i in `cat fame.txt | sed 's/\s//g' | tr A-Z a-z | cut -d '|' -f 2 | sort | uniq`; do printf $i; grep -i $i fame.txt | awk -F '|' '{ sum1+=$3; sum2+=$4; sum3+=$5 }END{ print "  | " sum1 " |   " sum2 " |   " sum3 " | " }'; done

echo "" > stats.txt

foreach($line in Get-Content .\authors.txt) {
	$stats = (grep -i $line fame.txt | awk -F '|' '{ sum1+=$3; sum2+=$4; sum3+=$5 }END{ print \"  | \" sum1 \" |   \" sum2 \" |   \" sum3 \" | \" }')
	echo "$line $stats" | out-file stats.txt -append
    #grep -i $line fame.txt | awk -F '|' '{ sum1+=$3; sum2+=$4; sum3+=$5 }END{ print \"  | \" sum1 \" |   \" sum2 \" |   \" sum3 \" | \" }';
}


#cat fame.txt | sort | tr A-Z a-z; cat fame.txt | awk -F '|' '{ sum1+=$3; sum2+=$4; sum3+=$5 }END{ print \"Totals:                      | \" sum1 \" |   \" sum2 \" |   \" sum3 \" | \" }'

(gc stats.txt) | ? {$_.trim() -ne "" } | set-content stats.txt
$tloc=0
$tcom=0
$tfil=0
Get-Content .\stats.txt | ?{$tloc += $_.split('|')[1]; $tcom+=$_.split('|')[2];$tfil+=$_.split('|')[3];}
Get-Content .\stats.txt | Sort-Object {[int]$_.split('|')[1] } -Descending | set-content stats.txt

echo "author | loc | com | fil | loc% | com% | fil%"
foreach($line in Get-Content .\stats.txt) {
	$loc = ([int]$line.split('|')[1])
	$com = ([int]$line.split('|')[2])
	$fil = ([int]$line.split('|')[3])
	
	$ploc = "{0:N2}" -f (($loc/$tloc)*100)
	$pcom = "{0:N2}" -f (($com/$tcom)*100)
	$pfil = "{0:N2}" -f (($fil/$tfil)*100)
	
	echo "$line $ploc% | $pcom% | $pfil%"
}

echo "total | $tloc | $tcom | $tfil"





