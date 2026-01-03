namespace CSF.Screenplay.Selenium.Queries;

using System.Drawing;
using System.Linq;
using CSF.Screenplay.Selenium.Elements;
using static CSF.Screenplay.PerformanceStarter;
using static CSF.Screenplay.Selenium.PerformableBuilder;

[TestFixture, Description("Tests for many classes in the Queries namespace")]
public class QueriesTests
{
    static readonly ITarget
        passwordInput = new ElementId("passwordInput", "the password input"),
        maybeClickableButtons = new CssSelector("button.maybeClickable", "the maybe-clickable buttons"),
        backgroundDiv = new ElementId("backgroundDiv", "the div with a background colour"),
        positionedDiv = new ElementId("positionedDiv", "the positioned div"),
        sizedDiv = new ElementId("sizedDiv", "the sized div"),
        hiddenDiv = new ElementId("hiddenDiv", "the hidden div"),
        multiSelect = new ElementId("multiSelect", "the multi-select element");
    static readonly NamedUri testPage = new NamedUri("QueriesTests.html", "the test page");

    [Test, Screenplay]
    public async Task ReadingTheTypeAttributeFromAnElementShouldReturnTheCorrectResult(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        var value = await When(webster).AttemptsTo(ReadFromTheElement(passwordInput).TheAttribute("type"));

        Assert.That(value, Is.EqualTo("password"));
    }

    [Test, Screenplay]
    public async Task ReadingTheClickabilityFromSomeElementsShouldReturnTheCorrectResults(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        var values = await When(webster).AttemptsTo(ReadFromTheCollectionOfElements(maybeClickableButtons).Clickability());

        Assert.That(values, Is.EqualTo(new bool[] { true, false }));
    }
    
    [Test, Screenplay]
    public async Task ReadingTheCssPropertyFromAnElementShouldReturnTheCorrectResult(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        var result = await When(webster).AttemptsTo(ReadFromTheElement(backgroundDiv).TheCssProperty("background-color"));

        Assert.That(result, Is.EqualTo("rgba(170, 170, 255, 1)"));
    }
    
    [Test, Screenplay]
    public async Task ReadingTheLocationOfAnElementShouldReturnTheCorrectResult(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        var result = await When(webster).AttemptsTo(ReadFromTheElement(positionedDiv).TheLocation());

        Assert.That(result, Is.EqualTo(new Point(700, 120)));
    }
    
    [Test, Screenplay]
    public async Task ReadingTheSizeOfAnElementShouldReturnTheCorrectResult(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        var result = await When(webster).AttemptsTo(ReadFromTheElement(sizedDiv).TheSize());

        Assert.That(result, Is.EqualTo(new Size(400, 150)));
    }
    
    [Test, Screenplay]
    public async Task ReadingTheTextOfAnElementShouldReturnTheCorrectResult(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        var result = await When(webster).AttemptsTo(ReadFromTheElement(sizedDiv).TheText());

        Assert.That(result, Is.EqualTo("This div has a size of 400px by 150px."));
    }
    
    [Test, Screenplay]
    public async Task ReadingTheValueOfAnElementShouldReturnTheCorrectResult(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        var result = await When(webster).AttemptsTo(ReadFromTheElement(passwordInput).TheValue());

        Assert.That(result, Is.EqualTo("secret"));
    }
    
    [Test, Screenplay]
    public async Task ReadingTheVisibilityOfAVisibleElementShouldReturnTrue(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        var result = await When(webster).AttemptsTo(ReadFromTheElement(passwordInput).TheVisibility());

        Assert.That(result, Is.True);
    }
    
    [Test, Screenplay]
    public async Task ReadingTheVisibilityOfAnInvisibleElementShouldReturnFalse(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        var result = await When(webster).AttemptsTo(ReadFromTheElement(hiddenDiv).TheVisibility());

        Assert.That(result, Is.False);
    }
    
    [Test, Screenplay]
    public async Task ReadingAllOptionsFromASelectElementShouldReturnTheCorrectResult(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        var result = await When(webster).AttemptsTo(ReadFromTheElement(multiSelect).AllOptions());

        Assert.That(result.Select(x => x.Text), Is.EqualTo(new string[] { "Option 1", "Option 2", "Option 3", "Option 4" }));
    }
    
    [Test, Screenplay]
    public async Task ReadingTheSelectedOptionsFromASelectElementShouldReturnTheCorrectResult(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        var result = await When(webster).AttemptsTo(ReadFromTheElement(multiSelect).SelectedOptions());

        Assert.That(result.Select(x => x.Text), Is.EqualTo(new string[] { "Option 1", "Option 3" }));
    }
    
    [Test, Screenplay]
    public async Task ReadingTheUnselectedOptionsFromASelectElementShouldReturnTheCorrectResult(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        var result = await When(webster).AttemptsTo(ReadFromTheElement(multiSelect).UnselectedOptions());

        Assert.That(result.Select(x => x.Text), Is.EqualTo(new string[] { "Option 2", "Option 4" }));
    }
}