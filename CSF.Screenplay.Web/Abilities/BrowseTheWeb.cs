using System;
using CSF.Screenplay.Abilities;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Abilities
{
  public class BrowseTheWeb : Ability
  {
    readonly IWebDriver webDriver;
    readonly IUrlTransformer urlTransformer;

    public IWebDriver WebDriver => webDriver;

    public IUrlTransformer UrlTransformer => urlTransformer;

    protected override string GetReport(Actors.INamed actor)
    {
      return $"{actor.Name} is able to browse the web.";
    }

    protected override void Dispose(bool disposing)
    {
      webDriver.Dispose();
      base.Dispose(disposing);
    }

    public BrowseTheWeb(IWebDriver webDriver, IUrlTransformer transformer)
    {
      if(transformer == null)
        throw new ArgumentNullException(nameof(transformer));
      if(webDriver == null)
        throw new ArgumentNullException(nameof(webDriver));

      this.webDriver = webDriver;
      this.urlTransformer = transformer;
    }
  }
}
