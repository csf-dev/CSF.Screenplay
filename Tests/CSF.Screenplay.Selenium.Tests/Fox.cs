using System;
using CSF.Extensions.WebDriver;
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Reporting;

namespace CSF.Screenplay.Selenium;

/// <summary>
/// Fox is an actor who can browse the web using the Firefox web browser.
/// They're otherwise identical to <see cref="Webster"/>.
/// </summary>
/// <param name="webDriverFactory">A factory which creates <see cref="Selenium.BrowseTheWeb"/> ability instances</param>
/// <param name="pathProvider">A service which gets the file system path at which asset files should be saved</param>
public class Fox(IGetsWebDriver webDriverFactory, IGetsAssetFilePath pathProvider) : IPersona
{
    static internal Uri TestWebappBaseUri => new Uri("http://localhost:5102/");

    public string Name => "Fox";

    public Actor GetActor(Guid performanceIdentity)
    {
        var webster = new Actor(Name, performanceIdentity);
        webster.IsAbleTo(BrowseTheWeb());
        webster.IsAbleTo(UseABaseUri());
        webster.IsAbleTo(GetAssetFilePaths());
        return webster;
    }

    BrowseTheWeb BrowseTheWeb() => new(webDriverFactory, "DefaultFirefox", collectLogs: true);

    GetAssetFilePaths GetAssetFilePaths() => new(pathProvider);

    static UseABaseUri UseABaseUri() => new(TestWebappBaseUri);
}
