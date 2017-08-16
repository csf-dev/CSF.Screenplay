#!/bin/bash

NUNIT_PATH="./testrunner/NUnit.ConsoleRunner.3.6.1/tools/nunit3-console.exe"
TEST_PATTERN="CSF.*.Tests.dll"
WEB_TESTS="CSF.Screenplay.Web.Tests.dll"
WEB_TESTS_PATH="Tests/CSF.Screenplay.Web.Tests/bin/Debug/CSF.Screenplay.Web.Tests.dll"
SERVER_PORT="8080"
SERVER_ADDR="127.0.0.1"
SERVER_WEB_APP="/:Tests/CSF.Screenplay.WebTestWebsite/"
SERVER_PID=".xsp4.pid"
APP_HOMEPAGE="http://localhost:8080/Home"

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
  mono "$NUNIT_PATH" $test_assemblies
  stop_if_failure $? "Run unit tests"
}

start_webserver()
{
  echo "Starting the app on a web server ..."
  xsp4 --nonstop --address "$SERVER_ADDR" --port "$SERVER_PORT" --applications "$SERVER_WEB_APP" --pidfile "$SERVER_PID" &
}

wait_for_app_to_become_available()
{
  echo "Waiting for the app to become available ..."
  sleep 1
  wget -O - "$APP_HOMEPAGE" >/dev/null 2>&1
  stop_if_failure $? "Wait for the app to start up"
}

run_integration_tests()
{
  echo "Running integration tests ..."
  mono "$NUNIT_PATH" "$WEB_TESTS_PATH"
  test_outcome=$? "Run integration tests"
}

shutdown_webserver()
{
  echo "Shutting down webserver ..."
  kill $(cat "$SERVER_PID")
  rm "$SERVER_PID"
}

echo_integration_test_results_to_console()
{
  cat NUnit.report.txt
}

build_solution
run_unit_tests
start_webserver
wait_for_app_to_become_available
run_integration_tests
echo_integration_test_results_to_console
shutdown_webserver

exit $test_outcome