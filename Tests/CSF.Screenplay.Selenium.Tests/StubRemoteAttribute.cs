using System.Collections.Generic;
using System.Reflection;
using AutoFixture;
using Moq;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace CSF.Screenplay.Selenium;

/// <summary>
/// Decorate a parameter of type <see cref="RemoteWebDriver"/> with this attribute to construct a stub remote driver.
/// </summary>
/// <remarks>
/// <para>
/// The driver won't be usable as an actual WebDriver, but it will facilitate tests which require a non-null instance.
/// </para>
/// </remarks>
public class StubRemoteAttribute : CustomizeAttribute
{
    public override ICustomization GetCustomization(ParameterInfo parameter)
        => new StubRemoteCustomization();
}

public class StubRemoteCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<RemoteWebDriver>(c => c.FromFactory(() => new StubRemoteWebDriver(Mock.Of<ICommandExecutor>(), Mock.Of<ICapabilities>())));
    }
    
    public class StubRemoteWebDriver : RemoteWebDriver
    {
        protected override Dictionary<string, object> GetCapabilitiesDictionary(ICapabilities capabilitiesToConvert)
            => new Dictionary<string, object>();

        protected override Response Execute(string driverCommandToExecute, Dictionary<string, object> parameters)
            => new Response() { Value = new Dictionary<string,object>() };

        public StubRemoteWebDriver(ICommandExecutor co, ICapabilities ca) : base(co, ca) {}
    }
}