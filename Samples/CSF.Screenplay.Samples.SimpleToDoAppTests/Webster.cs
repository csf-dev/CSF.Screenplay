using System;
using CSF.Extensions.WebDriver;

namespace CSF.Screenplay;

public class Webster(IGetsWebDriver webDriverFactory) : IPersona
{
    public string Name => nameof(Webster);

    public Actor GetActor(Guid performanceIdentity)
    {
        var webster = new Actor(Name, performanceIdentity);
        webster.IsAbleTo(BrowseTheWeb());
        webster.IsAbleTo(UseABaseUri());
        return webster;
    }

    BrowseTheWeb BrowseTheWeb() => new(webDriverFactory);

    static UseABaseUri UseABaseUri() => new(TestRunSetup.SampleAppUri);
}