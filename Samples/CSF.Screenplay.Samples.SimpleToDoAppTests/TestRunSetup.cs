using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
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
    static readonly ConcurrentQueue<string> sampleAppOutput = new();

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
    /// <para>
    /// This includes logic which waits for the sample app to come up (via <c>npm run serve</c>) and which attempts to clean up a dangling
    /// process should there be a failure of some kind.
    /// Additionally, if we are unable to connect to the sample app, an <see cref="InvalidOperationException"/> is thrown, which includes
    /// the output from NPM.  This should help diagnose the problem.
    /// </para>
    /// </remarks>
    /// <returns>An awaitable task</returns>
    static async Task StartSampleAppAsync()
    {
        var startInfo = GetSampleAppStartInfo();
        sampleAppProcess = new Process { StartInfo = startInfo };
        sampleAppProcess.OutputDataReceived += (_, e) => { if (e.Data is not null) sampleAppOutput.Enqueue($"[stdout] {e.Data}"); };
        sampleAppProcess.ErrorDataReceived += (_, e) => { if (e.Data is not null) sampleAppOutput.Enqueue($"[stderr] {e.Data}"); };

        try
        {
            sampleAppProcess.Start();
            sampleAppProcess.BeginOutputReadLine();
            sampleAppProcess.BeginErrorReadLine();

            await WaitForSampleAppAsync(sampleAppProcess);
        }
        catch (Exception ex)
        {
            try { if (!sampleAppProcess.HasExited) sampleAppProcess.Kill(true); } catch { /* intentional no-op */ }

            throw new InvalidOperationException(
                $"The sample app did not start correctly, or was unreachable.{Environment.NewLine}" +
                $"Working directory: {startInfo.WorkingDirectory}{Environment.NewLine}" +
                $"Command: {startInfo.FileName} {string.Join(' ', startInfo.ArgumentList)}{Environment.NewLine}" +
                $"--- npm output ---{Environment.NewLine}{GetSampleAppOutput()}{Environment.NewLine}--- end npm output ---",
                ex);
        }

        using var httpClient = new HttpClient();
        var result = await httpClient.GetAsync(sampleAppUrl);
        result.EnsureSuccessStatusCode();
    }

    /// <summary>
    /// Gets the start info for the sample app process.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Executing NPM via Windows requires cmd.exe, because NPM is a <c>npm.cmd</c> batch-file formatted file.
    /// On Linux (CI environments) though, it is not.
    /// </para>
    /// </remarks>
    /// <returns>Process start info</returns>
    static ProcessStartInfo GetSampleAppStartInfo()
    {
        var startInfo = new ProcessStartInfo
        {
            WorkingDirectory = GetSampleAppWorkingDirectory(),
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
        };

        if (OperatingSystem.IsWindows())
        {
            startInfo.FileName = "cmd.exe";
            foreach (var arg in new[] { "/c", "npm", "run", "serve" }) startInfo.ArgumentList.Add(arg);
        }
        else
        {
            startInfo.FileName = "npm";
            foreach (var arg in new[] { "run", "serve" }) startInfo.ArgumentList.Add(arg);
        }

        return startInfo;
    }
    static string GetSampleAppOutput()
    {
        var sb = new StringBuilder();
        foreach (var line in sampleAppOutput) sb.AppendLine(line);
        return sb.Length == 0 ? "(no output captured)" : sb.ToString();
    }

    /// <summary>
    /// Makes up to 10 attempts to connect to the sample application, over a maximum of approx 28 seconds.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If the NPM process has exited early then this algorithm fails fast with an <see cref="InvalidOperationException"/>.
    /// This method uses a very simple exponential backoff algorithm for the retries, capped at a 5 second wait between retries.
    /// Over the total of 10 attempts, this puts us at a maximum wait of approx 28 seconds.
    /// </para>
    /// </remarks>
    /// <param name="process"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="TimeoutException"></exception>
    static async Task WaitForSampleAppAsync(Process process)
    {
        const int maxConnectionAttempts = 10;

        using var httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(5) };
        Exception? lastError = null;

        for (var attempt = 1; attempt <= maxConnectionAttempts; attempt++)
        {
            if (process.HasExited)
            {
                process.WaitForExit();
                throw new InvalidOperationException($"The npm process exited prematurely with exit code {process.ExitCode}.");
            }

            try
            {
                using var response = await httpClient.GetAsync(sampleAppUrl);
                response.EnsureSuccessStatusCode();
                return;
            }
            catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException)
            {
                lastError = ex;
                if (attempt == maxConnectionAttempts) break;
                var delay = TimeSpan.FromMilliseconds(Math.Min(250 * Math.Pow(2, attempt - 1), 5000));
                await Task.Delay(delay);
            }
        }

        throw new TimeoutException($"The sample app did not respond after {maxConnectionAttempts} attempts.", lastError);
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