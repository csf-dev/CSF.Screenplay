#!/bin/bash

NUNIT_CONSOLE_VERSION="3.7.0"
NUNIT_PATH="./testrunner/NUnit.ConsoleRunner.${NUNIT_CONSOLE_VERSION}/tools/nunit3-console.exe"
TEST_PATTERN="CSF.*.Tests.dll"
WEB_TESTS="CSF.Screenplay.Web.Tests"
JSON_TESTS="CSF.Screenplay.JsonApis.Tests"
WEB_TESTS_PATH="Tests/${WEB_TESTS}/bin/Debug/${WEB_TESTS}.dll"
JSON_TESTS_PATH="Tests/${JSON_TESTS}/bin/Debug/${JSON_TESTS}.dll"

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
  msbuild /p:Configuration=Debug CSF.Screenplay.sln
  stop_if_failure $? "Build the solution"
}

run_unit_tests()
{
  echo "Running unit tests ..."
  test_assemblies=$(find ./Tests/ -type f -path "*/bin/Debug/*" -name "$TEST_PATTERN" \! -name "${WEB_TESTS}.dll" \! -name "${JSON_TESTS}.dll")
  mono "$NUNIT_PATH" $test_assemblies
  stop_if_failure $? "Run unit tests"
}

start_webserver()
{
  echo "Starting up the application ..."
  bash Tools/Start-webserver.sh
  stop_if_failure $? "Starting the application"
}

run_integration_tests()
{
  echo "Running integration tests ..."
  mono "$NUNIT_PATH" --labels=All "$WEB_TESTS_PATH" "$JSON_TESTS_PATH"
  test_outcome=$?
}

shutdown_webserver()
{
  bash Tools/Stop-webserver.sh
}

build_solution
run_unit_tests
start_webserver
run_integration_tests
shutdown_webserver

exit $test_outcome
