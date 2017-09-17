#!/bin/bash

NUNIT_CONSOLE_VERSION="3.8.0-ci-03749-socketexcep"
NUNIT_PATH="./testrunner/NUnit.ConsoleRunner.${NUNIT_CONSOLE_VERSION}/tools/nunit3-console.exe"
TEST_PATTERN="CSF.*.Tests.dll"
WEB_TESTS="CSF.Screenplay.Web.Tests.dll"
WEB_TESTS_PATH="Tests/CSF.Screenplay.Web.Tests/bin/Debug/CSF.Screenplay.Web.Tests.dll"
SERVER_PID=".xsp4.pid"

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
  test_assemblies=$(find ./Tests/ -type f -path "*/bin/Debug/*" -name "$TEST_PATTERN" \! -name "$WEB_TESTS")
  mono "$NUNIT_PATH" --remoting-wait-after-stop --remoting-shutdown-delay=5000 $test_assemblies
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
  mono "$NUNIT_PATH" --labels=All --remoting-wait-after-stop --remoting-shutdown-delay=5000 "$WEB_TESTS_PATH"
  test_outcome=$?
}

shutdown_webserver()
{
  bash Tools/Stop-webserver.sh
}

echo_integration_test_results_to_console()
{
  cat NUnit.report.txt
}

build_solution
run_unit_tests
start_webserver
run_integration_tests
echo_integration_test_results_to_console
shutdown_webserver

exit $test_outcome
