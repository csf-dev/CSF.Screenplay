# Taken from https://www.appveyor.com/docs/running-tests/#uploading-xml-test-results

$SolutionRoot = "$PSScriptRoot\.."
$TestProjects = Get-ChildItem $SolutionRoot\Tests\

$wc = New-Object 'System.Net.WebClient'

foreach($project in $TestProjects)
{
    $wc.UploadFile("https://ci.appveyor.com/api/testresults/nunit3/$env:APPVEYOR_JOB_ID", (Resolve-Path $SolutionRoot\Tests\$project\TestResults\TestResults.xml))
}
