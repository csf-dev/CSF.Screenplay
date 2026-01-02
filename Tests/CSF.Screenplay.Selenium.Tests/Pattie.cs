using System;
using CSF.Extensions.WebDriver;

namespace CSF.Screenplay.Selenium;

public class Pattie(IGetsWebDriver webDriverFactory) : IPersona
{
    static internal Uri TestWebappBaseUri => new Uri("http://localhost:5102/ChildPath/");

    public string Name => "Pattie";

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