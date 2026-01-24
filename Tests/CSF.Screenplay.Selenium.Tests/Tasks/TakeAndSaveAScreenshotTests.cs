using CSF.Screenplay.Actors;
using static CSF.Screenplay.PerformanceStarter;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Selenium.Questions;

[TestFixture, Parallelizable]
public class TakeAndSaveAScreenshotTests
{
    static readonly NamedUri testPage = new NamedUri("OpenUrlTests.html", "the test page");

    string? screenshotPath;

    [Test, Screenplay]
    public async Task TakeAndSaveAScreenshotShouldSaveAFile(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        try
        {
            webster.RecordsAsset += OnRecordsAsset;

            await Given(webster).WasAbleTo(OpenTheUrl(testPage));
            await When(webster).AttemptsTo(TakeAndSaveAScreenshot().WithTheName("test screenshot"));
        }
        finally
        {
            webster.RecordsAsset -= OnRecordsAsset;
        }

        Assert.Multiple(() =>
        {
            Assert.That(screenshotPath, Is.Not.Null);
            Assert.That(screenshotPath, Does.Exist);
        });
    }

    void OnRecordsAsset(object? sender, PerformableAssetEventArgs e)
    {
        screenshotPath = e.FilePath;
    }
}