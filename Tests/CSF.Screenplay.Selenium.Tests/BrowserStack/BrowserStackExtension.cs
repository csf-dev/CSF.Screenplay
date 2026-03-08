using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BrowserStack;
using CSF.Screenplay.Performances;
using Microsoft.Extensions.DependencyInjection;

namespace CSF.Screenplay.Selenium.BrowserStack;

public sealed class BrowserStackExtension : IDisposable
{
    Local? browserStackLocal;
    IHasPerformanceEvents? eventBus;

    public async Task OnTestRunStarting()
    {
        if(!BrowserStackEnvironment.IsBrowserStackTest()) return;

        browserStackLocal = new Local();
        browserStackLocal.start(GetBrowserStackLocalArgs().ToList());
        for(var i = 0; i < 80; i++)
        {
            Console.WriteLine("Waiting for BrowserStackLocal to start up, attempt {0} of {1} ...", i + 1, 80);
            await Task.Delay(250);
            if(browserStackLocal.isRunning()) break;
        }
        if(!browserStackLocal.isRunning()) throw new TimeoutException("BrowserStack Local is still not running after 20 seconds");

        eventBus = ScreenplayLocator.GetScreenplay(Assembly.GetExecutingAssembly()).GetEventBus();
        eventBus.PerformanceFinished += UpdateBrowserStackSession;

    }

    void UpdateBrowserStackSession(object? sender, PerformanceFinishedEventArgs e)
    {
        if(!e.Success.HasValue) return;

        var cast = e.Performance.ServiceProvider.GetRequiredService<ICast>();
        var ability = (from actorName in cast.GetCastList()
                       let actor = cast.GetActor(actorName)
                       where ((IHasAbilities) actor).HasAbility<BrowseTheWeb>()
                       select actor.GetAbility<BrowseTheWeb>())
            .FirstOrDefault();

        if(ability is null) return;

        var jsExecutor = ability.GetJavaScriptExecutor();
        var json = $@"{{""action"":""setSessionStatus"",""arguments"":{{""status"":""{(e.Success.Value ? "passed" : "failed")}"",""reason"":""Test completion""}}}}";
        jsExecutor.ExecuteScript("browserstack_executor: " + json);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        browserStackLocal?.stop();

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