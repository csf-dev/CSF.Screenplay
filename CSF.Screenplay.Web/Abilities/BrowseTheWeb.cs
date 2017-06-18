using System;
using CSF.Screenplay.Abilities;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Abilities
{
  public class BrowseTheWeb : Ability
  {
    readonly IWebDriver webDriver;

    public IWebDriver WebDriver => webDriver;

    public override string GetReport(Actors.INamed actor)
    {
      return $"{actor.Name} is able to browse the web.";
    }

    public BrowseTheWeb(IWebDriver webDriver)
    {
      if(webDriver == null)
        throw new ArgumentNullException(nameof(webDriver));

      this.webDriver = webDriver;
    }
  }
}
