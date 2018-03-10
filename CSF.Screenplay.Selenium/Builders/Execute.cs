using System;
using CSF.Screenplay.Selenium.Actions;
using CSF.Screenplay.Selenium.StoredScripts;

namespace CSF.Screenplay.Selenium.Builders
{
  /// <summary>
  /// Builds an action which executes JavaScript on the page.
  /// </summary>
  public class Execute
  {
    /// <summary>
    /// Gets a builder which assists in the creation of a performable which executes JavaScript.
    /// </summary>
    /// <returns>A JavaScript execution builder.</returns>
    public static ExecuteJavaScriptBuilder JavaScript => new ExecuteJavaScriptBuilder();

    /// <summary>
    /// Gets a builder which assists in the creation of a performable which executes some custom/provided JavaScript.
    /// </summary>
    /// <returns>A JavaScript execution builder.</returns>
    /// <param name="script">The custom JavaScript to be executed.</param>
    public static CustomJavaScriptBuilder TheJavaScript(string script) => new CustomJavaScriptBuilder(script);
  }
}
