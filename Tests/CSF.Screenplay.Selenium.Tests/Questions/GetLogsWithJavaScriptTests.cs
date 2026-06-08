using System;
using CSF.Screenplay.Selenium.Elements;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
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
        var actor = PickBestActor(stage);
        if(!actor.GetAbility<BrowseTheWeb>().WebDriver.HasQuirk(BrowserQuirks.CanGetLogsWithJavascriptWorkaround))
            Assert.Pass("This test cannot be performed with the current WebDriver");

        await Given(actor).WasAbleTo(NavigateTo(testPage));
        await When(actor).AttemptsTo(ClickOn(theInfoButton));

        var logs = await Then(actor).Should(GetBrowserLogsWithJavascript());

        Assert.That(logs, Has.One.Matches<BrowserLog>(x => x.Message.Contains("The info button has been clicked")));
    }

    [Test, Screenplay]
    public async Task ClickingTheInfoMessageTwiceShouldRecordTwoMessagesWhichMayBeRetrieved(IStage stage)
    {
        var actor = PickBestActor(stage);
        if(!actor.GetAbility<BrowseTheWeb>().WebDriver.HasQuirk(BrowserQuirks.CanGetLogsWithJavascriptWorkaround))
            Assert.Pass("This test cannot be performed with the current WebDriver");

        await Given(actor).WasAbleTo(NavigateTo(testPage));
        await When(actor).AttemptsTo(ClickOn(theInfoButton));
        await When(actor).AttemptsTo(ClickOn(theInfoButton));

        var logs = await Then(actor).Should(GetBrowserLogsWithJavascript());

        using var multiple = Assert.EnterMultipleScope();

        Assert.That(logs, Has.Exactly(2).Matches<BrowserLog>(x => x.Message.Contains("The info button has been clicked")));
    }

    [Test, Screenplay]
    public async Task GettingLogsTwiceDoesNotReturnDuplicateMessages(IStage stage)
    {
        var actor = PickBestActor(stage);
        if(!actor.GetAbility<BrowseTheWeb>().WebDriver.HasQuirk(BrowserQuirks.CanGetLogsWithJavascriptWorkaround))
            Assert.Pass("This test cannot be performed with the current WebDriver");

        await Given(actor).WasAbleTo(NavigateTo(testPage));
        await When(actor).AttemptsTo(ClickOn(theInfoButton));
        await When(actor).AttemptsTo(ClickOn(theInfoButton));

        var firstLogs = await Then(actor).Should(GetBrowserLogsWithJavascript());
        var secondLogs = await Then(actor).Should(GetBrowserLogsWithJavascript());

        using var multiple = Assert.EnterMultipleScope();

        Assert.That(firstLogs, Has.Count.AtLeast(2), "First set of logs has messages");
        Assert.That(secondLogs, Is.Empty, "Second set of logs is empty");
    }

    /// <summary>
    /// Picks the best actor to participate in this test.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Webster is the best actor when we are using Remote Web Drivers, but Fox is the best for local drivers.
    /// Remote/Local is controlled outside of this test, by configuration for the web driver factory.
    /// </para>
    /// </remarks>
    /// <param name="stage">The stage</param>
    /// <returns>The best actor for the test.</returns>
    static Actor PickBestActor(IStage stage)
    {
        var webster = stage.Cast.GetActor<Webster>();
        var browseTheWeb = webster.GetAbility<BrowseTheWeb>();
        if(browseTheWeb.WebDriver.Unproxy() is RemoteWebDriver) return webster;

        return stage.Cast.GetActor<Fox>();
    }
}