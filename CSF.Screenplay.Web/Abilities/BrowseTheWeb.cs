using System;
using CSF.Screenplay.Abilities;
using OpenQA.Selenium;

namespace CSF.Screenplay.Web.Abilities
{
  /// <summary>
  /// A Screenplay ability which represents an actor's ability to use a web browser, via Selenium a WebDriver.
  /// </summary>
  public class BrowseTheWeb : Ability
  {
    readonly IWebDriver webDriver;
    readonly IUriTransformer uriTransformer;
    readonly bool requireExplicitDisposal;

    /// <summary>
    /// Gets the Selenium WebDriver instance.
    /// </summary>
    /// <value>The web driver.</value>
    public IWebDriver WebDriver => webDriver;

    /// <summary>
    /// Gets the URI transformer which should be used by the current instance.  If not specified in the constructor,
    /// this will be a <see cref="NoOpUriTransformer"/>.
    /// </summary>
    /// <value>The URI transformer.</value>
    public IUriTransformer UriTransformer => uriTransformer;

    /// <summary>
    /// Gets the report text for the current ability.
    /// </summary>
    /// <returns>The report.</returns>
    /// <param name="actor">Actor.</param>
    protected override string GetReport(Actors.INamed actor)
    {
      return $"{actor.Name} is able to browse the web.";
    }

    /// <summary>
    /// Disposes of the current instance and its resources.  This involves disposing of the underlying
    /// WebDriver instance.
    /// </summary>
    /// <param name="disposing">A value which indicates whether we are performing an explicit disposal
    /// (<c>true</c>) or not (<c>false</c>).</param>
    protected override void Dispose(bool disposing)
    {
      if(!requireExplicitDisposal || disposing)
      {
        webDriver.Dispose();
      }

      base.Dispose(disposing);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Web.Abilities.BrowseTheWeb"/> class.
    /// </summary>
    /// <param name="webDriver">The Selenium WebDriver instance.</param>
    /// <param name="transformer">An optoinal URI transformer.</param>
    /// <param name="requireExplicitDisposal">When provided and set to <c>true</c>, indicates that this instance
    /// should not dispose its WebDriver unless disposal is explicitly requested.</param>
    public BrowseTheWeb(IWebDriver webDriver,
                        IUriTransformer transformer = null,
                        bool requireExplicitDisposal = false)
    {
      if(webDriver == null)
        throw new ArgumentNullException(nameof(webDriver));

      this.webDriver = webDriver;
      this.uriTransformer = transformer?? NoOpUriTransformer.Default;
      this.requireExplicitDisposal = requireExplicitDisposal;
    }
  }
}
