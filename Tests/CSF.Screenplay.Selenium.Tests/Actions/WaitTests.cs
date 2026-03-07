
using System;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Elements;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using static CSF.Screenplay.PerformanceStarter;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Selenium.Actions;

[TestFixture, Parallelizable]
public class WaitTests
{
    static readonly ITarget
        delayTimer = new ElementId("delay", "the delay timer"),
        clickableButton = new ElementId("clickable", "the clickable button"),
        displayText = new ElementId("display", "the displayable text"),
        displayTextSpan = new CssSelector("#display span", "the span within the displayable text");

    static readonly NamedUri testPage = new NamedUri("WaitTests.html", "the test page");

    static int GetDelayMilliseconds(Actor actor)
        => actor.GetAbility<BrowseTheWeb>().WebDriver.Unproxy() is RemoteWebDriver ? 5000 : 2000;

    static int GetSufficientWaitMilliseconds(Actor actor)
        => actor.GetAbility<BrowseTheWeb>().WebDriver.Unproxy() is RemoteWebDriver ? 9000 : 4000;

    static int GetInsufficientWaitMilliseconds(Actor actor) => 500;

    [Test, Screenplay]
    public async Task WaitingForSufficientTimeShouldSucceed(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        await Given(webster).WasAbleTo(EnterTheText(GetDelayMilliseconds(webster).ToString()).Into(delayTimer));
        await When(webster).AttemptsTo(ClickOn(clickableButton));
        await Then(webster).Should(WaitUntil(displayText.Has().Text($"Clicked, and {GetDelayMilliseconds(webster)}ms has elapsed"))
            .ForAtMost(TimeSpan.FromMilliseconds(GetSufficientWaitMilliseconds(webster)))
            );
        var contents = await Then(webster).Should(ReadFromTheElement(displayText).TheText());

        Assert.That(contents, Is.EqualTo($"Clicked, and {GetDelayMilliseconds(webster)}ms has elapsed"));
    }

    [Test, Screenplay]
    public async Task WaitingForSufficientTimeWithIgnoredExceptionsShouldSucceed(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        await Given(webster).WasAbleTo(EnterTheText(GetDelayMilliseconds(webster).ToString()).Into(delayTimer));
        await When(webster).AttemptsTo(ClickOn(clickableButton));
        await Then(webster).Should(WaitUntil(displayTextSpan.Has().Text(GetDelayMilliseconds(webster).ToString()))
            .ForAtMost(TimeSpan.FromMilliseconds(GetSufficientWaitMilliseconds(webster)))
            .IgnoringTheseExceptionTypes(typeof(TargetNotFoundException))
            );
        var contents = await Then(webster).Should(ReadFromTheElement(displayText).TheText());

        Assert.That(contents, Is.EqualTo($"Clicked, and {GetDelayMilliseconds(webster)}ms has elapsed"));
    }

    [Test, Screenplay]
    public async Task WaitingForSufficientTimeWithoutIgnoredExceptionsShouldSucceed(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        await Given(webster).WasAbleTo(EnterTheText(GetDelayMilliseconds(webster).ToString()).Into(delayTimer));
        await When(webster).AttemptsTo(ClickOn(clickableButton));
        await Then(webster).Should(WaitUntil(displayTextSpan.Has().Text(GetDelayMilliseconds(webster).ToString()))
            .ForAtMost(TimeSpan.FromMilliseconds(GetSufficientWaitMilliseconds(webster)))
            // No ignored exceptions specified here, but the default behaviour will ignore TargetNotFoundException
            );
        var contents = await Then(webster).Should(ReadFromTheElement(displayText).TheText());

        Assert.That(contents, Is.EqualTo($"Clicked, and {GetDelayMilliseconds(webster)}ms has elapsed"));
    }

    [Test, Screenplay]
    public async Task WaitingForSufficientTimeWithEmptyIgnoredExceptionsShouldThrow(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        await Given(webster).WasAbleTo(EnterTheText(GetDelayMilliseconds(webster).ToString()).Into(delayTimer));
        await When(webster).AttemptsTo(ClickOn(clickableButton));
        Assert.That(async () => await Then(webster).Should(WaitUntil(displayTextSpan.Has().Text(GetDelayMilliseconds(webster).ToString()))
            .ForAtMost(TimeSpan.FromMilliseconds(GetSufficientWaitMilliseconds(webster)))
            .IgnoringTheseExceptionTypes() // Explicitly empty
            ), Throws.InstanceOf<PerformableException>());
    }

