using System;
using System.Collections.ObjectModel;
using Moq;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Elements;

[TestFixture, Parallelizable]
public class ShadowRootAdapterTests
{
    [Test, AutoMoqData]
    public void FindElementShouldExerciseWrappedImpl([Frozen] ISearchContext wrapped, ShadowRootAdapter sut)
    {
        var by = By.Id("foo");
        sut.FindElement(by);
        Mock.Get(wrapped).Verify(x => x.FindElement(by));
    }

    [Test, AutoMoqData]
    public void FindElementsShouldExerciseWrappedImpl([Frozen] ISearchContext wrapped, ShadowRootAdapter sut)
    {
        var by = By.Id("foo");
        Mock.Get(wrapped).Setup(x => x.FindElements(by)).Returns(new ReadOnlyCollection<IWebElement>([]));
        sut.FindElements(by);
        Mock.Get(wrapped).Verify(x => x.FindElements(by));
    }

    [Test, AutoMoqData]
    public void TagNameShouldReturnHardcodedResult(ShadowRootAdapter sut)
    {
        Assert.That(sut.TagName, Is.EqualTo("#shadow-root"));
    }

    [Test, AutoMoqData]
    public void TextShouldThrow(ShadowRootAdapter sut)
    {
        Assert.That(() => sut.Text, Throws.InstanceOf<NotSupportedException>());
    }


    [Test, AutoMoqData]
    public void EnabledShouldThrow(ShadowRootAdapter sut)
    {
        Assert.That(() => sut.Enabled, Throws.InstanceOf<NotSupportedException>());
    }

    [Test, AutoMoqData]
    public void SelectedShouldThrow(ShadowRootAdapter sut)
    {
        Assert.That(() => sut.Selected, Throws.InstanceOf<NotSupportedException>());
    }

    [Test, AutoMoqData]
    public void LocationShouldThrow(ShadowRootAdapter sut)
    {
        Assert.That(() => sut.Location, Throws.InstanceOf<NotSupportedException>());
    }

    [Test, AutoMoqData]
    public void SizeShouldThrow(ShadowRootAdapter sut)
    {
        Assert.That(() => sut.Size, Throws.InstanceOf<NotSupportedException>());
    }

    [Test, AutoMoqData]
    public void DisplayedShouldThrow(ShadowRootAdapter sut)
    {
        Assert.That(() => sut.Displayed, Throws.InstanceOf<NotSupportedException>());
    }

    [Test, AutoMoqData]
    public void ClearShouldThrow(ShadowRootAdapter sut)
    {
        Assert.That(sut.Clear, Throws.InstanceOf<NotSupportedException>());
    }

    [Test, AutoMoqData]
    public void ClickShouldThrow(ShadowRootAdapter sut)
    {
        Assert.That(sut.Click, Throws.InstanceOf<NotSupportedException>());
    }

    [Test, AutoMoqData]
    public void GetAttributeShouldThrow(ShadowRootAdapter sut, string attributeName)
    {
        Assert.That(() => sut.GetAttribute(attributeName), Throws.InstanceOf<NotSupportedException>());
    }

    [Test, AutoMoqData]
    public void GetCssValueShouldThrow(ShadowRootAdapter sut, string propertyName)
    {
        Assert.That(() => sut.GetCssValue(propertyName), Throws.InstanceOf<NotSupportedException>());
    }

    [Test, AutoMoqData]
    public void GetDomAttributeShouldThrow(ShadowRootAdapter sut, string attributeName)
    {
        Assert.That(() => sut.GetDomAttribute(attributeName), Throws.InstanceOf<NotSupportedException>());
    }

    [Test, AutoMoqData]
    public void GetDomPropertyShouldThrow(ShadowRootAdapter sut, string propertyName)
    {
        Assert.That(() => sut.GetDomProperty(propertyName), Throws.InstanceOf<NotSupportedException>());
    }

    [Test, AutoMoqData]
    public void GetPropertyShouldThrow(ShadowRootAdapter sut, string propertyName)
    {
        Assert.That(() => sut.GetProperty(propertyName), Throws.InstanceOf<NotSupportedException>());
    }

    [Test, AutoMoqData]
    public void GetShadowRootShouldThrow(ShadowRootAdapter sut)
    {
        Assert.That(sut.GetShadowRoot, Throws.InstanceOf<NotSupportedException>());
    }

    [Test, AutoMoqData]
    public void SendKeysShouldThrow(ShadowRootAdapter sut, string text)
    {
        Assert.That(() => sut.SendKeys(text), Throws.InstanceOf<NotSupportedException>());
    }

    [Test, AutoMoqData]
    public void SubmitShouldThrow(ShadowRootAdapter sut)
    {
        Assert.That(sut.Submit, Throws.InstanceOf<NotSupportedException>());
    }
}