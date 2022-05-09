gci -Recurse -Filter "*.csproj" | %{ $fn = $_.Name; cat $_.FullName | sls PackageReference | %{ $r=$_.ToString().Trim(); 
							$parts = $r.Split('"');
							$p = $parts[1];
							$v = $parts[3];
							"$p|$v|$fn" }} | 
							sort | 
							uniq | out-file -filepath package-references.txt
							
gci -Recurse -Filter "*.csproj" | %{ $fn = $_.Name; cat $_.FullName | sls PackageReference | %{ $r=$_.ToString().Trim(); 
							$parts = $r.Split('"');
							$p = $parts[1];
							$v = $parts[3];
							"$p|$v" }} | 
							sort | 
							uniq | out-file -filepath packages.txt							