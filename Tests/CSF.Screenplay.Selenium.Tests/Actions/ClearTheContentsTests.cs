
using CSF.Screenplay.Selenium.Elements;
using static CSF.Screenplay.PerformanceStarter;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Selenium.Actions;

[TestFixture]
public class ClearTheContentsTests
{
    static readonly ITarget
        aTextArea = new ElementId("aTextArea", "the textarea"),
        anInput = new ElementId("aTextInput", "the text input");

    static readonly NamedUri testPage = new NamedUri("ClearTheContentsTests.html", "the test page");

    [Test, Screenplay]
    public async Task ClearATextareaShouldEmptyIt(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        await When(webster).AttemptsTo(ClearTheContentsOf(aTextArea));
        var contents = await Then(webster).Should(ReadFromTheElement(aTextArea).TheValue());

        Assert.That(contents, Is.Empty);
    }

    [Test, Screenplay]
    public async Task ClearATextInputShouldEmptyIt(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        await When(webster).AttemptsTo(ClearTheContentsOf(anInput));
        var contents = await Then(webster).Should(ReadFromTheElement(anInput).TheValue());

        Assert.That(contents, Is.Empty);
    }
}