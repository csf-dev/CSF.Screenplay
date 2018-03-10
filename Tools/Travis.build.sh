#!/bin/bash

echo_integration_test_results_to_console()
{
  cat NUnit.report.txt
}

setup_webdriver_environment_variables()
{
  WebDriver_SauceLabsBuildName="Travis Screenplay.Selenium job ${TRAVIS_JOB_NUMBER}; ${WebDriver_BrowserName} ${WebDriver_BrowserVersion}"
  WebDriver_TunnelIdentifier="$TRAVIS_JOB_NUMBER"
}

setup_webdriver_environment_variables

export WebDriver_SauceLabsBuildName
export WebDriver_TunnelIdentifier

Tools/Build.sh
result="$?"
echo_integration_test_results_to_console

exit $result