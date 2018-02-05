using System;
using System.Collections.Generic;
using System.Linq;
using CSF.Screenplay.Abilities;
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
    readonly ISet<string> webBrowserCapabilities;

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
    /// Marks the current ability as being capable of an operation.
    /// </summary>
    /// <param name="capabilityName">Capability name.</param>
    public void AddCapability(string capabilityName) => webBrowserCapabilities.Add(capabilityName);

    /// <summary>
    /// Gets a value indicating whether the current web-browsing ability if capable of an operation.
    /// </summary>
    /// <returns><c>true</c>, the browser is capable of the listed operation, <c>false</c> otherwise.</returns>
    /// <param name="capabilityName">Capability name.</param>
    public bool GetCapability(string capabilityName) => webBrowserCapabilities.Contains(capabilityName);

    /// <summary>
    /// Checks that the current instance contains the 
    /// </summary>
    /// <param name="capabilityName">Capability name.</param>
    public void DemandCapability(string capabilityName)
    {
      var isCapable = GetCapability(capabilityName);
      if(!isCapable)
        throw new MissingCapabilityException($"The capability '{capabilityName}' is required but was not provided.");
    }
    /// <summary>
    /// Adds a capability to the browse the web class, except for browsers where it is unsupported.
    /// </summary>
    /// <param name="capabilityName">Capability name.</param>
    /// <param name="actualBrowser">Actual browser.</param>
    /// <param name="unsupportedBrowsers">Unsupported browsers.</param>
    public void AddCapabilityExceptWhereUnsupported(string capabilityName,
                                                    string actualBrowser,
                                                    params string[] unsupportedBrowsers)
    {
      if(!IsInListOfBrowserNames(actualBrowser, unsupportedBrowsers))
        AddCapability(capabilityName);
    }

    /// <summary>
    /// Adds a capability to the browse the web class for browsers where it is supported.
    /// </summary>
    /// <param name="capabilityName">Capability name.</param>
    /// <param name="actualBrowser">Actual browser.</param>
    /// <param name="supportedBrowsers">Supported browsers.</param>
    public void AddCapabilityWhereSupported(string capabilityName,
                                            string actualBrowser,
                                            params string[] supportedBrowsers)
    {
      if(IsInListOfBrowserNames(actualBrowser, supportedBrowsers))
        AddCapability(capabilityName);
    }

    static bool IsInListOfBrowserNames(string name, IEnumerable<string> names)
    {
      return names.Any(x => String.Equals(name, x, StringComparison.InvariantCultureIgnoreCase));
    }

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

      webBrowserCapabilities = new HashSet<string>();
    }
  }
}
