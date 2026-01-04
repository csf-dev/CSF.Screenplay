using System.Collections.ObjectModel;
using System.Drawing;
using AutoFixture;
using CSF.Screenplay.Selenium.Elements;
using CSF.Specifications;
using Moq;
using OpenQA.Selenium;
using static CSF.Screenplay.Selenium.Builders.FilterSpecificationBuilder;

namespace CSF.Screenplay.Selenium.Builders;

[TestFixture, Parallelizable]
public class FilterSpecificationBuilderTests
{
    [Test, AutoMoqData]
    public void HaveAttributeValueShouldCreateASpecificationThatUsesGetAttribute(SeleniumElement matchingElement,
                                                                                 SeleniumElement nonMatchingElement)
    {
        Mock.Get(matchingElement.WebElement)
            .Setup(e => e.GetAttribute("data-test"))
            .Returns("foobar");
        Mock.Get(nonMatchingElement.WebElement)
            .Setup(e => e.GetAttribute("data-test"))
            .Returns("baz");

        var sut = HaveAttributeValue("data-test", v => v.StartsWith("foo"));

        Assert.Multiple(() =>
        {
            Assert.That(sut.Matches(matchingElement), Is.True, "Matching element should match");
            Assert.That(sut.Matches(nonMatchingElement), Is.False, "Non-matching element should not match");
        });
    }

    [Test, AutoMoqData]
    public void HaveAttributeValueWithPlainValueShouldCreateASpecificationThatUsesGetAttribute(SeleniumElement matchingElement,
                                                                                               SeleniumElement nonMatchingElement)
    {
        Mock.Get(matchingElement.WebElement)
            .Setup(e => e.GetAttribute("data-test"))
            .Returns("foobar");
        Mock.Get(nonMatchingElement.WebElement)
            .Setup(e => e.GetAttribute("data-test"))
            .Returns("food");

        var sut = HaveAttributeValue("data-test", "foobar");

        Assert.Multiple(() =>
        {
            Assert.That(sut.Matches(matchingElement), Is.True, "Matching element should match");
            Assert.That(sut.Matches(nonMatchingElement), Is.False, "Non-matching element should not match");
        });
    }

    [Test, AutoMoqData]
    public void HaveAttributeShouldCreateASpecificationThatUsesGetAttribute(SeleniumElement matchingElement,
                                                                            SeleniumElement nonMatchingElement)
    {
        Mock.Get(matchingElement.WebElement)
            .Setup(e => e.GetAttribute("data-test"))
            .Returns("foobar");
        Mock.Get(nonMatchingElement.WebElement)
            .Setup(e => e.GetAttribute("data-test"))
            .Returns(() => null!);

        var sut = HaveAttribute("data-test");

        Assert.Multiple(() =>
        {
            Assert.That(sut.Matches(matchingElement), Is.True, "Matching element should match");
            Assert.That(sut.Matches(nonMatchingElement), Is.False, "Non-matching element should not match");
        });
    }

    [Test, AutoMoqData]
    public void HaveClassShouldCreateASpecificationThatUsesGetAttribute(SeleniumElement matchingElement,
                                                                        SeleniumElement nonMatchingElement)
    {
        Mock.Get(matchingElement.WebElement)
            .Setup(e => e.GetAttribute("class"))
            .Returns("foobar baz");
        Mock.Get(nonMatchingElement.WebElement)
            .Setup(e => e.GetAttribute("class"))
            .Returns("baz qux quux");

        var sut = HaveClass("foobar");

        Assert.Multiple(() =>
        {
            Assert.That(sut.Matches(matchingElement), Is.True, "Matching element should match");
            Assert.That(sut.Matches(nonMatchingElement), Is.False, "Non-matching element should not match");
        });
    }

