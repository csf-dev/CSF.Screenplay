using System;
using System.Collections.Generic;
using System.Linq;
using CSF.Screenplay.Web.Abilities;

namespace CSF.Screenplay.Web.Tests
{
  /// <summary>
  /// Extension methods to the <see cref="BrowseTheWeb"/> class.
  /// </summary>
  public static class BrowseTheWebExtensions
  {
    /// <summary>
    /// Adds a capability to the browse the web class, except for browsers where it is unsupported.
    /// </summary>
    /// <param name="ability">Ability.</param>
    /// <param name="capabilityName">Capability name.</param>
    /// <param name="actualBrowser">Actual browser.</param>
    /// <param name="unsupportedBrowsers">Unsupported browsers.</param>
    public static void AddCapabilityExceptWhereUnsupported(this BrowseTheWeb ability,
                                                           string capabilityName,
                                                           string actualBrowser,
                                                           params string[] unsupportedBrowsers)
    {
      if(!IsInListOfBrowserNames(actualBrowser, unsupportedBrowsers))
        ability.AddCapability(capabilityName);
    }

    /// <summary>
    /// Adds a capability to the browse the web class for browsers where it is supported.
    /// </summary>
    /// <param name="ability">Ability.</param>
    /// <param name="capabilityName">Capability name.</param>
    /// <param name="actualBrowser">Actual browser.</param>
    /// <param name="supportedBrowsers">Supported browsers.</param>
    public static void AddCapabilityWhereSupported(this BrowseTheWeb ability,
                                                   string capabilityName,
                                                   string actualBrowser,
                                                   params string[] supportedBrowsers)
    {
      if(IsInListOfBrowserNames(actualBrowser, supportedBrowsers))
        ability.AddCapability(capabilityName);
    }

    static bool IsInListOfBrowserNames(string name, IEnumerable<string> names)
    {
      return names.Any(x => String.Equals(name, x, StringComparison.InvariantCultureIgnoreCase));
    }
  }
}
