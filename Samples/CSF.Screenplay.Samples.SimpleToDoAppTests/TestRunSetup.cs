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

    static string GetSampleAppWorkingDirectory() => Path.Combine(GetTestProjectDirectory(), "..", sampleAppName);

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

    static async Task StartSampleAppAsync()
    {
        var startInfo = new ProcessStartInfo("npm", ["run", "serve"]) { WorkingDirectory = GetSampleAppWorkingDirectory(), UseShellExecute = true, CreateNoWindow = true };
        sampleAppProcess = Process.Start(startInfo);
        await Task.Delay(100);

        using var httpClient = new HttpClient();
        var result = await httpClient.GetAsync(sampleAppUrl);
        result.EnsureSuccessStatusCode();
    }

    static async Task StopSampleAppAsync()
    {
        await Task.Delay(100);
        sampleAppProcess?.Kill(true);
    }
}