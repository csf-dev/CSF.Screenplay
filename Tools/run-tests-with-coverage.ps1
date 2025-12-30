$SolutionRoot = "$PSScriptRoot\.."
$TestProjects = Get-ChildItem $SolutionRoot\Tests\ -Exclude CSF.Screenplay.Selenium.TestWebapp
$Tfm = "net8.0"
$Configuration = "Debug"
Remove-Item $SolutionRoot\TestResults\* -ErrorAction Ignore
$PlannedExitCode = 0

foreach($project in $TestProjects)
{
    coverlet `
        "$SolutionRoot\Tests\$project\bin\$Configuration\$Tfm\$project.dll" `
        --target "dotnet" `
        --targetargs "test $SolutionRoot\Tests\$project --no-build --logger:nunit --test-adapter-path:." `
        -f=opencover `
        -o="$SolutionRoot\TestResults\$project.opencover.xml"

    if ($LastExitCode -eq 1) {
        $PlannedExitCode = 1
    }
    elseif ($LastExitCode -eq 3) {
        $PlannedExitCode = 1
    }
}

exit $PlannedExitCode
