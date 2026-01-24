using static CSF.Screenplay.PerformanceStarter;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Selenium.Questions;

[TestFixture, Parallelizable]
public class GetWindowTitleTests
{
    static readonly NamedUri testPage = new NamedUri("OpenUrlTests.html", "the test page");

    [Test, Screenplay]
    public async Task GetWindowTitleShouldGetTheCorrectResult(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        var title = await When(webster).AttemptsTo(ReadTheWindowTitle());

        Assert.That(title, Is.EqualTo("Open a URL test page"));
    }
}