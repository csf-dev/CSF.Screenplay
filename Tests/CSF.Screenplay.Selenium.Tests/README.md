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
There are two steps to enabling this, in this project.

[the BrowserStack SDK]: https://www.browserstack.com/blog/introducing-browserstack-sdk/

### An MSBuild property

When using `dotnet build` or `dotnet test`, include the property `BrowserStack` with a value of `true`.
For example:

```cmd
dotnet test -p:BrowserStackEnabled=true
```

This will activate two changes in the `.csproj` file in this directory:

* It will install the BrowserStack SDK NuGet package: `BrowserStack.TestAdapter`
* It will disable NUnit's parallelism feature
  * _When NUnit parallelism is enabled, it interferes with BrowserStack's own parallelism, creating confusing results_

### Add a `browserstack.yml` configuration

To use the BrowserStack SDK, you must provide a configuration file.
The configuration file must be named `browserstack.yml` and it must be in the directory which contains the tests `.csproj` file _(IE: this directory, which contains this README file)_.

There are seven sample configuration files available in the `Tools/` directory, which target seven different web browser/OS combinations.
These happen to be the targets for the CBT CI run.
You are welcome to copy one of these YAML files into the `CSF.Screenplay.Selenium.Tests` directory and rename it to `browserstack.yml`.
Alternatively [you may craft your own BrowserStack configuration file].

If you use one of the sample configurations, note that the `userName` and `accessKey` properties are omitted.
_You will need to provide your own BrowserStack credentials_.

[you may craft your own BrowserStack configuration file]: https://www.browserstack.com/docs/automate/capabilities
