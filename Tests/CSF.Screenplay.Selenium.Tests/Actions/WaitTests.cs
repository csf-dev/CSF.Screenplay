
using System;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Elements;
using static CSF.Screenplay.PerformanceStarter;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Selenium.Actions;

[TestFixture]
public class WaitTests
{
    static readonly ITarget
        delayTimer = new ElementId("delay", "the delay timer"),
        clickableButton = new ElementId("clickable", "the clickable button"),
        displayText = new ElementId("display", "the displayable text");

    static readonly NamedUri testPage = new NamedUri("WaitTests.html", "the test page");

    [Test, Screenplay]
    public async Task WaitingForSufficientTimeShouldSucceed(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        await Given(webster).WasAbleTo(EnterTheText("250").Into(delayTimer));
        await When(webster).AttemptsTo(ClickOn(clickableButton));
        await Then(webster).Should(WaitUntil(displayText.Has().TextEqualTo("Clicked, and 250ms has elapsed")).ForAtMost(TimeSpan.FromMilliseconds(500)).WithPollingInterval(TimeSpan.FromMilliseconds(150)));
        var contents = await Then(webster).Should(ReadFromTheElement(displayText).TheText());

        Assert.That(contents, Is.EqualTo("Clicked, and 250ms has elapsed"));
    }

    [Test, Screenplay]
    public async Task WaitingForInsufficientTimeShouldThrow(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        await Given(webster).WasAbleTo(EnterTheText("2000").Into(delayTimer));
        await When(webster).AttemptsTo(ClickOn(clickableButton));

        Assert.That(async () => await Then(webster).Should(WaitUntil(displayText.Has().TextEqualTo("Clicked, and 2000ms has elapsed")).ForAtMost(TimeSpan.FromMilliseconds(500))),
                    Throws.InstanceOf<PerformableException>().And.InnerException.TypeOf<OpenQA.Selenium.WebDriverTimeoutException>());
    }

    [Test, Screenplay]
    public async Task WaitingForSufficientTimeUsingDefaultWaitAbilityShouldSucceed(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();
        webster.IsAbleTo(new UseADefaultWaitTime(TimeSpan.FromMilliseconds(500)));

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        await Given(webster).WasAbleTo(EnterTheText("250").Into(delayTimer));
        await When(webster).AttemptsTo(ClickOn(clickableButton));
        await Then(webster).Should(WaitUntil(displayText.Has().TextEqualTo("Clicked, and 250ms has elapsed")));
        var contents = await Then(webster).Should(ReadFromTheElement(displayText).TheText());

        Assert.That(contents, Is.EqualTo("Clicked, and 250ms has elapsed"));
    }

    [Test, Screenplay]
    public async Task WaitingForInsufficientTimeUsingDefaultWaitAbilityShouldThrow(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();
        webster.IsAbleTo(new UseADefaultWaitTime(TimeSpan.FromMilliseconds(100)));

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        await Given(webster).WasAbleTo(EnterTheText("2000").Into(delayTimer));
        await When(webster).AttemptsTo(ClickOn(clickableButton));

        Assert.That(async () => await Then(webster).Should(WaitUntil(displayText.Has().TextEqualTo("Clicked, and 2000ms has elapsed"))),
                    Throws.InstanceOf<PerformableException>().And.InnerException.TypeOf<OpenQA.Selenium.WebDriverTimeoutException>());
    }

    [Test, Screenplay]
    public async Task WaitingForSufficientTimeWithoutAPredicateShouldSucceed(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        await Given(webster).WasAbleTo(EnterTheText("250").Into(delayTimer));
        await When(webster).AttemptsTo(ClickOn(clickableButton));
        await Then(webster).Should(WaitFor(TimeSpan.FromMilliseconds(300)));
        var contents = await Then(webster).Should(ReadFromTheElement(displayText).TheText());

        Assert.That(contents, Is.EqualTo("Clicked, and 250ms has elapsed"));
    }

    [Test, Screenplay]
    public async Task WaitingForInsufficientTimeWithoutAPredicateShouldYieldIncorrectResult(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        await Given(webster).WasAbleTo(EnterTheText("250").Into(delayTimer));
        await When(webster).AttemptsTo(ClickOn(clickableButton));
        await Then(webster).Should(WaitFor(TimeSpan.FromMilliseconds(10)));
        var contents = await Then(webster).Should(ReadFromTheElement(displayText).TheText());

        Assert.That(contents, Is.Not.EqualTo("Clicked, and 250ms has elapsed"));
    }
}