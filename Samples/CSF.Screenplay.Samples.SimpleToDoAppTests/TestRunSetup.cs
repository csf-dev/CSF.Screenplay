using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reflection;
using CSF.Extensions.WebDriver.Factories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll.BoDi;

namespace CSF.Screenplay;

/// <summary>
/// Configures the Reqnroll test run appropriately to use Screenplay/Selenium, and to ensure that the TodoMVC sample app
/// is started and running automatically when the test run is performed.
/// </summary>
[Binding]
public static class TestRunSetup
{
    const string
        testProjectName = "CSF.Screenplay.Samples.SimpleToDoAppTests",
        sampleAppName = "CSF.Screenplay.Samples.SimpleToDoApp",
        sampleAppUrl = "http://localhost:5173/";

    internal static readonly Uri SampleAppUri = new(sampleAppUrl);

    static Process? sampleAppProcess;

    [BeforeTestRun]
    public static async Task BeforeTestRunAsync(IObjectContainer container)
    {
        SetupDependencyInjection(container);
        await StartSampleAppAsync();
    }

    [AfterTestRun]
    public static async Task AfterTestRunAsync()
    {
        await StopSampleAppAsync();
    }

    /// <summary>
    /// Gets the working directory to the sample application.
    /// </summary>
    /// <returns>A string directory path</returns>
    static string GetSampleAppWorkingDirectory()
    {
        var path = Path.Combine(GetTestProjectDirectory(), "..", sampleAppName);
        if(!Directory.Exists(path)) throw new DriveNotFoundException($"The sample application directory must exist: {path}");
        return path;
    }

    /// <summary>
    /// Gets the file system directory path to the location which contains this test project
    /// </summary>
    /// <returns>A string directory path</returns>
    /// <exception cref="InvalidOperationException">If this assembly is loaded other than from the file system</exception>
    /// <exception cref="DirectoryNotFoundException">If the test project is not an ancestor of the directory which contains this test project</exception>
    static string GetTestProjectDirectory()
    {
        var testDirectory = Assembly.GetExecutingAssembly().Location;
        if(string.IsNullOrWhiteSpace(testDirectory))
            throw new InvalidOperationException("The current assembly must be loaded from a file system path");
        
        for(var currentPath = testDirectory; !string.IsNullOrEmpty(currentPath); currentPath = Directory.GetParent(currentPath!)?.FullName)
        {
            var directory = new DirectoryInfo(currentPath);
            if(directory.Exists && directory.Name == testProjectName)
                return directory.FullName;
        }

        throw new DirectoryNotFoundException($"The assembly {testDirectory} must be contained within the test project directory: {testProjectName}");
    }

    /// <summary>
    /// Sets up Reqnroll dependency injection for this test run.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The Reqnroll Screenplay plugin adds Screenplay to DI automatically, but we must add a few things manually:
    /// </para>
    /// <list type="bullet">
    /// <item><description>The configuration, using Microsoft.Extensions.Configuration.  This must be added manually because we are not using
    /// Generic Host here.</description></item>
    /// <item><description>The Screenplay/Selenium plugin.</description></item>
    /// <item><description>An adapter for the MS Options pattern.  This is required because the Reqnroll DI container comes with some limitations
    /// which cannot be worked around automatically.  One of these means that it is not compatible with the MS Options pattern without this
    /// additional DI registration.</description></item>
    /// <item><description>The Webster persona, which would need registering in any case.</description></item>
    /// </list>
    /// </remarks>
    /// <param name="container">The Reqnroll DI container</param>
    static void SetupDependencyInjection(IObjectContainer container)
    {
        container.ToServiceCollection()
            .AddSingleton(GetConfiguration)
            .AddSelenium()
            .AddOptionsAdapters<WebDriverCreationOptionsCollection>()
            .AddTransient<Webster>();
    }

    static IConfiguration GetConfiguration(IServiceProvider _)
    {
        var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
        return builder.Build();
    }

    /// <summary>
    /// Creates and starts an OS process which runs NPM, to start up the TodoMVC sample application.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The OS process handle is stored at <see cref="sampleAppProcess"/>, so that it may be stopped gracefully at a later stage.
    /// </para>
    /// </remarks>
    /// <returns>An awaitable task</returns>
    static async Task StartSampleAppAsync()
    {
        var startInfo = new ProcessStartInfo("npm", ["run", "serve"]) { WorkingDirectory = GetSampleAppWorkingDirectory(), UseShellExecute = true, CreateNoWindow = true };
        sampleAppProcess = Process.Start(startInfo);
        await Task.Delay(100);

        using var httpClient = new HttpClient();
        var result = await httpClient.GetAsync(sampleAppUrl);
        result.EnsureSuccessStatusCode();
    }

    /// <summary>
    /// Stops the OS process which runs the TodoMVC sample application.
    /// </summary>
    /// <returns>An awaitable task</returns>
    static async Task StopSampleAppAsync()
    {
        sampleAppProcess?.Kill(true);
    }
}