using System;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Elements;

[TestFixture, Parallelizable]
public class SeleniumElementCollectionTests
{
    [Test, AutoMoqData]
    public void GetElementsShouldReturnItself(IWebDriver driver)
    {
        ITarget sut = new SeleniumElementCollection(Array.Empty<SeleniumElement>());
        Assert.That(() => sut.GetElements(driver), Is.SameAs(sut));
    }

    [Test, AutoMoqData]
    public void GetElementShouldThrowIfNoElements(IWebDriver driver)
    {
        ITarget sut = new SeleniumElementCollection(Array.Empty<SeleniumElement>());
        Assert.That(() => sut.GetElement(driver), Throws.InstanceOf<TargetNotFoundException>());
    }

    [Test, AutoMoqData]
    public void GetElementShouldReturnFirst(IWebDriver driver, SeleniumElement first, SeleniumElement second)
    {
        ITarget sut = new SeleniumElementCollection([first, second]);
        Assert.That(() => sut.GetElement(driver), Is.SameAs(first));
    }
}