    [Test, AutoMoqData]
    public void HaveAllClassesShouldCreateASpecificationThatUsesGetAttribute(SeleniumElement matchingElement,
                                                                             SeleniumElement nonMatchingElement)
    {
        Mock.Get(matchingElement.WebElement)
            .Setup(e => e.GetAttribute("class"))
            .Returns("foobar baz");
        Mock.Get(nonMatchingElement.WebElement)
            .Setup(e => e.GetAttribute("class"))
            .Returns("qux foobar quux");

        var sut = HaveAllClasses("foobar", "baz");

        Assert.Multiple(() =>
        {
            Assert.That(sut.Matches(matchingElement), Is.True, "Matching element should match");
            Assert.That(sut.Matches(nonMatchingElement), Is.False, "Non-matching element should not match");
        });
    }

    [Test, AutoMoqData]
    public void AreClickableShouldCreateASpecificationThatTestsVisibilityAndEnabledState(SeleniumElement matchingElement,
                                                                                         SeleniumElement nonMatchingElement1,
                                                                                         SeleniumElement nonMatchingElement2,
                                                                                         SeleniumElement nonMatchingElement3)
    {
        Mock.Get(matchingElement.WebElement).SetupGet(e => e.Displayed).Returns(true);
        Mock.Get(matchingElement.WebElement).SetupGet(e => e.Enabled).Returns(true);
        Mock.Get(nonMatchingElement1.WebElement).SetupGet(e => e.Displayed).Returns(false);
        Mock.Get(nonMatchingElement1.WebElement).SetupGet(e => e.Enabled).Returns(true);
        Mock.Get(nonMatchingElement2.WebElement).SetupGet(e => e.Displayed).Returns(true);
        Mock.Get(nonMatchingElement2.WebElement).SetupGet(e => e.Enabled).Returns(false);
        Mock.Get(nonMatchingElement3.WebElement).SetupGet(e => e.Displayed).Returns(false);
        Mock.Get(nonMatchingElement3.WebElement).SetupGet(e => e.Enabled).Returns(false);

        var sut = AreClickable();

        Assert.Multiple(() =>
        {
            Assert.That(sut.Matches(matchingElement), Is.True, "Matching element should match");
            Assert.That(sut.Matches(nonMatchingElement1), Is.False, "Non-matching element 1 should not match");
            Assert.That(sut.Matches(nonMatchingElement2), Is.False, "Non-matching element 2 should not match");
            Assert.That(sut.Matches(nonMatchingElement3), Is.False, "Non-matching element 3 should not match");
        });
    }

    [Test, AutoMoqData]
    public void HaveCssPropertyShouldCreateASpecificationThatUsesGetCssValue(SeleniumElement matchingElement,
                                                                             SeleniumElement nonMatchingElement)
    {
        Mock.Get(matchingElement.WebElement)
            .Setup(e => e.GetCssValue("color"))
            .Returns("rgba(255, 0, 0, 1)");
        Mock.Get(nonMatchingElement.WebElement)
            .Setup(e => e.GetCssValue("color"))
            .Returns("rgba(0, 0, 255, 1)");

        var sut = HaveCssProperty("color", v => v == "rgba(255, 0, 0, 1)");

        Assert.Multiple(() =>
        {
            Assert.That(sut.Matches(matchingElement), Is.True, "Matching element should match");
            Assert.That(sut.Matches(nonMatchingElement), Is.False, "Non-matching element should not match");
        });
    }

    [Test, AutoMoqData]
    public void HaveLocationShouldCreateASpecificationThatUsesLocationQuery(SeleniumElement matchingElement,
                                                                            SeleniumElement nonMatchingElement)
    {
        Mock.Get(matchingElement.WebElement).SetupGet(e => e.Location).Returns(new Point(100, 200));
        Mock.Get(nonMatchingElement.WebElement).SetupGet(e => e.Location).Returns(new Point(150, 250));

        var sut = HaveLocation(new Point(100, 200));

        Assert.Multiple(() =>
        {
            Assert.That(sut.Matches(matchingElement), Is.True, "Matching element should match");
            Assert.That(sut.Matches(nonMatchingElement), Is.False, "Non-matching element should not match");
        });
    }

