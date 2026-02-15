using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using CSF.Screenplay.Performances;
using CSF.Screenplay.Selenium.BrowserStack;

namespace CSF.Screenplay.Selenium;

[SetUpFixture]
public class TestWebappSetupAndTeardown
{
    const int maxAttempts = 15;
    const int secondsDelay = 2;

    static Process? webAppProcess;
    static BrowserStackExtension browserStack;

    [OneTimeSetUp]
    public async Task StartWebAppAsync()
    {
        webAppProcess = Process.Start("dotnet", $"run --project {GetPathToWebappProject()}");
        await WaitForWebAppToBeAvailableAsync();

        browserStack = new BrowserStackExtension();
        await browserStack.OnTestRunStarting();
    }

    [OneTimeTearDown]
    public void StopWebApp()
    {
        webAppProcess?.Kill(true);
        webAppProcess?.Dispose();

        browserStack?.Dispose();
    }

    /// <summary>
    /// Waits for the testing web app to be available by attempting to connect to it.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This method will make a number of attempts to connect to the web app, with a delay between each attempt.
    /// If the web app is not available after the maximum number of attempts then an exception will be thrown.
    /// </para>
    /// </remarks>
    /// <returns>A task which completes when the web app is available.</returns>
    async static Task WaitForWebAppToBeAvailableAsync()
    {
        using var client = new HttpClient();

        for(var attempt = 0; attempt < maxAttempts; attempt++)
        {
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(secondsDelay));
                await client.GetAsync(new Uri(Webster.TestWebappBaseUri, "index.html"));
                return;
            }
            catch(Exception)
            {
                // Intentional no-op; we'll just try again
            }
        }

        throw new TimeoutException($"The web app was not available after {secondsDelay * maxAttempts} seconds.");
    }

    static string GetPathToWebappProject() => Path.Combine("..", "..", "..", "..", "CSF.Screenplay.Selenium.TestWebapp");
}