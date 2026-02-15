using System.Linq;
using CSF.Screenplay.Selenium.Elements;
using static CSF.Screenplay.PerformanceStarter;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Selenium.Questions;

[TestFixture, Parallelizable, Category("WebDriver")]
public class FindElementsTests
{
    static readonly Locator
        container = new ElementId("container"),
        special = new ClassName("specialInput"),
        theList = new ElementId("theList"),
        secondSpan = new CssSelector("li:nth-child(3) > span"),
        secondSpanDeepSelector = new CssSelector("#theList li:nth-child(3) > span");
    static readonly NamedUri testPage = new NamedUri("LocatorTests.html", "the test page");

    [Test, Screenplay]
    public async Task FindElementsWithinShouldFindCorrectElements(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        var inputs = await When(webster).AttemptsTo(FindElementsWithin(container).WhichMatch(special).AndNameThem("the special inputs"));

        Assert.That(inputs.Select(i => i.WebElement.GetDomProperty("value")).ToList(), Is.EqualTo(new[] { "Second input", "Third input" }));
    }

    [Test, Screenplay]
    public async Task FindAnElementWithinShouldFindCorrectElement(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        var span = await When(webster).AttemptsTo(FindAnElementWithin(theList).WhichMatches(secondSpan).AndNameIt("the second span in the list"));

        Assert.That(span.WebElement.Text, Is.EqualTo("Second text inside a span"));
    }

    [Test, Screenplay]
    public async Task FindAnElementOnThePageShouldFindCorrectElement(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        var span = await When(webster).AttemptsTo(FindAnElementOnThePage().WhichMatches(secondSpanDeepSelector));

        Assert.That(span.WebElement.Text, Is.EqualTo("Second text inside a span"));
    }

}