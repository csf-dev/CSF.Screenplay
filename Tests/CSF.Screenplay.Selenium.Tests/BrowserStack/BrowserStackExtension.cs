using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using BrowserStack;
using CSF.Screenplay.Performances;

namespace CSF.Screenplay.Selenium.BrowserStack;

public sealed class BrowserStackExtension : IDisposable
{
    const string urlPattern = "https://www.browserstack.com/automate/sessions/{0}.json";

    Local? browserStackLocal;
    IHasPerformanceEvents? eventBus;
    HttpClient? httpClient;

    public async Task OnTestRunStarting()
    {
        if(!BrowserStackEnvironment.IsBrowserStackTest()) return;

        browserStackLocal = new Local();
        browserStackLocal.start(GetBrowserStackLocalArgs().ToList());
        for(var i = 0; i < 80; i++)
        {
            await Task.Delay(250);
            if(browserStackLocal.isRunning()) break;
        }
        if(!browserStackLocal.isRunning()) throw new TimeoutException("BrowserStack Local is still not running after 20 seconds");

        httpClient = GetHttpClient();

        eventBus = ScreenplayLocator.GetScreenplay(Assembly.GetExecutingAssembly()).GetEventBus();
        eventBus.PerformanceFinished += UpdateBrowserStackSession;

    }

    void UpdateBrowserStackSession(object? sender, PerformanceFinishedEventArgs e)
    {
        if(!e.Success.HasValue) return;

        var sessionId = BrowserStackSessionIdProvider.GetBrowserStackSessionId(e.Performance);
        if(sessionId is null) return;
        
        var uri = GetApiUri(sessionId);
        var requestMessage = GetRequestMessage(uri, e.Success.Value);
        httpClient!.Send(requestMessage);
    }

    static HttpRequestMessage GetRequestMessage(Uri requestUri, bool success)
    {
        return new (HttpMethod.Put, requestUri)
        {
            Content = JsonContent.Create(@$"{{""status"":""{ (success ? "passed" : "failed") }"", ""reason"":""""}}"),
        };
    }

    static Uri GetApiUri(string sessionId) => new(string.Format(urlPattern, sessionId));

    static HttpClient GetHttpClient()
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = GetAuthenticationHeaderValue();
        return client;
    }

    static AuthenticationHeaderValue GetAuthenticationHeaderValue()
    {
        var headerBytes = Encoding.ASCII.GetBytes($"{BrowserStackEnvironment.GetBrowserStackUserName()}:{BrowserStackEnvironment.GetBrowserStackAccessKey()}");
        var headerValue = Convert.ToBase64String(headerBytes);
        return new ("Basic", headerValue);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        browserStackLocal?.stop();
        httpClient?.Dispose();

        if(eventBus is null) return;
        eventBus.PerformanceFinished -= UpdateBrowserStackSession;
    }

    static Dictionary<string, string> GetBrowserStackLocalArgs()
    {
        return new ()
        {
            { "key", BrowserStackEnvironment.GetBrowserStackAccessKey()! },
        };
    }
}