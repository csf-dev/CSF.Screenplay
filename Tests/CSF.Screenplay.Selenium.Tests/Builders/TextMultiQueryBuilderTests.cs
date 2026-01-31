using System.Collections.Generic;
using CSF.Extensions.WebDriver;
using CSF.Extensions.WebDriver.Factories;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Elements;
using Moq;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Builders;

[TestFixture, Parallelizable]
public class TextMultiQueryBuilderTests
{
    [Test, AutoMoqData]
    public async Task WithoutTrimmingWhitespaceShouldBuildAQueryWhichDoesNotTrimWhitespace([Frozen] ITarget target,
                                                                                           TextMultiQueryBuilder sut,
                                                                                           Actor actor,
                                                                                           SeleniumElement element1,
                                                                                           SeleniumElement element2,
                                                                                           IWebDriver driver)
    {
        actor.IsAbleTo(new BrowseTheWeb(Mock.Of<IGetsWebDriver>(f => f.GetDefaultWebDriver(null) == new WebDriverAndOptions(driver, Mock.Of<DriverOptions>()))));
        Mock.Get(target).Setup(x => x.GetElements(driver)).Returns(new SeleniumElementCollection([element1, element2], "the elements"));
        Mock.Get(element1.WebElement).SetupGet(x => x.Text).Returns("  foo bar  ");
        Mock.Get(element2.WebElement).SetupGet(x => x.Text).Returns("  baz bob  ");
        sut.WithoutTrimmingWhitespace();
        var performable = ((IGetsPerformableWithResult<IReadOnlyList<string>>) sut).GetPerformable();
        var result = await performable.PerformAsAsync(actor);
        Assert.That(result, Is.EqualTo(new [] {"  foo bar  ", "  baz bob  "}));
    }

    [Test, AutoMoqData]
    public async Task ShouldBuildAQueryWhichTrimsWhitespaceByDefault([Frozen] ITarget target,
                                                                     TextMultiQueryBuilder sut,
                                                                     Actor actor,
                                                                     SeleniumElement element1,
                                                                     SeleniumElement element2,
                                                                     IWebDriver driver)
    {
        actor.IsAbleTo(new BrowseTheWeb(Mock.Of<IGetsWebDriver>(f => f.GetDefaultWebDriver(null) == new WebDriverAndOptions(driver, Mock.Of<DriverOptions>()))));
        Mock.Get(target).Setup(x => x.GetElements(driver)).Returns(new SeleniumElementCollection([element1, element2], "the elements"));
        Mock.Get(element1.WebElement).SetupGet(x => x.Text).Returns("  foo bar  ");
        Mock.Get(element2.WebElement).SetupGet(x => x.Text).Returns("  baz bob  ");
        var performable = ((IGetsPerformableWithResult<IReadOnlyList<string>>) sut).GetPerformable();
        var result = await performable.PerformAsAsync(actor);
        Assert.That(result, Is.EqualTo(new [] {"foo bar", "baz bob"}));
    }
}