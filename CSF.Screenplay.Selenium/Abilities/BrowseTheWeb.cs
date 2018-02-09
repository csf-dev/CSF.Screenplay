using System;
using System.Collections.Generic;
using System.Linq;
using CSF.Screenplay.Abilities;
using CSF.WebDriverExtras;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Abilities
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
    /// Gets the <c>WebDriver</c> as an instance of <c>IHasFlags</c>.
    /// </summary>
    /// <value>A service which exposes the browser flags.</value>
    public IHasFlags FlagsDriver
    {
      get {
        var output = WebDriver as IHasFlags;
        return output ?? EmptyFlagsDriver.Singleton;
      }
    }

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
      return $"{actor.Name} is able to browse the web using {WebDriver.GetIdentification().ToString()}";
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Selenium.Abilities.BrowseTheWeb"/> class.
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

    class EmptyFlagsDriver : IHasFlags
    {
      static readonly string[] flags = new string[0];
      static readonly EmptyFlagsDriver singleton = new EmptyFlagsDriver();

      public string GetFirstFlagPresent(IList<string> flags) => null;

      public string GetFirstFlagPresent(params string[] flags) => null;

      public IReadOnlyCollection<string> GetFlags() => flags;

      public bool HasFlag(string flag) => false;

      internal static IHasFlags Singleton => singleton;
    }
  }
}
