@echo on

git submodule update --init --recursive

nuget restore CSF.Screenplay.sln

copy /y Tests\CSF.Screenplay.Web.Tests\App.AppVeyor.config Tests\CSF.Screenplay.Web.Tests\App.config

@echo off