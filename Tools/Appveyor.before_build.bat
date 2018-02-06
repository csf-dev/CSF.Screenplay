@echo on

git submodule update --init --recursive

nuget restore CSF.Screenplay.Selenium.sln

copy /y CSF.Screenplay.Selenium.Tests\App.AppVeyor.config CSF.Screenplay.Selenium.Tests\App.config

@echo off