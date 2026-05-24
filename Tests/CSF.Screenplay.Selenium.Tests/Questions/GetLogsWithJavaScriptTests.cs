using CSF.Screenplay.Selenium.Elements;
using OpenQA.Selenium;
using static CSF.Screenplay.PerformanceStarter;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Selenium.Questions;

[TestFixture, Parallelizable]
public class GetLogsWithJavaScriptTests
{
    static readonly Locator theInfoButton = new ElementId("infoMessage");
    static readonly NamedUri testPage = new NamedUri("GetTheLogsTests.html", "the test page");

    [Test, Screenplay]
    public async Task ClickingTheInfoMessageShouldRecordAMessageWhichMayBeRetrieved(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();
        var ability = webster.GetAbility<BrowseTheWeb>();
        if(!ability.WebDriver.HasQuirk(BrowserQuirks.CanGetLogsWithJavascriptWorkaround)) Assert.Pass("This test can't be run on the current WebDriver");
        if(ability.WebDriver.Unproxy() is OpenQA.Selenium.Remote.RemoteWebDriver)
            Assert.Pass("This test can't be run on the remote WebDrivers, due to limitations which mean that native logs are not returned");

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        await When(webster).AttemptsTo(ClickOn(theInfoButton));

        var logs = await Then(webster).Should(GetBrowserLogsWithJavascript());

        Assert.That(logs, Has.One.Matches<BrowserLog>(x => x.Message.Contains("The info button has been clicked")));
    }

    [Test, Screenplay]
    public async Task ClickingTheInfoMessageTwiceShouldRecordTwoMessagesWhichMayBeRetrieved(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();
        var ability = webster.GetAbility<BrowseTheWeb>();
        if(!ability.WebDriver.HasQuirk(BrowserQuirks.CanGetLogsWithJavascriptWorkaround)) Assert.Pass("This test can't be run on the current WebDriver");
        if(ability.WebDriver.Unproxy() is OpenQA.Selenium.Remote.RemoteWebDriver)
            Assert.Pass("This test can't be run on the remote WebDrivers, due to limitations which mean that native logs are not returned");

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        await When(webster).AttemptsTo(ClickOn(theInfoButton));
        await When(webster).AttemptsTo(ClickOn(theInfoButton));

        var logs = await Then(webster).Should(GetBrowserLogsWithJavascript());

        using var multiple = Assert.EnterMultipleScope();

        Assert.That(logs, Has.Exactly(2).Matches<BrowserLog>(x => x.Message.Contains("The info button has been clicked")));
    }

    [Test, Screenplay]
    public async Task GettingLogsTwiceDoesNotReturnDuplicateMessages(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();
        var ability = webster.GetAbility<BrowseTheWeb>();
        if(!ability.WebDriver.HasQuirk(BrowserQuirks.CanGetLogsWithJavascriptWorkaround)) Assert.Pass("This test can't be run on the current WebDriver");
        if(ability.WebDriver.Unproxy() is OpenQA.Selenium.Remote.RemoteWebDriver)
            Assert.Pass("This test can't be run on the remote WebDrivers, due to limitations which mean that native logs are not returned");

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        await When(webster).AttemptsTo(ClickOn(theInfoButton));
        await When(webster).AttemptsTo(ClickOn(theInfoButton));

        var firstLogs = await Then(webster).Should(GetBrowserLogsWithJavascript());
        var secondLogs = await Then(webster).Should(GetBrowserLogsWithJavascript());

        using var multiple = Assert.EnterMultipleScope();

        Assert.That(firstLogs, Has.Count.AtLeast(2), "First set of logs has messages");
        Assert.That(secondLogs, Is.Empty, "Second set of logs is empty");
    }
}