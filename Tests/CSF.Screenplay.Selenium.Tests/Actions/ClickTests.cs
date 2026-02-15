
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Elements;
using static CSF.Screenplay.PerformanceStarter;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Selenium.Actions;

[TestFixture, Parallelizable, Category("WebDriver")]
public class ClickTests
{
    static readonly ITarget
        clickableButton = new ElementId("clickable", "the clickable button"),
        nonExistent = new ElementId("nope", "the non-existent element"),
        displayText = new ElementId("display", "the displayable text");

    static readonly NamedUri testPage = new NamedUri("ClickTests.html", "the test page");

    [Test, Screenplay]
    public async Task ClickingAButtonShouldTriggerAnEvent(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        await When(webster).AttemptsTo(ClickOn(clickableButton));
        var contents = await Then(webster).Should(ReadFromTheElement(displayText).TheText());

        Assert.That(contents, Is.EqualTo("Clicked!"));
    }

    [Test, Screenplay]
    public async Task ClickingAnElementWhichDoesNotExistShouldThrow(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        Assert.That(async () => await When(webster).AttemptsTo(ClickOn(nonExistent)),
                    Throws.InstanceOf<PerformableException>().And.InnerException.InstanceOf<TargetNotFoundException>());
    }

    [Test, Screenplay]
    public async Task ClickingAnElementWhichDoesNotExistShouldIncludeTheCorrectTargetInTheException(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        try
        {
            await When(webster).AttemptsTo(ClickOn(nonExistent));
            Assert.Fail("Should have thrown an exception!");
        }
        catch(PerformableException e) when (e is { InnerException: TargetNotFoundException tnfEx })
        {
            Assert.That(tnfEx.Target, Has.Property(nameof(ITarget.Name)).EqualTo("the non-existent element"));
        }
    }
}