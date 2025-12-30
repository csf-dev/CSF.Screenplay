REM Set up a Selenium Manager config file, so that a fresh browser and driver are
REM downloaded explicitly, ignoring the pre-installed driver on the CI image.

mkdir %USERPROFILE%\.cache\selenium
cp Tools\se-config.toml %USERPROFILE%\.cache\selenium

REM Redefines the PATH environment variable, removing the preinstalled Selenium Webdriver.
REM Modern Selenium downloads/fetches the appropriate driver version for the browser, so
REM having this pre-installed driver in the path actually hurts more than helps.

set "UNWANTED_PATH=C:\Tools\WebDriver"

REM Remove the unwanted path (handles all of ";path;", ";path" and "path;" cases)
set "NEW_PATH=%PATH:;%UNWANTED_PATH%;=;%"
set "NEW_PATH=%NEW_PATH:;%UNWANTED_PATH%=%"
set "NEW_PATH=%NEW_PATH:%UNWANTED_PATH%;=%"

set "PATH=%NEW_PATH%"

echo Updated PATH is: %PATH%
