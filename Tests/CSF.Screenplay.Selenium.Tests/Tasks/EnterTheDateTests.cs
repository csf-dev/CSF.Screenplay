using System;
using System.Globalization;
using CSF.Screenplay.Selenium.Elements;
using static CSF.Screenplay.PerformanceStarter;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Selenium.Tasks;

[TestFixture, Parallelizable]
public class EnterTheDateTests
{
    static readonly ITarget
        inputArea = new ElementId("inputArea", "the input area"),
        displayText = new ElementId("display", "the displayable text");

    static readonly NamedUri testPage = new NamedUri("InputDateTests.html", "the test page");

    static readonly string[] ignoredBrowsers = ["firefox", "safari"];

    [Test, Screenplay]
    public async Task EnteringADateShouldYieldTheCorrectValue(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        await When(webster).AttemptsTo(EnterTheDate(new DateTime(2025, 11, 12)).Into(inputArea));
        var result = await Then(webster).Should(ReadFromTheElement(displayText).TheText());

        Assert.That(result, Is.EqualTo("2025-11-12"));
    }
    
    [Test, Screenplay]
    public async Task EnteringANullDateShouldClearTheValue(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        await When(webster).AttemptsTo(EnterTheDate(null).Into(inputArea));
        var result = await Then(webster).Should(ReadFromTheElement(displayText).TheText());

        Assert.That(result, Is.EqualTo(string.Empty));
    }
    
    [Test, Screenplay]
    public async Task EnteringADateInAnUnusualCultureShouldYieldIncorrectResults(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        if(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern.StartsWith("y", StringComparison.InvariantCultureIgnoreCase))
            Assert.Inconclusive("This test can't be meaningfully run when the current culture uses Y/M/D date formatting");

       var ability = Webster.GetAbility<BrowseTheWeb>(); if(ignoredBrowsers.Contains(ability.DriverOptions.BrowserName))
            Assert.Pass("This test cannot meaningfully be run on a Safari or Firefox browser, because they use a JS workaround to set dates in a culture-neutral fashion. Treating this test as an implicit pass.");

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        await When(webster).AttemptsTo(EnterTheDate(new DateTime(2025, 11, 12)).Into(inputArea).ForTheCultureNamed("ja-JP"));
        var result = await Then(webster).Should(ReadFromTheElement(displayText).TheText());

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.EqualTo(string.Empty), "The date shouldn't be empty");
            Assert.That(result, Is.Not.EqualTo("2025-11-12"), "The date shouldn't be the value which was entered either, because of the culture/format difference"); 
        });
    }
}