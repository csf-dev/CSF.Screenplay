#!/bin/bash

NUNIT_PATH="./testrunner/NUnit.ConsoleRunner.3.6.1/tools/nunit3-console.exe"
TEST_PATTERN="CSF.*.Tests.dll"
WEB_TESTS="CSF.Screenplay.Web.Tests.dll"

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

build_solution

run_unit_tests