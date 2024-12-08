
using CSF.Screenplay.Selenium.Elements;
using static CSF.Screenplay.PerformanceStarter;
using static CSF.Screenplay.Selenium.SeleniumPerformableBuilder;

namespace CSF.Screenplay.Selenium.Actions;

[TestFixture,Parallelizable]
public class ClearCookiesTests
{
    [Test, Screenplay]
    public async Task ClearCookiesShouldClearCookies(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();
        var listCookiesButton = new ElementId("getCookies", "the cookies-listing button");
        var cookieList = new ElementId("cookiesList", "the list of cookies");

        await Given(webster).WasAbleTo(OpenTheUrl("ClearCookiesTests.html"));
        await When(webster).AttemptsTo(ClearAllDomainCookies());
        await Then(webster).Should(ClickOn(listCookiesButton));
        var cookies = await Then(webster).Should(ReadFromTheElement(cookieList).TheText());

        Assert.That(cookies, Is.Empty);
    }
}