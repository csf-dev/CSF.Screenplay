using System;
using static System.Environment;

namespace CSF.Screenplay.Selenium.BrowserStack;

/// <summary>
/// Static helper provides access to environment variables which control <see cref="BrowserStackDriverFactory"/>.
/// </summary>
public static class BrowserStackEnvironment
{
    internal const string
        BrowserName = "BSbrowserName",
        BrowserVersion = "BSbrowserVersion",
        OperatingSystem = "BSos",
        OperatingSystemVersion = "BSosVersion",
        BrowserStackUserName = "BSuserName",
        BrowserStackAccessKey = "BSaccessKey",
        ProjectName = "BSprojectName",
        BuildName = "BSbuildName",
        SelectedWebDriverConfig = "WebDriverFactory__SelectedConfiguration",
        BrowserStackConfigName = "BrowserStack";

    internal static string? GetBrowserName() => GetEnvironmentVariable(BrowserName);
    internal static string? GetBrowserVersion() => GetEnvironmentVariable(BrowserVersion);
    internal static string? GetOperatingSystem() => GetEnvironmentVariable(OperatingSystem);
    internal static string? GetOperatingSystemVersion() => GetEnvironmentVariable(OperatingSystemVersion);
    internal static string? GetBrowserStackUserName() => GetEnvironmentVariable(BrowserStackUserName);
    internal static string? GetBrowserStackAccessKey() => GetEnvironmentVariable(BrowserStackAccessKey);
    internal static string? GetProjectName() => GetEnvironmentVariable(ProjectName);
    internal static string? GetBuildName() => GetEnvironmentVariable(BuildName);

    /// <summary>
    /// Gets a value indicating whether the current test run is running on BrowserStack
    /// </summary>
    internal static bool IsBrowserStackTest() => GetEnvironmentVariable(SelectedWebDriverConfig) == BrowserStackConfigName;
}