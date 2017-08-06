@echo on

nunit3-console.exe Tests\CSF.Screenplay.Web.Tests\bin\Debug\CSF.Screenplay.Web.Tests.dll

appveyor PushArtifact screenplay-report.txt

@echo.
@echo *****************
@echo Screenplay report
@echo *****************

@type screenplay-report.txt

@echo off