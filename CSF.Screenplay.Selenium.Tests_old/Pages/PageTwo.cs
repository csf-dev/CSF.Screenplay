using System;
using CSF.Screenplay.Selenium.Models;

namespace CSF.Screenplay.Selenium.Tests.Pages
{
  public class PageTwo : Page
  {
    public override string GetName() => "the second page";

    public override IUriProvider GetUriProvider() => new AppUri("PageTwo");

    public static ILocatorBasedTarget SpecialInputField => new CssSelector(".special_text input", "the special input field");

    public static ITarget SecondTextbox => new CssSelector(".second_textbox input", "the second text box");

    public static ITarget TheDynamicTextArea => new ElementId("dynamic_value", "the dynamic value");

    public static ITarget JavaScriptResult => new ElementId("ScriptOutput", "the Javascript output");
  }
}
