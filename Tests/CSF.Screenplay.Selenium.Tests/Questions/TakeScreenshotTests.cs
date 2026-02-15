using static CSF.Screenplay.PerformanceStarter;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Selenium.Questions;

[TestFixture, Parallelizable, Category("WebDriver")]
public class TakeScreenshotTests
{
    static readonly NamedUri testPage = new NamedUri("OpenUrlTests.html", "the test page");

    [Test, Screenplay]
    public async Task TakeAScreenshotShouldGetASeleniumScreenshot(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        var screenshot = await When(webster).AttemptsTo(TakeAScreenshot());

        Assert.That(screenshot, Is.Not.Null);
    }
}