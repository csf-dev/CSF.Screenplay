using System;
using CSF.Screenplay.Scenarios;
using CSF.Screenplay.Web.Abilities;

namespace CSF.Screenplay
{
  public static class WebBrowsingResolverExtensions
  {
    public static BrowseTheWeb GetWebBrowser(this IServiceResolver resolver, string name = null)
    {
      if(resolver == null)
        throw new ArgumentNullException(nameof(resolver));

      return resolver.GetService<BrowseTheWeb>(name);
    }
  }
}
