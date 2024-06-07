@echo on

git submodule update --init --recursive

nuget restore CSF.Screenplay.sln

@echo off