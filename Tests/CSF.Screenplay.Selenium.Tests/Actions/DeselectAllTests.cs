
using CSF.Screenplay.Selenium.Elements;
using static CSF.Screenplay.PerformanceStarter;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Selenium.Actions;

[TestFixture, Parallelizable]
public class DeselectAllTests
{
    static readonly ITarget
        selectElement = new ElementId("selectElement", "the select element"),
        displayText = new ElementId("display", "the displayable text");

    static readonly NamedUri testPage = new NamedUri("DeselectionTests.html", "the test page");

    [Test, Screenplay]
    public async Task DeselectEverythingFromShouldClearTheSelection(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        await When(webster).AttemptsTo(DeselectEverythingFrom(selectElement));
        var contents = await Then(webster).Should(ReadFromTheElement(displayText).TheText());

        Assert.That(contents, Is.Empty);
    }
}