using CSF.Extensions.WebDriver;
using CSF.Extensions.WebDriver.Factories;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Elements;
using Moq;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Builders;

[TestFixture, Parallelizable]
public class TextQueryBuilderTests
{
    [Test, AutoMoqData]
    public async Task WithoutTrimmingWhitespaceShouldBuildAQueryWhichDoesNotTrimWhitespace([Frozen] ITarget target,
                                                                                           TextQueryBuilder sut,
                                                                                           Actor actor,
                                                                                           SeleniumElement element,
                                                                                           IWebDriver driver)
    {
        actor.IsAbleTo(new BrowseTheWeb(Mock.Of<IGetsWebDriver>(f => f.GetDefaultWebDriver(null) == new WebDriverAndOptions(driver, Mock.Of<DriverOptions>()))));
        Mock.Get(target).Setup(x => x.GetElement(driver)).Returns(element);
        Mock.Get(element.WebElement).SetupGet(x => x.Text).Returns("  foo bar  ");
        sut.WithoutTrimmingWhitespace();
        var performable = ((IGetsPerformableWithResult<string>) sut).GetPerformable();
        var result = await performable.PerformAsAsync(actor);
        Assert.That(result, Is.EqualTo("  foo bar  "));
    }

    [Test, AutoMoqData]
    public async Task ShouldBuildAQueryWhichTrimsWhitespaceByDefault([Frozen] ITarget target,
                                                                     TextQueryBuilder sut,
                                                                     Actor actor,
                                                                     SeleniumElement element,
                                                                     IWebDriver driver)
    {
        actor.IsAbleTo(new BrowseTheWeb(Mock.Of<IGetsWebDriver>(f => f.GetDefaultWebDriver(null) == new WebDriverAndOptions(driver, Mock.Of<DriverOptions>()))));
        Mock.Get(target).Setup(x => x.GetElement(driver)).Returns(element);
        Mock.Get(element.WebElement).SetupGet(x => x.Text).Returns("  foo bar  ");
        var performable = ((IGetsPerformableWithResult<string>) sut).GetPerformable();
        var result = await performable.PerformAsAsync(actor);
        Assert.That(result, Is.EqualTo("foo bar"));
    }
}
