# Adapted from https://www.appveyor.com/docs/running-tests/#uploading-xml-test-results

$SolutionRoot = "$PSScriptRoot\.."
$TestProjects = Get-ChildItem -Path $SolutionRoot\Tests\ -Exclude CSF.Screenplay.Selenium.TestWebapp

$wc = New-Object 'System.Net.WebClient'
$TestEndpoint = "https://ci.appveyor.com/api/testresults/nunit/$env:APPVEYOR_JOB_ID"

foreach($project in $TestProjects)
{
    $projectName = Split-Path $project -Leaf
    $testResultFile = "$project\TestResults\TestResults.xml"
    Move-Item $testResultFile "$SolutionRoot\TestResults\$projectName.TestResults.xml"
    $wc.UploadFile($TestEndpoint, (Resolve-Path $SolutionRoot\TestResults\$projectName.TestResults.xml))
}

exit $env:TESTS_FAILED
