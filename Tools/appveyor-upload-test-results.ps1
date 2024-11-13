# Adapted from https://www.appveyor.com/docs/running-tests/#uploading-xml-test-results

$SolutionRoot = "$PSScriptRoot\.."
$TestProjects = Get-ChildItem $SolutionRoot\Tests\

$wc = New-Object 'System.Net.WebClient'

foreach($project in $TestProjects)
{
    $testResultFile = "$SolutionRoot\Tests\$project\TestResults\TestResults.xml"
    Move-Item $testResultFile "$SolutionRoot\TestResults\$project.TestResults.xml"
    
    # Intentionally using the 'nunit' endpoint and not 'nunit3'.  See the following ticket for more info:
    # https://help.appveyor.com/discussions/problems/37319-http-500-error-when-uploading-nunit3-test-results-suspected-failure-on-parameterized-test-cases
    $wc.UploadFile("https://ci.appveyor.com/api/testresults/nunit/$($env:APPVEYOR_JOB_ID)", (Resolve-Path $SolutionRoot\TestResults\$project.TestResults.xml))
}
