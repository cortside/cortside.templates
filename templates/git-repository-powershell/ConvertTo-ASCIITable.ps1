#04/17/2020 04:35:00 PM - Steve Witt
#-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
#Object to ASCII Table Output Converter
#
#Purpose:
#  Outputs an ASCII table from an input object.
#
#Usage:
#  ConvertTo-ASCIITable -InputObject $MyObject
#   -OR-
#  $MyObject | ConvertTo-ASCIITable
#
#Examples:
#  $CSVTable = @"
#  "ID", "FirstName", "LastName", "Role"
#  1, "Homer", "Simpson",  "Dad"
#  552, "Marge", "Simpson",  "Mom"
#  123, "Bart",  "Simpson",  "Son"
#  3324, "Lisa",  "Simpson",  "Daughter"
#  442225, "Maggie", "Simpson", "Daughter"
#  "@
#  $Family = $CSVTable | ConvertFrom-CSV
#  $Family | ConvertTo-ASCIITable
#
#  OUTPUT:
#  +------------+---------------+--------------+--------------+
#  |         ID | FirstName     | LastName     | Role         |
#  +------------+---------------+--------------+--------------+
#  |          1 | Homer         | Simpson      | Dad          |
#  |        552 | Marge         | Simpson      | Mom          |
#  |        123 | Bart          | Simpson      | Son          |
#  |       3324 | Lisa          | Simpson      | Daughter     |
#  |     442225 | Maggie        | Simpson      | Daughter     |
#  +------------+---------------+--------------+--------------+
#
#  (Get-ChildItem c:\Windows\p*.exe | select FullName, CreationTime, LastAccessTime, LastWriteTime) | ConvertTo-ASCIITable
#  OUTPUT:
#  +-------------------------------+--------------------------+--------------------------+---------------------------+
#  | FullName                      | CreationTime             | LastAccessTime           | LastWriteTime             |
#  +-------------------------------+--------------------------+--------------------------+---------------------------+
#  | C:\Windows\pfm.exe            | 4/30/2018 2:42:51 PM     | 4/30/2018 2:42:51 PM     | 7/11/2014 12:58:17 PM     |
#  | C:\Windows\pfmhost.exe        | 4/30/2018 2:42:51 PM     | 4/30/2018 2:42:51 PM     | 7/11/2014 12:58:20 PM     |
#  | C:\Windows\pfmstat.exe        | 4/30/2018 2:42:51 PM     | 4/30/2018 2:42:51 PM     | 7/11/2014 12:58:21 PM     |
#  | C:\Windows\pfmsyshost.exe     | 4/30/2018 2:42:51 PM     | 4/30/2018 2:42:51 PM     | 7/11/2014 12:58:20 PM     |
#  | C:\Windows\pfolder.exe        | 4/30/2018 2:42:52 PM     | 4/30/2018 2:42:52 PM     | 7/11/2014 1:08:01 PM      |
#  | C:\Windows\pftest.exe         | 4/30/2018 2:42:52 PM     | 4/30/2018 2:42:52 PM     | 7/11/2014 12:58:20 PM     |
#  | C:\Windows\ptramfs.exe        | 4/30/2018 2:42:52 PM     | 4/30/2018 2:42:52 PM     | 7/11/2014 12:58:22 PM     |
#  +-------------------------------+--------------------------+--------------------------+---------------------------+
[cmdletbinding()]
Param(
  [Parameter(Mandatory=$True, ValueFromPipeline=$True)]$InputObject
)

#Accepting pipeline input requires there to be "begin," "process," and "end" blocks.
begin{
  $TempObject = @()
}

#Normally the "process" section would be where the bulk of the code would be, but in my case I was working with an object as an entire entity, and I didn't want to process each individual sub-part of that object. Solution was to store each chunk fed by the "process" block into a temp object, and then dump that temp object back in when finished.
process{
  $TempObject += $InputObject
}

end{
  $InputObject = @()
  $InputObject = ($TempObject | ConvertTo-CSV | ConvertFrom-CSV) #Converts the array to CSV and back to an object -- a poor man's method of coercing all object containers into string values so they'll have a "length" property that we can reference.

  #$FieldNames = $InputObject | Get-Member -Type properties | foreach Name
  $FieldNames = $InputObject | ConvertTo-Csv -NoTypeInformation | Select-Object -First 1
  $FieldNames = $FieldNames -Replace '"', ''
  $FieldNames = ($FieldNames).Split(',',[System.StringSplitOptions]::RemoveEmptyEntries)

  $MaxLength = New-Object pscustomobject
  $FieldJustification = New-Object pscustomobject
  $strHeader=""
  $strHeader+="|"
  foreach ($Field in $FieldNames){
    $MaxTmp = ($InputObject.$Field | Measure-Object -Maximum -Property Length).Maximum
    if ($Field.length -gt $MaxTmp) {$MaxTmp = $Field.length}
    $MaxLength | Add-Member NoteProperty $Field $MaxTmp
    if ($InputObject.$Field -notmatch "^\$*[\d\.]+$"){
      $FieldJustification | Add-Member NoteProperty $Field "Left"
      $strHeader+=" $Field".PadRight($MaxTmp+6)
    } else {
      $FieldJustification | Add-Member NoteProperty $Field "Right"
      $strHeader+="$Field ".PadLeft($MaxTmp+6)
    }
    $strHeader+="|"
  }

  ##Write the header info:
  #Use some fancy regex replacements of the header to create a nice "---------" wrapper for it.
  $strHeaderPad=$strHeader -replace "[^|]", "-" #Regex - replace anything that is not a pipe character with a dash
  $strHeaderPad=$strHeaderPad -replace "\|","+" #Regex - replace the pipe characters with plus signs.
  $strHeaderPad #Print the header top ascii line
  $strHeader #Print the header text
  $strHeaderPad #Print the header bottom ascii line

  #Loop through each record in the input object and print it
  foreach ($Record in $InputObject){
    #Write the detail records:
    $OutString = "|"
    foreach ($Field in $FieldNames){
      if ($FieldJustification.$Field -eq "Left") {
        $OutString += (" {0,-$($MaxLength.$Field+3)}  |" -f $Record.$Field)
      } else {
        $OutString += ("  {0,$($MaxLength.$Field+3)} |" -f $Record.$Field)
      }
    }
    $OutString
  }
  #Close off the table:
  "$strHeaderPad`r`n"  #Print the footer bottom ascii line (which is the same as the header ascii lines)
}
