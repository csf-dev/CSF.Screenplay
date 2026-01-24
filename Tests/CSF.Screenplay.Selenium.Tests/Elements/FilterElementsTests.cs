namespace CSF.Screenplay.Selenium.Elements;

using static CSF.Screenplay.PerformanceStarter;
using static CSF.Screenplay.Selenium.PerformableBuilder;

[TestFixture, Parallelizable]
public class FilterElementsTests
{
    static readonly Locator
        allInputs = new ClassName("multiInput", "all input elements with the 'multiInput' class");
    static readonly NamedUri testPage = new NamedUri("LocatorTests.html", "the test page");

    [Test, Screenplay]
    public async Task FilteringTheInputElementsForOnlySpecialInputsShouldReturnTheCorrectResult(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        var elements = await Given(webster).WasAbleTo(FindElementsOnThePage().WhichMatch(allInputs));
        var filteredElements = await When(webster).AttemptsTo(Filter(elements).ForThoseWhich(have => have.Class("specialInput")).AndNameThem("the special input elements"));
        var values = await Then(webster).Should(ReadFromTheCollectionOfElements(filteredElements).Value());

        Assert.That(values, Is.EqualTo(new [] {"Second input", "Third input"}));
    }
}