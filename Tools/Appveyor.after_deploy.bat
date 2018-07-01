@echo on

@SET /A exitcode=0
@SET /A TESTFAILURE_ERROR=1
@SET /A PUSHARTIFACT_ERROR=2
@SET /A READREPORT_ERROR=4

nunit3-console.exe CSF.Screenplay.Selenium.Tests\bin\Debug\CSF.Screenplay.Selenium.Tests.dll
@IF %ERRORLEVEL% NEQ 0 SET /A exitcode^|=%TESTFAILURE_ERROR%

appveyor PushArtifact NUnit.report.json
@IF %ERRORLEVEL% NEQ 0 SET /A exitcode^|=%PUSHARTIFACT_ERROR%

@EXIT /B %exitcode%
