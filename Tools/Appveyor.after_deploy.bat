﻿@echo on

@SET /A exitcode=0
@SET /A TESTFAILURE_ERROR=1
@SET /A PUSHARTIFACT_ERROR=2
@SET /A READREPORT_ERROR=4

nunit3-console.exe CSF.Screenplay.Selenium.Tests\bin\Debug\CSF.Screenplay.Selenium.Tests.dll
@IF %ERRORLEVEL% NEQ 0 SET /A exitcode^|=%TESTFAILURE_ERROR%

appveyor PushArtifact NUnit.report.txt
@IF %ERRORLEVEL% NEQ 0 SET /A exitcode^|=%PUSHARTIFACT_ERROR%

@echo ******************
@echo Screenplay reports
@echo ******************

@type NUnit.report.txt
@IF %ERRORLEVEL% NEQ 0 SET /A exitcode^|=%READREPORT_ERROR%

@echo off

@EXIT /B %exitcode%
