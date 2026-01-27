
using CSF.Screenplay.Selenium.Elements;
using static CSF.Screenplay.PerformanceStarter;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Selenium.Actions;

[TestFixture, Parallelizable]
public class OpenUrlTests
{
    static readonly ITarget
        textContent = new ElementId("textContent", "the displayable text");

    static readonly NamedUri testPage = new NamedUri("OpenUrlTests.html", "the test page");

    [Test, Screenplay]
    public async Task OpenTheUrlShouldShouldYieldExpectedContent(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        var contents = await Then(webster).Should(ReadFromTheElement(textContent).TheText());

        Assert.That(contents, Is.EqualTo("This is the page content."));
    }

    [Test, Screenplay]
    public async Task OpenTheUrlWithDifferentBasePathShouldYieldDifferentContent(IStage stage)
    {
        var pattie = stage.Spotlight<Pattie>();

        await Given(pattie).WasAbleTo(OpenTheUrl(testPage));
        var contents = await Then(pattie).Should(ReadFromTheElement(textContent).TheText());

        Assert.That(contents, Is.EqualTo("This is content at the deeper path."));
    }
}