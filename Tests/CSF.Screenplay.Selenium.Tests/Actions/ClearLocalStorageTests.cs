
using CSF.Screenplay.Selenium.Elements;
using static CSF.Screenplay.PerformanceStarter;
using static CSF.Screenplay.Selenium.SeleniumPerformableBuilder;

namespace CSF.Screenplay.Selenium.Actions;

[TestFixture]
public class ClearLocalStorageTests
{
    static readonly ITarget
        listItemsButton = new ElementId("getItems", "the items-listing button"),
        itemList = new ElementId("itemsList", "the list of storage items");

    static readonly NamedUri testPage = new NamedUri("ClearLocalStorageTests.html", "the test page");

    [Test, Screenplay]
    public async Task ClearLocalStorageShouldClearItems(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        await When(webster).AttemptsTo(ClearLocalStorage());
        await Then(webster).Should(ClickOn(listItemsButton));
        var items = await Then(webster).Should(ReadFromTheElement(itemList).TheText());

        Assert.That(items, Is.Empty);
    }
}