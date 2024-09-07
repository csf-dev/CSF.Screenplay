$SolutionRoot = "$PSScriptRoot\.."
$TestProjects = Get-ChildItem $SolutionRoot\Tests\
$Tfm = "net8.0"
$Configuration = "Debug"
Remove-Item $SolutionRoot\TestResults\coverage.json -ErrorAction Ignore

$IsFirst = $true

foreach($project in $TestProjects)
{
    if($IsFirst)
    {
        $IsFirst = $false
        coverlet `
            "$SolutionRoot\Tests\$project\bin\$Configuration\$Tfm\$project.dll" `
            --target "dotnet" `
            --targetargs "test $SolutionRoot\Tests\$project --no-build --logger:nunit --test-adapter-path:." `
            -o="$SolutionRoot\TestResults\coverage.json"
    }
    else
    {
        coverlet `
            "$SolutionRoot\Tests\$project\bin\$Configuration\$Tfm\$project.dll" `
            --target "dotnet" `
            --targetargs "test Tests\$project --no-build --logger:nunit --test-adapter-path:." `
            --merge-with "$SolutionRoot\TestResults\coverage.json"
    }
}
