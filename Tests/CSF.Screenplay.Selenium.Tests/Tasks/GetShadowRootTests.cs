using System.Linq;
using CSF.Screenplay.Selenium.Elements;
using OpenQA.Selenium;
using static CSF.Screenplay.PerformanceStarter;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Selenium.Tasks;

[TestFixture, Parallelizable]
public class GetShadowRootTests
{
    static readonly Locator
        host = new ElementId("shadowHost", "The shadow host"),
        content = new CssSelector("p.content", "The content inside the Shadow DOM");

    static readonly NamedUri testPage = new NamedUri("GetShadowRoot.html", "the test page");

    [Test, Screenplay]
    public async Task GetShadowRootShouldResultInBeingAbleToReadTheShadowDomContent(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();
        var browseTheWeb = webster.GetAbility<BrowseTheWeb>();

        if (browseTheWeb.WebDriver.HasQuirk(BrowserQuirks.CannotGetShadowRoot))
            Assert.Pass("This test cannot be run on the current web browser");

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        var shadowRoot = await When(webster).AttemptsTo(GetTheShadowRootFrom(host));
        var shadowContent = await Then(webster).Should(FindAnElementWithin(shadowRoot).WhichMatches(content));
        var text = await Then(webster).Should(ReadFromTheElement(shadowContent).TheText());

        Assert.That(text, Is.EqualTo("I am an element inside the Shadow DOM"));
    }

    [Test, Screenplay]
    public async Task GetShadowRootShouldResultInBeingAbleToReadACollectionOfShadowDomContent(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();
        var browseTheWeb = webster.GetAbility<BrowseTheWeb>();

        if (browseTheWeb.WebDriver.HasQuirk(BrowserQuirks.CannotGetShadowRoot))
            Assert.Pass("This test cannot be run on the current web browser");

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        var shadowRoot = await When(webster).AttemptsTo(GetTheShadowRootFrom(host));
        var shadowContent = await Then(webster).Should(FindElementsWithin(shadowRoot).WhichMatch(content));
        var text = await Then(webster).Should(ReadFromTheCollectionOfElements(shadowContent).Text());

        using var scope = Assert.EnterMultipleScope();
        Assert.That(text, Has.Count.EqualTo(1), "Count of elements found");
        Assert.That(text.First(), Is.EqualTo("I am an element inside the Shadow DOM"), "Correct text in found element");
    }
}
