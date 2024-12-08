using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;

namespace CSF.Screenplay.Selenium;

[SetUpFixture]
public class TestWebappSetupAndTeardown
{
    static Process? webAppProcess;

    [OneTimeSetUp]
    public async Task StartWebAppAsync()
    {
        webAppProcess = Process.Start("dotnet", $"run --project {GetPathToWebappProject()}");
        using var client = new HttpClient();

        // Wait for the web app to start, up to 20 seconds across 10 attempts
        for(var attempt = 0; attempt < 10; attempt++)
        {
            try
            {
                await client.GetAsync(new Uri(Webster.TestWebappBaseUri, "index.html"));
                break;
            }
            catch(Exception e)
            {
                Console.Error.WriteLine($"Failed to connect to web app on attempt {attempt + 1} of 10: " + e.Message);
                await Task.Delay(TimeSpan.FromSeconds(2));
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