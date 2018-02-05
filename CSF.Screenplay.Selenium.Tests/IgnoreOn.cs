using System;
using System.Linq;
using NUnit.Framework;

namespace CSF.Screenplay.Selenium.Tests
{
  public class IgnoreOn
  {
    const string WebBrowserEnvVar = "BROWSER_NAME";

    public static void Browser(string reason, string browserName)
    {
      Browsers(reason, browserName);
    }

    public static void Browsers(string reason, params string[] browserNames)
    {
      if(reason == null)
        throw new ArgumentNullException(nameof(reason));
      if(browserNames == null)
        throw new ArgumentNullException(nameof(browserNames));

      var currentBrowser = Environment.GetEnvironmentVariable(WebBrowserEnvVar);
      if(String.IsNullOrEmpty(currentBrowser))
        return;

      var ignoredBrowsers = browserNames.AsEnumerable();
      if(ignoredBrowsers.Contains(currentBrowser))
        Assert.Ignore(reason);
    }
  }
}
