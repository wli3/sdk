$repoRoot = (get-item $PSScriptRoot).parent
$allTests = Get-Childitem -Path $repoRoot.GetDirectories("src").GetDirectories("Tests").FullName -Recurse '*.Tests.csproj'

$testsProjectCannotRunOnHelixListPath = $repoRoot.GetDirectories("src").GetDirectories("Tests").GetFiles("testsProjectCannotRunOnHelixList.txt").FullName
$buildshScriptPath = $repoRoot.GetDirectories("eng").GetDirectories("common").GetFiles("build.ps1").FullName

$args = $args
$passin = @("-test")
$passin = $passin  +  $args

foreach($line in ([System.IO.File]::ReadLines($testsProjectCannotRunOnHelixListPath) | Where-Object { $_.Trim() -ne '' }))
{
    foreach ($testProject in $allTests)
    {
        if ($testProject.FullName.Contains($line.Trim())) { 
            $passInArgs = @("-test") + $args + @("-projects", $testProject.FullName)
            Invoke-Expression "& $buildshScriptPath $passInArgs"
        }
    }
}
