using CSF.Screenplay.Selenium.Elements;
using OpenQA.Selenium;
using static CSF.Screenplay.PerformanceStarter;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Selenium.Questions;

[TestFixture, Parallelizable]
public class GetLogsNativelyTests
{
    static readonly Locator
        theDebugButton = new ElementId("debugMessage"),
        theInfoButton = new ElementId("infoMessage");
    static readonly NamedUri testPage = new NamedUri("GetTheLogsTests.html", "the test page");

    [Test, Screenplay]
    public async Task ClickingTheInfoMessageShouldRecordAMessageWhichMayBeRetrieved(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();
        var ability = webster.GetAbility<BrowseTheWeb>();
        if(!ability.WebDriver.HasQuirk(BrowserQuirks.HasNativeLogsSupport)) Assert.Pass("This test can't be run on the current WebDriver");
        if(ability.WebDriver.Unproxy() is OpenQA.Selenium.Remote.RemoteWebDriver)
            Assert.Pass("This test can't be run on the remote WebDrivers, due to limitations which mean that native logs are not returned");

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        await When(webster).AttemptsTo(ClickOn(theInfoButton));

        var logs = await Then(webster).Should(GetNativeBrowserLogs());

        Assert.That(logs, Has.One.Matches<BrowserLog>(x => x.Message.Contains("The info button has been clicked")));
    }

    [Test, Screenplay]
    public async Task ClickingTheInfoMessageTwiceShouldRecordTwoMessagesWhichMayBeRetrieved(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();
        var ability = webster.GetAbility<BrowseTheWeb>();
        if(!ability.WebDriver.HasQuirk(BrowserQuirks.HasNativeLogsSupport)) Assert.Pass("This test can't be run on the current WebDriver");
        if(ability.WebDriver.Unproxy() is OpenQA.Selenium.Remote.RemoteWebDriver)
            Assert.Pass("This test can't be run on the remote WebDrivers, due to limitations which mean that native logs are not returned");

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        await When(webster).AttemptsTo(ClickOn(theInfoButton));
        await When(webster).AttemptsTo(ClickOn(theInfoButton));

        var logs = await Then(webster).Should(GetNativeBrowserLogs());

        using var multiple = Assert.EnterMultipleScope();

        Assert.That(logs, Has.Exactly(2).Matches<BrowserLog>(x => x.Message.Contains("The info button has been clicked")));
    }

    [Test, Screenplay]
    public async Task GettingLogsTwiceDoesNotReturnDuplicateMessages(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();
        var ability = webster.GetAbility<BrowseTheWeb>();
        if(!ability.WebDriver.HasQuirk(BrowserQuirks.HasNativeLogsSupport)) Assert.Pass("This test can't be run on the current WebDriver");
        if(ability.WebDriver.Unproxy() is OpenQA.Selenium.Remote.RemoteWebDriver)
            Assert.Pass("This test can't be run on the remote WebDrivers, due to limitations which mean that native logs are not returned");

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        await When(webster).AttemptsTo(ClickOn(theInfoButton));
        await When(webster).AttemptsTo(ClickOn(theInfoButton));

        var firstLogs = await Then(webster).Should(GetNativeBrowserLogs());
        var secondLogs = await Then(webster).Should(GetNativeBrowserLogs());

        using var multiple = Assert.EnterMultipleScope();

        Assert.That(firstLogs, Has.Count.AtLeast(2), "First set of logs has messages");
        Assert.That(secondLogs, Is.Empty, "Second set of logs is empty");
    }

    [Test, Screenplay]
    public async Task ClickingTheDebugMessageShouldNotRecordAMessage(IStage stage)
    {
        /* Note that this test requires the `BrowserLogLevel` in the `appsettings.json` config file for the current WebDriver
         * to be set to `Info`.  That will filter Debug-level messages out from the log.
         */
        var webster = stage.Spotlight<Webster>();
        var ability = webster.GetAbility<BrowseTheWeb>();
        if(!ability.WebDriver.HasQuirk(BrowserQuirks.HasNativeLogsSupport)) Assert.Pass("This test can't be run on the current WebDriver");
        if(ability.WebDriver.Unproxy() is OpenQA.Selenium.Remote.RemoteWebDriver)
            Assert.Pass("This test can't be run on the remote WebDrivers, due to limitations which mean that native logs are not returned");

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        await When(webster).AttemptsTo(ClickOn(theDebugButton));

        var logs = await Then(webster).Should(GetNativeBrowserLogs());

        Assert.That(logs, Has.None.Matches<BrowserLog>(x => x.Message.Contains("debug button")));
    }
}