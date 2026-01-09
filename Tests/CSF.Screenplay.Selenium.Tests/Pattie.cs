using System;
using CSF.Extensions.WebDriver;
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Reporting;

namespace CSF.Screenplay.Selenium;

/// <summary>
/// Pattie is an actor who can browse the web, using a different base path to <see cref="Webster"/>.
/// </summary>
/// <param name="webDriverFactory">A factory which creates <see cref="Selenium.BrowseTheWeb"/> ability instances</param>
/// <param name="pathProvider">A service which gets the file system path at which asset files should be saved</param>
public class Pattie(IGetsWebDriver webDriverFactory, IGetsAssetFilePath pathProvider) : IPersona
{
    static internal Uri TestWebappBaseUri => new Uri("http://localhost:5102/ChildPath/");

    public string Name => "Pattie";

    public Actor GetActor(Guid performanceIdentity)
    {
        var pattie = new Actor(Name, performanceIdentity);
        pattie.IsAbleTo(BrowseTheWeb());
        pattie.IsAbleTo(UseABaseUri());
        pattie.IsAbleTo(GetAssetFilePaths());
        return pattie;
    }

    BrowseTheWeb BrowseTheWeb() => new(webDriverFactory);

    GetAssetFilePaths GetAssetFilePaths() => new(pathProvider);

    static UseABaseUri UseABaseUri() => new(TestWebappBaseUri);
}