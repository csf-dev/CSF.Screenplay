using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;

namespace CSF.Screenplay.Selenium;

[SetUpFixture]
public class TestWebappSetupAndTeardown
{
    const int maxAttempts = 15;
    const int secondsDelay = 2;

    static Process? webAppProcess;

    [OneTimeSetUp]
    public async Task StartWebAppAsync()
    {
        webAppProcess = Process.Start("dotnet", $"run --project {GetPathToWebappProject()}");
        using var client = new HttpClient();

        // Wait for the web app to start, up to 30 seconds across 15 attempts
        for(var attempt = 0; attempt < maxAttempts; attempt++)
        {
            try
            {
                await client.GetAsync(new Uri(Webster.TestWebappBaseUri, "index.html"));
                break;
            }
            catch(Exception e)
            {
                Console.Error.WriteLine($"Failed to connect to web app on attempt {attempt + 1} of {maxAttempts}: " + e.Message);
                await Task.Delay(TimeSpan.FromSeconds(secondsDelay));
            }
        }
    }

    [OneTimeTearDown]
    public void StopWebApp()
    {
        webAppProcess?.Kill();
        webAppProcess?.Dispose();
    }

    static string GetPathToWebappProject() => Path.Combine("..", "..", "..", "..", "CSF.Screenplay.Selenium.TestWebapp");
}