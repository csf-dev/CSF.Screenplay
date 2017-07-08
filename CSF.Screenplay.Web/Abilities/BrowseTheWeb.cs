using System;
using CSF.Screenplay.Abilities;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Abilities
{
  public class BrowseTheWeb : Ability
  {
    readonly IWebDriver webDriver;
    readonly IUriTransformer uriTransformer;
    readonly bool requireExplicitDisposal;

    public IWebDriver WebDriver => webDriver;

    public IUriTransformer UriTransformer => uriTransformer;

    protected override string GetReport(Actors.INamed actor)
    {
      return $"{actor.Name} is able to browse the web.";
    }

    protected override void Dispose(bool disposing)
    {
      if(!requireExplicitDisposal || disposing)
      {
        webDriver.Dispose();
      }

      base.Dispose(disposing);
    }

    public BrowseTheWeb(IWebDriver webDriver,
                        IUriTransformer transformer = null,
                        bool requireExplicitDisposal = false)
    {
      if(webDriver == null)
        throw new ArgumentNullException(nameof(webDriver));

      this.webDriver = webDriver;
      this.uriTransformer = transformer?? new NoOpUrlTransformer();
      this.requireExplicitDisposal = requireExplicitDisposal;
    }
  }
}