    [Test, AutoMoqData]
    public void HaveSizeShouldCreateASpecificationThatUsesSizeQuery(SeleniumElement matchingElement,
                                                                    SeleniumElement nonMatchingElement)
    {
        Mock.Get(matchingElement.WebElement).SetupGet(e => e.Size).Returns(new Size(100, 200));
        Mock.Get(nonMatchingElement.WebElement).SetupGet(e => e.Size).Returns(new Size(150, 250));

        var sut = HaveSize(new Size(100, 200));

        Assert.Multiple(() =>
        {
            Assert.That(sut.Matches(matchingElement), Is.True, "Matching element should match");
            Assert.That(sut.Matches(nonMatchingElement), Is.False, "Non-matching element should not match");
        });
    }

    [Test, AutoMoqData]
    public void HaveTextShouldCreateASpecificationThatUsesTextQuery(SeleniumElement matchingElement,
                                                                    SeleniumElement nonMatchingElement)
    {
        Mock.Get(matchingElement.WebElement).SetupGet(e => e.Text).Returns("Hello World");
        Mock.Get(nonMatchingElement.WebElement).SetupGet(e => e.Text).Returns("Goodbye World");

        var sut = HaveText("Hello World");

        Assert.Multiple(() =>
        {
            Assert.That(sut.Matches(matchingElement), Is.True, "Matching element should match");
            Assert.That(sut.Matches(nonMatchingElement), Is.False, "Non-matching element should not match");
        });
    }

    [Test, AutoMoqData]
    public void HaveValueShouldCreateASpecificationThatUsesValueQuery(SeleniumElement matchingElement,
                                                                      SeleniumElement nonMatchingElement)
    {
        Mock.Get(matchingElement.WebElement).Setup(e => e.GetDomProperty("value")).Returns("Hello World");
        Mock.Get(nonMatchingElement.WebElement).Setup(e => e.GetDomProperty("value")).Returns("Goodbye World");

        var sut = HaveValue("Hello World");

        Assert.Multiple(() =>
        {
            Assert.That(sut.Matches(matchingElement), Is.True, "Matching element should match");
            Assert.That(sut.Matches(nonMatchingElement), Is.False, "Non-matching element should not match");
        });
    }
    
    [Test, AutoMoqData]
    public void AreVisibleShouldCreateASpecificationThatUsesVisibilityQuery(SeleniumElement matchingElement,
                                                                            SeleniumElement nonMatchingElement)
    {
        Mock.Get(matchingElement.WebElement).SetupGet(e => e.Displayed).Returns(true);
        Mock.Get(nonMatchingElement.WebElement).SetupGet(e => e.Displayed).Returns(false);

        var sut = AreVisible();

        Assert.Multiple(() =>
        {
            Assert.That(sut.Matches(matchingElement), Is.True, "Matching element should match");
            Assert.That(sut.Matches(nonMatchingElement), Is.False, "Non-matching element should not match");
        });
    }
    
    [Test, AutoMoqData]
    public void HaveSelectedOptionsByTextShouldCreateASpecificationThatUsesOptionsQuery(SeleniumElement matchingElement,
                                                                                        SeleniumElement nonMatchingElement)
    {
        Mock.Get(matchingElement.WebElement)
            .Setup(e => e.FindElements(It.Is<By>(b => b.Mechanism == "tag name" && b.Criteria == "option")))
            .Returns(new ReadOnlyCollection<IWebElement>([
                Mock.Of<IWebElement>(we => we.Text == "Option 1" && we.Selected && we.GetDomProperty("value") == "1"),
                Mock.Of<IWebElement>(we => we.Text == "Option 2" && !we.Selected && we.GetDomProperty("value") == "2"),
                Mock.Of<IWebElement>(we => we.Text == "Option 3" && we.Selected && we.GetDomProperty("value") == "3"),
                ]));
        Mock.Get(nonMatchingElement.WebElement)
            .Setup(e => e.FindElements(It.Is<By>(b => b.Mechanism == "tag name" && b.Criteria == "option")))
            .Returns(new ReadOnlyCollection<IWebElement>([
                Mock.Of<IWebElement>(we => we.Text == "Option 1" && !we.Selected && we.GetDomProperty("value") == "1"),
                Mock.Of<IWebElement>(we => we.Text == "Option 2" && !we.Selected && we.GetDomProperty("value") == "2"),
                Mock.Of<IWebElement>(we => we.Text == "Option 3" && we.Selected && we.GetDomProperty("value") == "3"),
                ]));

        var sut = HaveSelectedOptionsByText("Option 1", "Option 3");

        Assert.Multiple(() =>
        {
            Assert.That(sut.Matches(matchingElement), Is.True, "Matching element should match");
            Assert.That(sut.Matches(nonMatchingElement), Is.False, "Non-matching element should not match");
        });
    }
    
