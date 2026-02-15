using CSF.Screenplay.Selenium.Elements;
using static CSF.Screenplay.PerformanceStarter;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Selenium.Actions;

[TestFixture, Parallelizable, Category("WebDriver")]
public class SendKeysTests
{
    static readonly ITarget
        inputArea = new ElementId("inputArea", "the input area"),
        displayText = new ElementId("display", "the displayable text");

    static readonly NamedUri testPage = new NamedUri("SendKeysTests.html", "the test page");

    [Test, Screenplay]
    public async Task SendingKeysToAnInputAreaShouldUpdateTheDisplay(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        await When(webster).AttemptsTo(EnterTheText("Hello World").Into(inputArea));
        var contents = await Then(webster).Should(ReadFromTheElement(displayText).TheText());

        Assert.That(contents, Is.EqualTo("Hello World"));
    }
}