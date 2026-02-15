
using CSF.Screenplay.Selenium.Elements;
using static CSF.Screenplay.PerformanceStarter;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Selenium.Actions;

[TestFixture, Parallelizable, Category("WebDriver")]
public class DeselectByIndexTests
{
    static readonly ITarget
        selectElement = new ElementId("selectElement", "the select element"),
        displayText = new ElementId("display", "the displayable text");

    static readonly NamedUri testPage = new NamedUri("DeselectionTests.html", "the test page");

    [Test, Screenplay]
    public async Task DeselectTheOptionFromShouldLeaveOneSelectedItem(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        await When(webster).AttemptsTo(DeselectTheOption(0).From(selectElement));
        var contents = await Then(webster).Should(ReadFromTheElement(displayText).TheText());

        Assert.That(contents, Is.EqualTo("Third"));
    }
}
