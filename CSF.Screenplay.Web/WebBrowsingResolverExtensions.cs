using System;
using CSF.Screenplay.Scenarios;
using CSF.Screenplay.Web.Abilities;
using OpenQA.Selenium;

namespace CSF.Screenplay
{
  /// <summary>
  /// Extension methods related to the resolving of web-browsing-related services.
  /// </summary>
  public static class WebBrowsingResolverExtensions
  {
    /// <summary>
    /// Gets the web browsing ability from the current resolver.
    /// </summary>
    /// <returns>The web browser.</returns>
    /// <param name="resolver">Resolver.</param>
    /// <param name="name">Name.</param>
    public static BrowseTheWeb GetWebBrowser(this IServiceResolver resolver, string name = null)
    {
      if(resolver == null)
        throw new ArgumentNullException(nameof(resolver));

      return resolver.GetService<BrowseTheWeb>(name);
    }
  }
}
