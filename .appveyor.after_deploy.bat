@echo on

nunit3-console.exe Tests\CSF.Screenplay.Web.Tests\bin\Debug\CSF.Screenplay.Web.Tests.dll

@echo.
@echo *****************
@echo Screenplay report
@echo *****************

@type screenplay-report.txt

@echo off