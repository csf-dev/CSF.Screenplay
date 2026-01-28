using System;
using System.Linq;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Elements;
using OpenQA.Selenium;
using static CSF.Screenplay.PerformanceStarter;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Selenium.Tasks;

[TestFixture, Parallelizable]
public class ClickAndWaitForDocumentReadyTests
{
    static readonly NamedUri startPage = new NamedUri("DelayedNavigation.html", "the test page");
    static readonly ITarget
        link = new ElementId("clickable"),
        displayText = new ElementId("textContent");

    static readonly string[] ignoredBrowsers = ["chrome", "MicrosoftEdge"];

    [Test, Screenplay]
    public async Task PerformAsAsyncShouldWaitSoItCanGetTheAppropriateContent(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(startPage));
        await When(webster).AttemptsTo(ClickOn(link).AndWaitForANewPageToLoad());
        var result = await Then(webster).Should(ReadFromTheElement(displayText).TheText());

        Assert.That(result, Is.EqualTo("You're finally here!"));
    }

    [Test, Screenplay]
    public async Task PerformAsAsyncShouldThrowIfWeDontWaitLongEnough(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();
        var ability = webster.GetAbility<BrowseTheWeb>();

        if(ignoredBrowsers.Contains(ability.DriverOptions.BrowserName))
            Assert.Pass("This test cannot meaningfully be run on a Chrome or Edge browser, because they always wait for the page load. Treating this test as an implicit pass.");

        await Given(webster).WasAbleTo(OpenTheUrl(startPage));
        
        Assert.That(async () => await When(webster).AttemptsTo(ClickOn(link).AndWaitForANewPageToLoad(TimeSpan.FromMilliseconds(100))),
                    Throws.InstanceOf<PerformableException>().And.InnerException.InstanceOf<WebDriverException>());
    }
}