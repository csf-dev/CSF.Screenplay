namespace CSF.Screenplay.Selenium.Elements;

using CSF.Specifications;
using static CSF.Screenplay.PerformanceStarter;
using static CSF.Screenplay.Selenium.PerformableBuilder;

[TestFixture]
public class FilterElementsTests
{
    static readonly Locator
        allInputs = new ClassName("multiInput", "all input elements with the 'multiInput' class");
    static readonly ISpecificationFunction<SeleniumElement>
        specialInputs = Spec.Func<SeleniumElement>(e => e.WebElement.GetAttribute("class").Contains("specialInput"));
    static readonly NamedUri testPage = new NamedUri("LocatorTests.html", "the test page");

    [Test, Screenplay]
    public async Task FilteringTheInputElementsForOnlySpecialInputsShouldReturnTheCorrectResult(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        var elements = await Given(webster).WasAbleTo(FindElementsOnThePage().WhichMatch(allInputs));
        var filteredElements = await When(webster).AttemptsTo(FilterTheElements(elements).ForThoseWhichAre(specialInputs));
        var values = await Then(webster).Should(ReadFromTheCollectionOfElements(filteredElements).Value());

        Assert.That(values, Is.EqualTo(new [] {"Second input", "Third input"}));
    }
}