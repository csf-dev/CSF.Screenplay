using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Nodes;

namespace CSF.Screenplay.Selenium.BrowserStack;

public static class BrowserStackSessionIdProvider
{
    public static string? GetBrowserStackSessionId(IPerformance performance)
    {
        var performanceCast = performance.ServiceProvider.GetRequiredService<ICast>();
        var actors = performanceCast.GetCastList().Select(name => performanceCast.GetActor(name));
        var browseTheWeb = actors.FirstOrDefault((ICanPerform actor) => actor.HasAbility<BrowseTheWeb>())?.GetAbility<BrowseTheWeb>();
        if(browseTheWeb is null) return null;

        var javascriptExecutor = browseTheWeb.GetJavaScriptExecutor();
        var sessionDetailsJson = (string) javascriptExecutor.ExecuteScript("browserstack_executor: {\"action\": \"getSessionDetails\"}");
        var sessionDetails = JsonNode.Parse(sessionDetailsJson)!;
        return (string?) sessionDetails["hashed_id"];
    }
}