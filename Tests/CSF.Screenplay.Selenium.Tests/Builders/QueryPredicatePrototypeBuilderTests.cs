using System.Collections.ObjectModel;
using System.Drawing;
using CSF.Screenplay.Selenium.Elements;
using CSF.Specifications;
using Moq;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Builders;

[TestFixture, Parallelizable]
public class QueryPredicatePrototypeBuilderTests
{
    [Test, AutoMoqData]
    public void AttributeValueShouldCreateASpecificationThatUsesGetAttribute(QueryPredicatePrototypeBuilder builder,
                                                                                 SeleniumElement matchingElement,
                                                                                 SeleniumElement nonMatchingElement)
    {
        Mock.Get(matchingElement.WebElement)
            .Setup(e => e.GetAttribute("data-test"))
            .Returns("foobar");
        Mock.Get(nonMatchingElement.WebElement)
            .Setup(e => e.GetAttribute("data-test"))
            .Returns("baz");

        var sut = builder.AttributeValue("data-test", v => v.StartsWith("foo")).GetElementSpecification();

        Assert.Multiple(() =>
        {
            Assert.That(sut.Matches(matchingElement), Is.True, "Matching element should match");
            Assert.That(sut.Matches(nonMatchingElement), Is.False, "Non-matching element should not match");
        });
    }

    [Test, AutoMoqData]
    public void AttributeValueWithPlainValueShouldCreateASpecificationThatUsesGetAttribute(QueryPredicatePrototypeBuilder builder,
                                                                                               SeleniumElement matchingElement,
                                                                                               SeleniumElement nonMatchingElement)
    {
        Mock.Get(matchingElement.WebElement)
            .Setup(e => e.GetAttribute("data-test"))
            .Returns("foobar");
        Mock.Get(nonMatchingElement.WebElement)
            .Setup(e => e.GetAttribute("data-test"))
            .Returns("food");

        var sut = builder.AttributeValue("data-test", "foobar").GetElementSpecification();

        Assert.Multiple(() =>
        {
            Assert.That(sut.Matches(matchingElement), Is.True, "Matching element should match");
            Assert.That(sut.Matches(nonMatchingElement), Is.False, "Non-matching element should not match");
        });
    }

    [Test, AutoMoqData]
    public void AttributeShouldCreateASpecificationThatUsesGetAttribute(QueryPredicatePrototypeBuilder builder,
                                                                            SeleniumElement matchingElement,
                                                                            SeleniumElement nonMatchingElement)
    {
        Mock.Get(matchingElement.WebElement)
            .Setup(e => e.GetAttribute("data-test"))
            .Returns("foobar");
        Mock.Get(nonMatchingElement.WebElement)
            .Setup(e => e.GetAttribute("data-test"))
            .Returns(() => null!);

        var sut = builder.Attribute("data-test").GetElementSpecification();

        Assert.Multiple(() =>
        {
            Assert.That(sut.Matches(matchingElement), Is.True, "Matching element should match");
            Assert.That(sut.Matches(nonMatchingElement), Is.False, "Non-matching element should not match");
        });
    }

    [Test, AutoMoqData]
    public void ClassShouldCreateASpecificationThatUsesGetAttribute(QueryPredicatePrototypeBuilder builder,
                                                                        SeleniumElement matchingElement,
                                                                        SeleniumElement nonMatchingElement)
    {
        Mock.Get(matchingElement.WebElement)
            .Setup(e => e.GetAttribute("class"))
            .Returns("foobar baz");
        Mock.Get(nonMatchingElement.WebElement)
            .Setup(e => e.GetAttribute("class"))
            .Returns("baz qux quux");

        var sut = builder.Class("foobar").GetElementSpecification();

        Assert.Multiple(() =>
        {
            Assert.That(sut.Matches(matchingElement), Is.True, "Matching element should match");
            Assert.That(sut.Matches(nonMatchingElement), Is.False, "Non-matching element should not match");
        });
    }

