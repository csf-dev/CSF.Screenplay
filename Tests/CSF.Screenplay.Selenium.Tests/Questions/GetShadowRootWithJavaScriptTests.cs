using CSF.Screenplay.Selenium.Elements;
using OpenQA.Selenium;
using static CSF.Screenplay.PerformanceStarter;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Selenium.Questions;

[TestFixture, Parallelizable]
public class GetShadowRootWithJavaScriptTests
{
    static readonly Locator
        host = new ElementId("shadowHost", "The shadow host"),
        content = new CssSelector("p.content", "The content inside the Shadow DOM");

    static readonly NamedUri testPage = new NamedUri("GetShadowRoot.html", "the test page");

    [Test, Screenplay]
    public async Task GetShadowRootWithJavaScriptShouldResultInBeingAbleToReadTheShadowDomContent(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();
        var browseTheWeb = webster.GetAbility<BrowseTheWeb>();

        if (browseTheWeb.WebDriver.HasQuirk(BrowserQuirks.CannotGetShadowRoot))
            Assert.Pass("This test cannot be run on the current web browser");

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        var shadowRoot = await When(webster).AttemptsTo(GetTheShadowRootWithJavaScriptFrom(host));
        var shadowContent = await Then(webster).Should(FindAnElementWithin(shadowRoot).WhichMatches(content));
        var text = await Then(webster).Should(ReadFromTheElement(shadowContent).TheText());

        Assert.That(text, Is.EqualTo("I am an element inside the Shadow DOM"));
    }
}