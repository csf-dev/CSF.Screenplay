using System;
using CSF.Extensions.WebDriver;

namespace CSF.Screenplay.Selenium;

/// <summary>
/// Webster is an actor who can browse the web.
/// </summary>
/// <param name="webDriverFactory">A factory which creates <see cref="Selenium.BrowseTheWeb"/> ability instances</param>
public class Webster(IGetsWebDriver webDriverFactory) : IPersona
{
    static internal Uri TestWebappBaseUri => new Uri("http://localhost:5102/");

    public string Name => "Webster";

    public Actor GetActor(Guid performanceIdentity)
    {
        var webster = new Actor(Name, performanceIdentity);
        webster.IsAbleTo(BrowseTheWeb());
        webster.IsAbleTo(UseABaseUri());
        return webster;
    }

    BrowseTheWeb BrowseTheWeb() => new(webDriverFactory);

    static UseABaseUri UseABaseUri() => new(TestWebappBaseUri);
}