    [Test, AutoMoqData]
    public void NotClassShouldCreateASpecificationThatUsesGetAttribute(QueryPredicatePrototypeBuilder builder,
                                                                        SeleniumElement matchingElement,
                                                                        SeleniumElement nonMatchingElement)
    {
        Mock.Get(matchingElement.WebElement)
            .Setup(e => e.GetAttribute("class"))
            .Returns("foobar baz");
        Mock.Get(nonMatchingElement.WebElement)
            .Setup(e => e.GetAttribute("class"))
            .Returns("baz qux quux");

        var sut = builder.NotClass("qux").GetElementSpecification();

        Assert.Multiple(() =>
        {
            Assert.That(sut.Matches(matchingElement), Is.True, "Matching element should match");
            Assert.That(sut.Matches(nonMatchingElement), Is.False, "Non-matching element should not match");
        });
    }

    [Test, AutoMoqData]
    public void AllClassesShouldCreateASpecificationThatUsesGetAttribute(QueryPredicatePrototypeBuilder builder,
                                                                             SeleniumElement matchingElement,
                                                                             SeleniumElement nonMatchingElement)
    {
        Mock.Get(matchingElement.WebElement)
            .Setup(e => e.GetAttribute("class"))
            .Returns("foobar baz");
        Mock.Get(nonMatchingElement.WebElement)
            .Setup(e => e.GetAttribute("class"))
            .Returns("qux foobar quux");

        var sut = builder.AllClasses("foobar", "baz").GetElementSpecification();

        Assert.Multiple(() =>
        {
            Assert.That(sut.Matches(matchingElement), Is.True, "Matching element should match");
            Assert.That(sut.Matches(nonMatchingElement), Is.False, "Non-matching element should not match");
        });
    }

    [Test, AutoMoqData]
    public void ClickableShouldCreateASpecificationThatTestsVisibilityAndEnabledState(QueryPredicatePrototypeBuilder builder,
                                                                                         SeleniumElement matchingElement,
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

        var sut = builder.Clickable().GetElementSpecification();

        Assert.Multiple(() =>
        {
            Assert.That(sut.Matches(matchingElement), Is.True, "Matching element should match");
            Assert.That(sut.Matches(nonMatchingElement1), Is.False, "Non-matching element 1 should not match");
            Assert.That(sut.Matches(nonMatchingElement2), Is.False, "Non-matching element 2 should not match");
            Assert.That(sut.Matches(nonMatchingElement3), Is.False, "Non-matching element 3 should not match");
        });
    }

    [Test, AutoMoqData]
    public void CssPropertyShouldCreateASpecificationThatUsesGetCssValue(QueryPredicatePrototypeBuilder builder,
                                                                             SeleniumElement matchingElement,
                                                                             SeleniumElement nonMatchingElement)
    {
        Mock.Get(matchingElement.WebElement)
            .Setup(e => e.GetCssValue("color"))
            .Returns("rgba(255, 0, 0, 1)");
        Mock.Get(nonMatchingElement.WebElement)
            .Setup(e => e.GetCssValue("color"))
            .Returns("rgba(0, 0, 255, 1)");

        var sut = builder.CssProperty("color", v => v == "rgba(255, 0, 0, 1)").GetElementSpecification();

        Assert.Multiple(() =>
        {
            Assert.That(sut.Matches(matchingElement), Is.True, "Matching element should match");
            Assert.That(sut.Matches(nonMatchingElement), Is.False, "Non-matching element should not match");
        });
    }

    [Test, AutoMoqData]
    public void CssPropertyWithValueShouldCreateASpecificationThatUsesGetCssValue(QueryPredicatePrototypeBuilder builder,
                                                                             SeleniumElement matchingElement,
                                                                             SeleniumElement nonMatchingElement)
    {
        Mock.Get(matchingElement.WebElement)
            .Setup(e => e.GetCssValue("color"))
            .Returns("rgba(255, 0, 0, 1)");
        Mock.Get(nonMatchingElement.WebElement)
            .Setup(e => e.GetCssValue("color"))
            .Returns("rgba(0, 0, 255, 1)");

        var sut = builder.CssProperty("color", "rgba(255, 0, 0, 1)").GetElementSpecification();

        Assert.Multiple(() =>
        {
            Assert.That(sut.Matches(matchingElement), Is.True, "Matching element should match");
            Assert.That(sut.Matches(nonMatchingElement), Is.False, "Non-matching element should not match");
        });
    }

