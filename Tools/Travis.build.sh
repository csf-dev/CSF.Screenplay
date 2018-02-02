#!/bin/bash

echo_integration_test_results_to_console()
{
  cat JsonApis.report.txt
}

Tools/Build.sh
result="$?"
echo_integration_test_results_to_console

exit $result