    [Test, Screenplay]
    public async Task WaitingForInsufficientTimeShouldThrow(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        await Given(webster).WasAbleTo(EnterTheText(GetDelayMilliseconds(webster).ToString()).Into(delayTimer));
        await When(webster).AttemptsTo(ClickOn(clickableButton));

        Assert.That(async () => await Then(webster).Should(WaitUntil(displayText.Has().Text($"Clicked, and {GetDelayMilliseconds(webster)}ms has elapsed"))
                                                            .ForAtMost(TimeSpan.FromMilliseconds(GetInsufficientWaitMilliseconds(webster)))),
                    Throws.InstanceOf<PerformableException>().And.InnerException.TypeOf<WebDriverTimeoutException>());
    }

    [Test, Screenplay]
    public async Task WaitingForSufficientTimeUsingDefaultWaitAbilityShouldSucceed(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();
        webster.IsAbleTo(new UseADefaultWaitTime(TimeSpan.FromMilliseconds(GetSufficientWaitMilliseconds(webster))));

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        await Given(webster).WasAbleTo(EnterTheText(GetDelayMilliseconds(webster).ToString()).Into(delayTimer));
        await When(webster).AttemptsTo(ClickOn(clickableButton));
        await Then(webster).Should(WaitUntil(displayText.Has().Text($"Clicked, and {GetDelayMilliseconds(webster)}ms has elapsed")));
        var contents = await Then(webster).Should(ReadFromTheElement(displayText).TheText());

        Assert.That(contents, Is.EqualTo($"Clicked, and {GetDelayMilliseconds(webster)}ms has elapsed"));
    }

    [Test, Screenplay]
    public async Task WaitingForInsufficientTimeUsingDefaultWaitAbilityShouldThrow(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();
        webster.IsAbleTo(new UseADefaultWaitTime(TimeSpan.FromMilliseconds(GetInsufficientWaitMilliseconds(webster))));

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        await Given(webster).WasAbleTo(EnterTheText(GetDelayMilliseconds(webster).ToString()).Into(delayTimer));
        await When(webster).AttemptsTo(ClickOn(clickableButton));

        Assert.That(async () => await Then(webster).Should(WaitUntil(displayText.Has().Text($"Clicked, and {GetDelayMilliseconds(webster)}ms has elapsed"))),
                    Throws.InstanceOf<PerformableException>().And.InnerException.TypeOf<WebDriverTimeoutException>());
    }

    [Test, Screenplay]
    public async Task WaitingForSufficientTimeWithoutAPredicateShouldSucceed(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        await Given(webster).WasAbleTo(EnterTheText(GetDelayMilliseconds(webster).ToString()).Into(delayTimer));
        await When(webster).AttemptsTo(ClickOn(clickableButton));
        await Then(webster).Should(WaitFor(TimeSpan.FromMilliseconds(GetSufficientWaitMilliseconds(webster))));
        var contents = await Then(webster).Should(ReadFromTheElement(displayText).TheText());

        Assert.That(contents, Is.EqualTo($"Clicked, and {GetDelayMilliseconds(webster)}ms has elapsed"));
    }

    [Test, Screenplay]
    public async Task WaitingForInsufficientTimeWithoutAPredicateShouldYieldIncorrectResult(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        await Given(webster).WasAbleTo(EnterTheText(GetDelayMilliseconds(webster).ToString()).Into(delayTimer));
        await When(webster).AttemptsTo(ClickOn(clickableButton));
        await Then(webster).Should(WaitFor(TimeSpan.FromMilliseconds(GetInsufficientWaitMilliseconds(webster))));
        var contents = await Then(webster).Should(ReadFromTheElement(displayText).TheText());

        Assert.That(contents, Is.Not.EqualTo($"Clicked, and {GetDelayMilliseconds(webster)}ms has elapsed"));
    }
}