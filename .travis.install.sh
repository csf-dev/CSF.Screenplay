#!/bin/bash

NUGET_LATEST_DIST="https://dist.nuget.org/win-x86-commandline/latest/nuget.exe"
TRAVIS_TEST_CONFIG_SOURCE="Tests/CSF.Screenplay.Web.Tests/App.Travis.config"
TEST_CONFIG_DESTINATION="Tests/CSF.Screenplay.Web.Tests/App.config"
NUGET_PATH=".nuget/nuget.exe"

stop_if_failure()
{
  code="$1"
  if [ "$code" -ne "0" ]
  then
    exit "$code"
  fi
}

install_latest_nuget()
{
  # Travis uses Xamarin's apt repo which has an ancient nuget version
  mkdir -p .nuget
  wget -O "$NUGET_PATH" "$NUGET_LATEST_DIST"
  stop_if_failure $?
}

echo_nuget_version_to_console()
{
  mono "$NUGET_PATH"
}

setup_travis_test_config()
{
  cp "$TRAVIS_TEST_CONFIG_SOURCE" "$TEST_CONFIG_DESTINATION"
  stop_if_failure $?
}

install_test_runner()
{
  mono "$NUGET_PATH" install NUnit.ConsoleRunner -Version 3.6.1 -OutputDirectory testrunner
  stop_if_failure $?
}

restore_solution_nuget_packages()
{
  mono "$NUGET_PATH" restore CSF.Screenplay.sln
  stop_if_failure $?
}

install_latest_nuget

echo_nuget_version_to_console
setup_travis_test_config
install_test_runner
restore_solution_nuget_packages

exit 0