using System.Reflection;
using AutoFixture;
using CSF.Extensions.WebDriver;
using CSF.Extensions.WebDriver.Factories;
using Moq;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium;

public class MockDriverAttribute : CustomizeAttribute
{
    public override ICustomization GetCustomization(ParameterInfo parameter)
        => new MockDriverCustomization();
}

public class MockDriverCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<IWebDriver>(c =>
        {
            return c
                .FromFactory(() => (IWebDriver) new Mock<IWebDriver>().As<IJavaScriptExecutor>().Object);
        });
        fixture.Customize<IGetsWebDriver>(c => c.FromFactory((WebDriverAndOptions d) => Mock.Of<IGetsWebDriver>(m => m.GetDefaultWebDriver(null) == d
                                                                                                                  && m.GetWebDriver(It.IsAny<string>(), null) == d)));
        fixture.Customize<BrowseTheWeb>(c => c.FromFactory((IGetsWebDriver d) => new BrowseTheWeb(d)));
    }
}