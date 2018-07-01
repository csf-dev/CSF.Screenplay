#!/bin/bash

NUNIT_CONSOLE_VERSION="3.7.0"
NUNIT_PATH="./testrunner/NUnit.ConsoleRunner.${NUNIT_CONSOLE_VERSION}/tools/nunit3-console.exe"
TEST_PATTERN="CSF.*.Tests.dll"
UNIT_TESTS="CSF.Screenplay.Selenium.BrowserFlags.Tests"
UNIT_TESTS_PATH="${WEB_TESTS}/bin/Debug/${WEB_TESTS}.dll"
WEB_TESTS="CSF.Screenplay.Selenium.Tests"
WEB_TESTS_PATH="${WEB_TESTS}/bin/Debug/${WEB_TESTS}.dll"

test_outcome=1

stop_if_failure()
{
  code="$1"
  process="$2"
  if [ "$code" -ne "0" ]
  then
    echo "The process '${process}' failed with exit code $code"
    exit "$code"
  fi
}

build_solution()
{
  echo "Building the solution ..."
  msbuild /p:Configuration=Debug CSF.Screenplay.Selenium.sln
  stop_if_failure $? "Build the solution"
}

start_webserver()
{
  echo "Starting up the application ..."
  bash Tools/Start-webserver.sh
  stop_if_failure $? "Starting the application"
}

run_unit_tests()
{
  echo "Running integration tests ..."
  mono "$NUNIT_PATH" --labels=All "$UNIT_TESTS_PATH"
  test_outcome=$?
}

run_integration_tests()
{
  echo "Running integration tests ..."
  mono "$NUNIT_PATH" --labels=All "$WEB_TESTS_PATH"
  test_outcome=$?
}

shutdown_webserver()
{
  bash Tools/Stop-webserver.sh
}

build_solution

run_unit_tests
if [ "$test_outcome" -ne "0" ]
then
  echo "Stopping the build: Unit test failure"
  exit 1
fi

start_webserver
run_integration_tests
shutdown_webserver

exit $test_outcome