    [Test, AutoMoqData]
    public void LocationShouldCreateASpecificationThatUsesLocationQuery(QueryPredicatePrototypeBuilder builder,
                                                                            SeleniumElement matchingElement,
                                                                            SeleniumElement nonMatchingElement)
    {
        Mock.Get(matchingElement.WebElement).SetupGet(e => e.Location).Returns(new Point(100, 200));
        Mock.Get(nonMatchingElement.WebElement).SetupGet(e => e.Location).Returns(new Point(150, 250));

        var sut = builder.Location(new Point(100, 200)).GetElementSpecification();

        Assert.Multiple(() =>
        {
            Assert.That(sut.Matches(matchingElement), Is.True, "Matching element should match");
            Assert.That(sut.Matches(nonMatchingElement), Is.False, "Non-matching element should not match");
        });
    }

    [Test, AutoMoqData]
    public void SizeShouldCreateASpecificationThatUsesSizeQuery(QueryPredicatePrototypeBuilder builder,
                                                                    SeleniumElement matchingElement,
                                                                    SeleniumElement nonMatchingElement)
    {
        Mock.Get(matchingElement.WebElement).SetupGet(e => e.Size).Returns(new Size(100, 200));
        Mock.Get(nonMatchingElement.WebElement).SetupGet(e => e.Size).Returns(new Size(150, 250));

        var sut = builder.Size(new Size(100, 200)).GetElementSpecification(); 

        Assert.Multiple(() =>
        {
            Assert.That(sut.Matches(matchingElement), Is.True, "Matching element should match");
            Assert.That(sut.Matches(nonMatchingElement), Is.False, "Non-matching element should not match");
        });
    }

    [Test, AutoMoqData]
    public void TextShouldCreateASpecificationThatUsesTextQuery(QueryPredicatePrototypeBuilder builder,
                                                                    SeleniumElement matchingElement,
                                                                    SeleniumElement nonMatchingElement)
    {
        Mock.Get(matchingElement.WebElement).SetupGet(e => e.Text).Returns("Hello World");
        Mock.Get(nonMatchingElement.WebElement).SetupGet(e => e.Text).Returns("Goodbye World");

        var sut = builder.Text("Hello World").GetElementSpecification();

        Assert.Multiple(() =>
        {
            Assert.That(sut.Matches(matchingElement), Is.True, "Matching element should match");
            Assert.That(sut.Matches(nonMatchingElement), Is.False, "Non-matching element should not match");
        });
    }

    [Test, AutoMoqData]
    public void ValueShouldCreateASpecificationThatUsesValueQuery(QueryPredicatePrototypeBuilder builder,
                                                                      SeleniumElement matchingElement,
                                                                      SeleniumElement nonMatchingElement)
    {
        Mock.Get(matchingElement.WebElement).Setup(e => e.GetDomProperty("value")).Returns("Hello World");
        Mock.Get(nonMatchingElement.WebElement).Setup(e => e.GetDomProperty("value")).Returns("Goodbye World");

        var sut = builder.Value("Hello World").GetElementSpecification();

        Assert.Multiple(() =>
        {
            Assert.That(sut.Matches(matchingElement), Is.True, "Matching element should match");
            Assert.That(sut.Matches(nonMatchingElement), Is.False, "Non-matching element should not match");
        });
    }
    
