
using CSF.Screenplay.Selenium.Elements;
using static CSF.Screenplay.PerformanceStarter;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Selenium.Actions;

[TestFixture]
public class ClickTests
{
    static readonly ITarget
        clickableButton = new ElementId("clickable", "the clickable button"),
        displayText = new ElementId("display", "the displayable text");

    static readonly NamedUri testPage = new NamedUri("ClickTests.html", "the test page");

    [Test, Screenplay]
    public async Task ClickingAButtonShouldTriggerAnEvent(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        await When(webster).AttemptsTo(ClickOn(clickableButton));
        var contents = await Then(webster).Should(ReadFromTheElement(displayText).TheText());

        Assert.That(contents, Is.EqualTo("Clicked!"));
    }
}