$SolutionRoot = "$PSScriptRoot\.."
$TestProjects = Get-ChildItem $SolutionRoot\Tests\
$Tfm = "net8.0"
$Configuration = "Debug"
Remove-Item $SolutionRoot\TestResults\* -ErrorAction Ignore

foreach($project in $TestProjects)
{
    coverlet `
        "$SolutionRoot\Tests\$project\bin\$Configuration\$Tfm\$project.dll" `
        --target "dotnet" `
        --targetargs "test $SolutionRoot\Tests\$project --no-build --logger:nunit --test-adapter-path:." `
        -f=opencover `
        -o="$SolutionRoot\TestResults\$project.opencover.xml"
}

