#!/bin/bash

NUNIT_PATH="./testrunner/NUnit.ConsoleRunner.3.6.1/tools/nunit3-console.exe"
TEST_PATTERN="CSF.*.Tests.dll"
WEB_TESTS="CSF.Screenplay.Web.Tests.dll"
WEB_TESTS_PATH="Tests/CSF.Screenplay.Web.Tests/bin/Debug/CSF.Screenplay.Web.Tests.dll"
SERVER_PORT="8080"
SERVER_ADDR="127.0.0.1"
SERVER_WEB_APP="/:Tests/CSF.Screenplay.WebTestWebsite/"
SERVER_PID=".xsp4.pid"

test_outcome=1

stop_if_failure()
{
  code="$1"
  if [ "$code" -ne "0" ]
  then
    exit "$code"
  fi
}

build_solution()
{
  msbuild /p:Configuration=Debug CSF.Screenplay.sln
  stop_if_failure $?
}

run_unit_tests()
{
  test_assemblies=$(find ./Tests/ -type f -path "*/bin/Debug/*" -name "$TEST_PATTERN" \! -name "$WEB_TESTS")
  mono "$NUNIT_PATH" $test_assemblies
  stop_if_failure $?
}

start_webserver()
{
  xsp4 --address "$SERVER_ADDR" --port "$SERVER_PORT" --applications "$SERVER_WEB_APP" --pidfile "$SERVER_PID" &
  echo "Waiting for 10 seconds for the web server to come up ..."
  sleep 10
}

run_integration_tests()
{
  mono "$NUNIT_PATH" "$WEB_TESTS_PATH"
  test_outcome=$?
}

shutdown_webserver()
{
  echo "Shutting down webserver ..."
  cat "$SERVER_PID" | kill
}

build_solution
run_unit_tests
start_webserver
run_integration_tests
shutdown_webserver

exit $test_outcome