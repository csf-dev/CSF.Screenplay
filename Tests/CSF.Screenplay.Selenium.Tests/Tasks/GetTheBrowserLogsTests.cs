using System;
using CSF.Extensions.WebDriver;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Elements;
using CSF.Screenplay.Selenium.Questions;
using Moq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using static CSF.Screenplay.PerformanceStarter;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Selenium.Tasks;

[TestFixture, Parallelizable]
public class GetTheBrowserLogsTests
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
        if(ability.WebDriver.Unproxy() is OpenQA.Selenium.Remote.RemoteWebDriver)
            Assert.Pass("This test can't be run on the remote WebDrivers, due to limitations which mean that native logs are not returned");
        if(!ability.WebDriver.HasQuirk(BrowserQuirks.HasNativeLogsSupport) && !ability.WebDriver.HasQuirk(BrowserQuirks.CanGetLogsWithJavascriptWorkaround))
            Assert.Pass("This test can't be run on the current WebDriver");

        await Given(webster).WasAbleTo(NavigateTo(testPage));
        await When(webster).AttemptsTo(ClickOn(theInfoButton));

        var logs = await Then(webster).Should(GetTheBrowserLogs());

        Assert.That(logs, Has.One.Matches<BrowserLog>(x => x.Message.Contains("The info button has been clicked")));
    }

    [Test, Screenplay]
    public async Task ClickingTheInfoMessageTwiceShouldRecordTwoMessagesWhichMayBeRetrieved(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();
        var ability = webster.GetAbility<BrowseTheWeb>();
        if(ability.WebDriver.Unproxy() is OpenQA.Selenium.Remote.RemoteWebDriver)
            Assert.Pass("This test can't be run on the remote WebDrivers, due to limitations which mean that native logs are not returned");
        if(!ability.WebDriver.HasQuirk(BrowserQuirks.HasNativeLogsSupport) && !ability.WebDriver.HasQuirk(BrowserQuirks.CanGetLogsWithJavascriptWorkaround))
            Assert.Pass("This test can't be run on the current WebDriver");

        await Given(webster).WasAbleTo(NavigateTo(testPage));
        await When(webster).AttemptsTo(ClickOn(theInfoButton));
        await When(webster).AttemptsTo(ClickOn(theInfoButton));

        var logs = await Then(webster).Should(GetTheBrowserLogs());

        using var multiple = Assert.EnterMultipleScope();

        Assert.That(logs, Has.Exactly(2).Matches<BrowserLog>(x => x.Message.Contains("The info button has been clicked")));
    }

    [Test, Screenplay]
    public async Task GettingLogsTwiceDoesNotReturnDuplicateMessages(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();
        var ability = webster.GetAbility<BrowseTheWeb>();
        if(ability.WebDriver.Unproxy() is OpenQA.Selenium.Remote.RemoteWebDriver)
            Assert.Pass("This test can't be run on the remote WebDrivers, due to limitations which mean that native logs are not returned");
        if(!ability.WebDriver.HasQuirk(BrowserQuirks.HasNativeLogsSupport) && !ability.WebDriver.HasQuirk(BrowserQuirks.CanGetLogsWithJavascriptWorkaround))
            Assert.Pass("This test can't be run on the current WebDriver");

        await Given(webster).WasAbleTo(NavigateTo(testPage));
        await When(webster).AttemptsTo(ClickOn(theInfoButton));
        await When(webster).AttemptsTo(ClickOn(theInfoButton));

        var firstLogs = await Then(webster).Should(GetTheBrowserLogs());
        var secondLogs = await Then(webster).Should(GetTheBrowserLogs());

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

        await Given(webster).WasAbleTo(NavigateTo(testPage));
        await When(webster).AttemptsTo(ClickOn(theDebugButton));

        var logs = await Then(webster).Should(GetTheBrowserLogs());

        Assert.That(logs, Has.None.Matches<BrowserLog>(x => x.Message.Contains("debug button")));
    }

    [Test, Screenplay]
    public async Task GettingLogsShouldThrowIfTheBrowserDoesNotSupportIt(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();
        var ability = webster.GetAbility<BrowseTheWeb>();
        if(ability.WebDriver.HasQuirk(BrowserQuirks.HasNativeLogsSupport) || ability.WebDriver.HasQuirk(BrowserQuirks.CanGetLogsWithJavascriptWorkaround))
            Assert.Pass("This test can't be run on the current WebDriver");
        if(ability.WebDriver.Unproxy() is OpenQA.Selenium.Remote.RemoteWebDriver)
            Assert.Pass("This test can't be run on the remote WebDrivers, due to limitations which mean that native logs are not returned");

        await Given(webster).WasAbleTo(NavigateTo(testPage));
        await When(webster).AttemptsTo(ClickOn(theInfoButton));

        Assert.That(async () => await Then(webster).Should(GetTheBrowserLogs()), Throws.InstanceOf<PerformableException>());
    }

    [Test, Screenplay]
    public async Task GettingLogsShouldReturnAnEmptyCollectionForAnUnsupportedBrowserIfInstructedNotToThrow(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();
        var ability = webster.GetAbility<BrowseTheWeb>();
        if(ability.WebDriver.HasQuirk(BrowserQuirks.HasNativeLogsSupport) || ability.WebDriver.HasQuirk(BrowserQuirks.CanGetLogsWithJavascriptWorkaround))
            Assert.Pass("This test can't be run on the current WebDriver");
        if(ability.WebDriver.Unproxy() is OpenQA.Selenium.Remote.RemoteWebDriver)
            Assert.Pass("This test can't be run on the remote WebDrivers, due to limitations which mean that native logs are not returned");

        await Given(webster).WasAbleTo(NavigateTo(testPage));
        await When(webster).AttemptsTo(ClickOn(theInfoButton));

        var logs = await Then(webster).Should(GetTheBrowserLogs().ButReturnEmptyLogsIfUnsupported());

        Assert.That(logs, Is.Empty);
    }

    [Test, AutoMoqData]
    public void GettingLogsShouldThrowIfConfiguredToDoSoWhenUsedWithARemoteWebDriver(Actor actor, IGetsWebDriver driverFactory, [StubRemote] RemoteWebDriver driver)
    {
        var sut = new GetTheBrowserLogs(true);
        Mock.Get(driverFactory).Setup(x => x.GetDefaultWebDriver(null)).Returns(new Extensions.WebDriver.Factories.WebDriverAndOptions(driver, new ChromeOptions()));
        var browseTheWeb = new BrowseTheWeb(driverFactory);
        actor.IsAbleTo(browseTheWeb);

        Assert.That(async () => await sut.PerformAsAsync(actor), Throws.InstanceOf<NotSupportedException>().And.Message.Contains("not supported for Remote Web Drivers"));
    }

    [Test, AutoMoqData]
    public void GettingLogsShouldReturnEmptyIfConfiguredToDoSoWhenUsedWithARemoteWebDriver(Actor actor, IGetsWebDriver driverFactory, [StubRemote] RemoteWebDriver driver)
    {
        var sut = new GetTheBrowserLogs(false);
        Mock.Get(driverFactory).Setup(x => x.GetDefaultWebDriver(null)).Returns(new Extensions.WebDriver.Factories.WebDriverAndOptions(driver, new ChromeOptions()));
        var browseTheWeb = new BrowseTheWeb(driverFactory);
        actor.IsAbleTo(browseTheWeb);

        Assert.That(async () => await sut.PerformAsAsync(actor), Is.Empty);
    }


    [Test, AutoMoqData]
    public void GettingLogsShouldThrowIfConfiguredToDoSoWhenUsedWithADriverThatCannotGetLogs(Actor actor, IGetsWebDriver driverFactory, IWebDriver driver)
    {
        var sut = new GetTheBrowserLogs(true);
        Mock.Get(driverFactory).Setup(x => x.GetDefaultWebDriver(null)).Returns(new Extensions.WebDriver.Factories.WebDriverAndOptions(driver, new ChromeOptions()));
        var browseTheWeb = new BrowseTheWeb(driverFactory);
        actor.IsAbleTo(browseTheWeb);

        Assert.That(async () => await sut.PerformAsAsync(actor), Throws.InstanceOf<NotSupportedException>().And.Message.Contains("does not support retrieving console logs"));
    }

    [Test, AutoMoqData]
    public void GettingLogsShouldReturnEmptyIfConfiguredToDoSoWhenUsedWithADriverThatCannotGetLogs(Actor actor, IGetsWebDriver driverFactory, IWebDriver driver)
    {
        var sut = new GetTheBrowserLogs(false);
        Mock.Get(driverFactory).Setup(x => x.GetDefaultWebDriver(null)).Returns(new Extensions.WebDriver.Factories.WebDriverAndOptions(driver, new ChromeOptions()));
        var browseTheWeb = new BrowseTheWeb(driverFactory);
        actor.IsAbleTo(browseTheWeb);

        Assert.That(async () => await sut.PerformAsAsync(actor), Is.Empty);
    }
}