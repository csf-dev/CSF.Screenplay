#!/bin/bash

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

exit $result