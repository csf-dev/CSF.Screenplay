#!/bin/bash

if [ -n "$NUGET_PATH" ]
then
  NUGET_COMMAND="mono ${NUGET_PATH}"
else
  NUGET_COMMAND="nuget"
fi

NUNIT_VERSION="${NUNIT_CONSOLE_VERSION:-3.7.0}"

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

install_test_runner()
{
  echo "Downloading an NUnit test runner ..."
  $NUGET_COMMAND install \
    "NUnit.ConsoleRunner" \
    -Version "$NUNIT_VERSION" \
    -OutputDirectory testrunner
  stop_if_failure $? "Download NUnit test runner"
}

restore_solution_nuget_packages()
{
  echo "Restoring NuGet packages for the solution ..."
  $NUGET_COMMAND restore CSF.Screenplay.Selenium.sln
  stop_if_failure $? "Restore NuGet packages"
}

install_test_runner
restore_solution_nuget_packages

exit 0