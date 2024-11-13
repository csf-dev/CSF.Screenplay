# Taken from https://www.appveyor.com/docs/running-tests/#uploading-xml-test-results

$SolutionRoot = "$PSScriptRoot\.."
$TestProjects = Get-ChildItem $SolutionRoot\Tests\

$wc = New-Object 'System.Net.WebClient'

# Temporary change, made to diagnose test results upload error:
# https://help.appveyor.com/discussions/problems/37319-http-500-error-when-uploading-nunit3-test-results-suspected-failure-on-parameterized-test-cases
Push-AppveyorArtifact "$SolutionRoot\Tests\CSF.Screenplay.Tests\TestResults\TestResults.xml"

foreach($project in $TestProjects)
{
    $testResultFile = "$SolutionRoot\Tests\$project\TestResults\TestResults.xml"
    Move-Item $testResultFile "$SolutionRoot\TestResults\$project.TestResults.xml"
    $wc.UploadFile("https://ci.appveyor.com/api/testresults/nunit/$($env:APPVEYOR_JOB_ID)", (Resolve-Path $SolutionRoot\TestResults\$project.TestResults.xml))
}
