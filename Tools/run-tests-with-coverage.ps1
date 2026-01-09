$SolutionRoot = "$PSScriptRoot\.."
$TestProjects = Get-ChildItem -Path $SolutionRoot\Tests\ -Exclude CSF.Screenplay.Selenium.TestWebapp
$Tfm = "net8.0"
$Configuration = "Debug"
Remove-Item $SolutionRoot\TestResults\* -ErrorAction Ignore
$TetsFailed = 0

foreach($project in $TestProjects)
{
    $projectName = Split-Path $project -Leaf
    $projectAssembly = "$project\bin\$Configuration\$Tfm\$projectName.dll"
    coverlet `
        "$projectAssembly" `
        --target "dotnet" `
        --targetargs "test $project --no-build --logger:nunit --test-adapter-path:." `
        -f=opencover `
        -o="$SolutionRoot\TestResults\$projectName.opencover.xml"

    if ($LastExitCode -eq 1) {
        $TetsFailed = 1
    }
    elseif ($LastExitCode -eq 3) {
        $TetsFailed = 1
    }
}

$env:TESTS_FAILED = $TetsFailed
