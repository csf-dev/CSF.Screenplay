
using CSF.Screenplay.Selenium.Elements;
using static CSF.Screenplay.PerformanceStarter;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Selenium.Actions;

[TestFixture, Parallelizable]
public class ClearCookiesTests
{
    static readonly ITarget
        listCookiesButton = new ElementId("getCookies", "the cookies-listing button"),
        cookieList = new ElementId("cookiesList", "the list of cookies");

    static readonly NamedUri testPage = new NamedUri("ClearCookiesTests.html", "the test page");

    [Test, Screenplay]
    public async Task ClearCookiesShouldClearCookies(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        await When(webster).AttemptsTo(ClearAllDomainCookies());
        await Then(webster).Should(ClickOn(listCookiesButton));
        var cookies = await Then(webster).Should(ReadFromTheElement(cookieList).TheText());

        Assert.That(cookies, Is.Empty);
    }
}