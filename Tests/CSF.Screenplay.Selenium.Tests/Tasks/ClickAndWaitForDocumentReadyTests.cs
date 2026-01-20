using System;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Elements;
using OpenQA.Selenium;
using static CSF.Screenplay.PerformanceStarter;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Selenium.Tasks;

[TestFixture]
public class ClickAndWaitForDocumentReadyTests
{
    static readonly NamedUri startPage = new NamedUri("DelayedNavigation.html", "the test page");
    static readonly ITarget
        link = new ElementId("clickable"),
        displayText = new ElementId("textContent");

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

        if(ability.DriverOptions.BrowserName == "chrome")
            Assert.Pass("This test cannot meaningfully be run on a Chrome browser, because it always waits for the page load; treating as an implicit pass");

        await Given(webster).WasAbleTo(OpenTheUrl(startPage));
        
        Assert.That(async () => await When(webster).AttemptsTo(ClickOn(link).AndWaitForANewPageToLoad(TimeSpan.FromMilliseconds(200))),
                    Throws.InstanceOf<PerformableException>().And.InnerException.InstanceOf<WebDriverException>());
    }
}