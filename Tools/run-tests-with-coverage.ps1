$SolutionRoot = "$PSScriptRoot\.."
$TestProjects = Get-ChildItem $SolutionRoot\Tests\
$Tfm = "net8.0"
$Configuration = "Debug"
Remove-Item $SolutionRoot\TestResults\coverage.json -ErrorAction Ignore

foreach($project in $TestProjects)
{
    if($project -eq $TestProjects[0])
    {
        # First one, no merging results; nothing to merge with!
        coverlet `
            "$SolutionRoot\Tests\$project\bin\$Configuration\$Tfm\$project.dll" `
            --target "dotnet" `
            --targetargs "test $SolutionRoot\Tests\$project --no-build --logger:nunit --test-adapter-path:." `
            -o="$SolutionRoot\TestResults\coverage.json"
    }
    elseif($project -eq $TestProjects[-1])
    {
        # Last one, merge results and output opencover
        coverlet `
            "$SolutionRoot\Tests\$project\bin\$Configuration\$Tfm\$project.dll" `
            --target "dotnet" `
            --targetargs "test $SolutionRoot\Tests\$project --no-build --logger:nunit --test-adapter-path:." `
            -f=opencover `
            -o="$SolutionRoot\TestResults\coverage.opencover.xml"
    }
    else
    {
        # Neither first nor last, merge results
        coverlet `
            "$SolutionRoot\Tests\$project\bin\$Configuration\$Tfm\$project.dll" `
            --target "dotnet" `
            --targetargs "test Tests\$project --no-build --logger:nunit --test-adapter-path:." `
            --merge-with "$SolutionRoot\TestResults\coverage.json" `
            -o="$SolutionRoot\TestResults\coverage.json"
    }
}

