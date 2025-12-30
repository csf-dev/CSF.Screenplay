REM Set up a Selenium Manager config file, so that a fresh browser and driver are
REM downloaded explicitly, ignoring the pre-installed driver on the CI image.

mkdir %USERPROFILE%\.cache\selenium
cp Tools\se-config.toml %USERPROFILE%\.cache\selenium