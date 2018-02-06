#!/bin/bash

echo_integration_test_results_to_console()
{
  cat NUnit.report.txt
}

Tools/Build.sh
result="$?"
echo_integration_test_results_to_console

exit $result