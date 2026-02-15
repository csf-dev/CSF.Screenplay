# Selenium extension tests

This directory contains tests for the Selenium extension.
Note that (for CI/Cross Browser Testing reasons) this test project includes an integration with [the BrowserStack SDK].
BrowserStack is primarily used by the **Cross-browser testing GitHub action** configuration; see `.github\workflows\crossBrowserTesting.yml` in the root of this repository.

## Testing locally (without BrowserStack)

The BrowserStack integration is _disabled by default_ to make local testing as simple as possible.

The WebDriver for local testing is chosen via the `appsettings.json` file.
This file is a configuration for [a universal WebDriver factory].
You may add/adjust WebDrivers present there, to your requirements.
The default configuration is to test with a locally-installed Google Chrome browser.
You may select a different browser/WebDriver for tests by either:

* Setting an environment variable `WebDriverFactory__SelectedConfiguration` to the name of your chosen configuration, for example `VerboseChrome`
* Using a command-line parameter for `dotnet test`:

  ```cmd
  dotnet test ---WebDriverFactory::SelectedConfiguration VerboseChrome
  ```

[a universal WebDriver factory]: https://csf-dev.github.io/CSF.Extensions.WebDriver/docs/index.html

## Enabling BrowserStack

The BrowserStack integration is via [the BrowserStack SDK].
There are three steps to enabling this, in this project.

[the BrowserStack SDK]: https://www.browserstack.com/blog/introducing-browserstack-sdk/

### Add the .props file

Before executing `dotnet build` or `dotnet test`, copy the file `Tools/BrowserStack.props` into this directory (that contains both the `.csproj` and this README files).
For example:

```cmd
copy Tools/BrowserStack.props .
dotnet test --filter TestCategory=WebDriver
```

The presence of this props file will activate two changes in the `.csproj` file in this directory:

* It will install the BrowserStack SDK NuGet package: `BrowserStack.TestAdapter`
* It will disable NUnit's parallelism feature
  * _When NUnit parallelism is enabled, it interferes with BrowserStack's own parallelism, creating confusing results_

### Review the `browserstack.yml` configuration

To use the BrowserStack SDK, you must provide a configuration file.
There is a `browserstack.yml` already present in this directory but it is configured so that most of its settings are read from environment variables.

You may wish to adjust the configuration file and replace the environment variable usage with selections of your own choice.
Also note that the `userName` and `accessKey` properties are omitted in the provided configuration file, as these are secrets.
_You will need to provide your own BrowserStack credentials_.
Alternatively [you may craft your own BrowserStack configuration file].

[you may craft your own BrowserStack configuration file]: https://www.browserstack.com/docs/automate/capabilities

### Advice: Only run WebDriver tests

When running tests, use `dotnet test --filter TestCategory=WebDriver`.
This limits the test run to only those which have the `WebDriver` test category.

The `WebDriver` category is shared by all tests which make use of a WebDriver (and are meaningful to run with BrowserStack).
Some other tests make use of NUnit attributes such as `[TestCase]` which seems not to play well with BrowserStack.
These appear to cause the BrowserStack SDK to repeat tests needlessly and to report on many more tests than are actually taking place.
