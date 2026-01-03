namespace CSF.Screenplay.Selenium.Elements;
using static CSF.Screenplay.PerformanceStarter;
using static CSF.Screenplay.Selenium.SeleniumPerformableBuilder;

[TestFixture, Description("Tests for various subclasses of Locator")]
public class LocatorTests
{
    static readonly ITarget
        theParagraph = new ElementId("theParagraph", "the unique paragraph element"),
        theDiv = new ClassName("theDiv", "the unique div element"),
        allInputs = new ClassName("multiInput", "all input elements with the 'multiInput' class"),
        specialInputs = new CssSelector("#container .specialInput", "the input element with both 'input' and 'specialInput' classes"),
        spansInList = new XPath("//ul[@id='theList']//span", "all span elements within the ul element which has id 'theList'");

    static readonly NamedUri testPage = new NamedUri("LocatorTests.html", "the test page");

    [Test, Screenplay]
    public async Task GettingTextFromTheParagraphShouldReturnTheCorrectResult(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        var result = await When(webster).AttemptsTo(ReadFromTheElement(theParagraph).TheText());

        Assert.That(result, Is.EqualTo("This is a paragraph of test which is selectable by the element id theParagraph."));
    }

    [Test, Screenplay]
    public async Task GettingTextFromTheDivShouldReturnTheCorrectResult(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        var result = await When(webster).AttemptsTo(ReadFromTheElement(theDiv).TheText());

        Assert.That(result, Is.EqualTo("This is a div which is selectable by the class attribute theDiv."));
    }

    [Test, Screenplay]
    public async Task GettingTheValueFromTheMultiInputsShouldReturnTheCorrectResult(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        var result = await When(webster).AttemptsTo(ReadFromTheCollectionOfElements(allInputs).Value());

        Assert.That(result, Is.EqualTo(new [] {"First input", "Second input", "Third input"}));
    }

    [Test, Screenplay]
    public async Task GettingTheValueFromTheSpecialInputsShouldReturnTheCorrectResult(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        var result = await When(webster).AttemptsTo(ReadFromTheCollectionOfElements(specialInputs).Value());

        Assert.That(result, Is.EqualTo(new [] {"Second input", "Third input"}));
    }

    [Test, Screenplay]
    public async Task GettingTheTextFromTheSpansInTheListShouldReturnTheCorrectResult(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        var result = await When(webster).AttemptsTo(ReadFromTheCollectionOfElements(spansInList).Text());

        Assert.That(result, Is.EqualTo(new [] {"First text inside a span", "Second text inside a span"}));
    }


}