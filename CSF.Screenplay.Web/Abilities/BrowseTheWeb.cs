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
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Web.Abilities.BrowseTheWeb"/> class.
    /// </summary>
    /// <param name="webDriver">The Selenium WebDriver instance.</param>
    /// <param name="transformer">An optoinal URI transformer.</param>
    public BrowseTheWeb(IWebDriver webDriver,
                        IUriTransformer transformer = null)
    {
      if(webDriver == null)
        throw new ArgumentNullException(nameof(webDriver));

      this.webDriver = webDriver;
      this.uriTransformer = transformer?? NoOpUriTransformer.Default;
    }
  }
}
