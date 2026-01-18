using CSF.Screenplay.Selenium.Elements;
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
        await When(webster).AttemptsTo(ClickAndWaitForDocumentReady(link));
        var result = await Then(webster).Should(ReadFromTheElement(displayText).TheText());

        Assert.That(result, Is.EqualTo("You're finally here!"));
    }
}