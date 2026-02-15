using CSF.Screenplay.Selenium.Elements;
using static CSF.Screenplay.PerformanceStarter;
using static CSF.Screenplay.Selenium.PerformableBuilder;
using static CSF.Screenplay.Selenium.Tasks.ReadTheListItemsBuilder;

namespace CSF.Screenplay.Selenium.Actions;

[TestFixture, Parallelizable]
public class SelectByTextTests
{
    static readonly ITarget
        selectElement = new ElementId("selectElement", "the select element"),
        displayText = new ElementId("display", "the displayable text");

    static readonly NamedUri testPage = new NamedUri("SelectionTests.html", "the test page");

    [Test, Screenplay]
    public async Task SelectTheOptionFromShouldAddOneSelectedItem(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        await When(webster).AttemptsTo(SelectTheOption("Second").From(selectElement));
        var contents = await Then(webster).Should(ReadTheListItemsIn(displayText));

        Assert.That(contents, Is.EqualTo(new [] { "Second", "Third" }));
    }
}
