gci -Recurse -Filter "package.json" | %{ $fn = $_.Name; $full = $_.FullName; cat $_.FullName | convertfrom-json | select dependencies | %{ $d = $_; $d.dependencies | gm -MemberType *property | %{ $dep=$_.name; $val = $d.dependencies.($dep); "$full|$dep|$val"; } }} | 
							sort | 
							uniq | out-file -filepath dependency-references.txt
							
							gci -Recurse -Filter "package.json" | %{ $fn = $_.Name; $full = $_.FullName; cat $_.FullName | convertfrom-json | select dependencies | %{ $d = $_; $d.dependencies | gm -MemberType *property | %{ $dep=$_.name; $val = $d.dependencies.($dep); "$dep|$val"; } }} | 
							sort | 
							uniq | out-file -filepath dependencies.txt