    [Test, AutoMoqData]
    public void VisibleShouldCreateASpecificationThatUsesVisibilityQuery(QueryPredicatePrototypeBuilder builder,
                                                                            SeleniumElement matchingElement,
                                                                            SeleniumElement nonMatchingElement)
    {
        Mock.Get(matchingElement.WebElement).SetupGet(e => e.Displayed).Returns(true);
        Mock.Get(nonMatchingElement.WebElement).SetupGet(e => e.Displayed).Returns(false);

        var sut = builder.Visible().GetElementSpecification();

        Assert.Multiple(() =>
        {
            Assert.That(sut.Matches(matchingElement), Is.True, "Matching element should match");
            Assert.That(sut.Matches(nonMatchingElement), Is.False, "Non-matching element should not match");
        });
    }
    
    [Test, AutoMoqData]
    public void SelectedOptionsWithTextShouldCreateASpecificationThatUsesOptionsQuery(QueryPredicatePrototypeBuilder builder,
                                                                                        SeleniumElement matchingElement,
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

        var sut = builder.SelectedOptionsWithText("Option 1", "Option 3").GetElementSpecification();

        Assert.Multiple(() =>
        {
            Assert.That(sut.Matches(matchingElement), Is.True, "Matching element should match");
            Assert.That(sut.Matches(nonMatchingElement), Is.False, "Non-matching element should not match");
        });
    }
    
    [Test, AutoMoqData]
    public void SelectedOptionsWithValueShouldCreateASpecificationThatUsesOptionsQuery(QueryPredicatePrototypeBuilder builder,
                                                                                        SeleniumElement matchingElement,
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

        var sut = builder.SelectedOptionsWithValue("1", "3").GetElementSpecification();

        Assert.Multiple(() =>
        {
            Assert.That(sut.Matches(matchingElement), Is.True, "Matching element should match");
            Assert.That(sut.Matches(nonMatchingElement), Is.False, "Non-matching element should not match");
        });
    }
    
    [Test, AutoMoqData]
    public void UnselectedOptionsWithTextShouldCreateASpecificationThatUsesOptionsQuery(QueryPredicatePrototypeBuilder builder,
                                                                                          SeleniumElement matchingElement,
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

        var sut = builder.UnselectedOptionsWithText("Option 2").GetElementSpecification();

        Assert.Multiple(() =>
        {
            Assert.That(sut.Matches(matchingElement), Is.True, "Matching element should match");
            Assert.That(sut.Matches(nonMatchingElement), Is.False, "Non-matching element should not match");
        });
    }
    
    [Test, AutoMoqData]
    public void UnselectedOptionsWithValueShouldCreateASpecificationThatUsesOptionsQuery(QueryPredicatePrototypeBuilder builder,
                                                                                          SeleniumElement matchingElement,
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

        var sut = builder.UnselectedOptionsWithValue("2").GetElementSpecification();

        Assert.Multiple(() =>
        {
            Assert.That(sut.Matches(matchingElement), Is.True, "Matching element should match");
            Assert.That(sut.Matches(nonMatchingElement), Is.False, "Non-matching element should not match");
        });
    }

    [Test, AutoMoqData]
    public void OptionsWithTextShouldCreateASpecificationThatUsesOptionsQuery(QueryPredicatePrototypeBuilder builder,
                                                                                SeleniumElement matchingElement,
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

        var sut = builder.OptionsWithText("Option 1", "Option 2", "Option 3").GetElementSpecification();

        Assert.Multiple(() =>
        {
            Assert.That(sut.Matches(matchingElement), Is.True, "Matching element should match");
            Assert.That(sut.Matches(nonMatchingElement), Is.False, "Non-matching element should not match");
        });
    }
    
    [Test, AutoMoqData]
    public void OptionsWithValueShouldCreateASpecificationThatUsesOptionsQuery(QueryPredicatePrototypeBuilder builder,
                                                                                SeleniumElement matchingElement,
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

        var sut = builder.OptionsWithValue("1", "2", "3").GetElementSpecification();

        Assert.Multiple(() =>
        {
            Assert.That(sut.Matches(matchingElement), Is.True, "Matching element should match");
            Assert.That(sut.Matches(nonMatchingElement), Is.False, "Non-matching element should not match");
        });
    }
}
