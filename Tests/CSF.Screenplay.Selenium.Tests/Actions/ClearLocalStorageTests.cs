
using System;
using CSF.Extensions.WebDriver;
using CSF.Extensions.WebDriver.Factories;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Elements;
using Moq;
using OpenQA.Selenium;
using static CSF.Screenplay.PerformanceStarter;
using static CSF.Screenplay.Selenium.PerformableBuilder;

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

    [Test, AutoMoqData]
    public void PerformAsAsyncShouldThrowIfBrowserDoesNotSupportJavaScriptAndThrowIfUnsupportedIsTrue(Actor actor,
                                                                                                      IWebDriver webDriver,
                                                                                                      Mock<DriverOptions> options)
    {
        var driverAndOptions = new WebDriverAndOptions(webDriver, options.Object);
        var ability = new BrowseTheWeb(Mock.Of<IGetsWebDriver>(x => x.GetDefaultWebDriver(It.IsAny<Action<DriverOptions>>()) == driverAndOptions));
        actor.IsAbleTo(ability);
        var sut = new ClearLocalStorage(true);
        Assert.That(async () => await sut.PerformAsAsync(actor), Throws.InstanceOf<PerformableException>().And.InnerException.InstanceOf<NotSupportedException>());
    }

    [Test, AutoMoqData]
    public void PerformAsAsyncShouldNotThrowIfBrowserDoesNotSupportJavaScriptAndThrowIfUnsupportedIsFalse(Actor actor,
                                                                                                          IWebDriver webDriver,
                                                                                                          Mock<DriverOptions> options)
    {
        var driverAndOptions = new WebDriverAndOptions(webDriver, options.Object);
        var ability = new BrowseTheWeb(Mock.Of<IGetsWebDriver>(x => x.GetDefaultWebDriver(It.IsAny<Action<DriverOptions>>()) == driverAndOptions));
        actor.IsAbleTo(ability);
        var sut = new ClearLocalStorage(false);
        Assert.That(async () => await sut.PerformAsAsync(actor), Throws.Nothing);
    }
}