    [Test, AutoMoqData]
    public void HaveUnSelectedOptionsByTextShouldCreateASpecificationThatUsesOptionsQuery(SeleniumElement matchingElement,
                                                                                          SeleniumElement nonMatchingElement)
    {
        Mock.Get(matchingElement.WebElement)
            .Setup(e => e.FindElements(It.Is<By>(b => b.Mechanism == "tag name" && b.Criteria == "option")))
            .Returns(new ReadOnlyCollection<IWebElement>([
                Mock.Of<IWebElement>(we => we.Text == "Option 1" && we.Selected && we.GetDomProperty("value") == "1"),
                Mock.Of<IWebElement>(we => we.Text == "Option 2" && !we.Selected && we.GetDomProperty("value") == "2"),
                Mock.Of<IWebElement>(we => we.Text == "Option 3" && we.Selected && we.GetDomProperty("value") == "3"),
                ]));
        Mock.Get(nonMatchingElement.WebElement)
            .Setup(e => e.FindElements(It.Is<By>(b => b.Mechanism == "tag name" && b.Criteria == "option")))
            .Returns(new ReadOnlyCollection<IWebElement>([
                Mock.Of<IWebElement>(we => we.Text == "Option 1" && !we.Selected && we.GetDomProperty("value") == "1"),
                Mock.Of<IWebElement>(we => we.Text == "Option 2" && !we.Selected && we.GetDomProperty("value") == "2"),
                Mock.Of<IWebElement>(we => we.Text == "Option 3" && we.Selected && we.GetDomProperty("value") == "3"),
                ]));

        var sut = HaveUnselectedOptionsByText("Option 2");

        Assert.Multiple(() =>
        {
            Assert.That(sut.Matches(matchingElement), Is.True, "Matching element should match");
            Assert.That(sut.Matches(nonMatchingElement), Is.False, "Non-matching element should not match");
        });
    }
    
    [Test, AutoMoqData]
    public void HaveOptionsByTextShouldCreateASpecificationThatUsesOptionsQuery(SeleniumElement matchingElement,
                                                                                        SeleniumElement nonMatchingElement)
    {
        Mock.Get(matchingElement.WebElement)
            .Setup(e => e.FindElements(It.Is<By>(b => b.Mechanism == "tag name" && b.Criteria == "option")))
            .Returns(new ReadOnlyCollection<IWebElement>([
                Mock.Of<IWebElement>(we => we.Text == "Option 1" && we.Selected && we.GetDomProperty("value") == "1"),
                Mock.Of<IWebElement>(we => we.Text == "Option 2" && !we.Selected && we.GetDomProperty("value") == "2"),
                Mock.Of<IWebElement>(we => we.Text == "Option 3" && we.Selected && we.GetDomProperty("value") == "3"),
                ]));
        Mock.Get(nonMatchingElement.WebElement)
            .Setup(e => e.FindElements(It.Is<By>(b => b.Mechanism == "tag name" && b.Criteria == "option")))
            .Returns(new ReadOnlyCollection<IWebElement>([
                Mock.Of<IWebElement>(we => we.Text == "Option 1" && !we.Selected && we.GetDomProperty("value") == "1"),
                Mock.Of<IWebElement>(we => we.Text == "Option 3" && we.Selected && we.GetDomProperty("value") == "3"),
                ]));

        var sut = HaveOptionsByText("Option 1", "Option 2", "Option 3");

        Assert.Multiple(() =>
        {
            Assert.That(sut.Matches(matchingElement), Is.True, "Matching element should match");
            Assert.That(sut.Matches(nonMatchingElement), Is.False, "Non-matching element should not match");
        });
    }
}
