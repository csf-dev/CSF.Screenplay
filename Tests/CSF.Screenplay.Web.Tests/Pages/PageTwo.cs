using System;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Tests.Pages
{
  public class PageTwo : Page
  {
    public override string GetName() => "the second page";

    public override IUriProvider GetUriProvider() => new AppUri("PageTwo");

    public static ITarget SpecialInputField => new CssSelector(".special_text input", "the special input field");

    public static ITarget SecondTextbox => new CssSelector(".second_textbox input", "the second text box");

    public static ITarget TheDynamicTextArea => new ElementId("dynamic_value", "the dynamic value");

    public static ITarget SingleSelectionList => new CssSelector("#single_selection", "the single selection list");

    public static ITarget SingleSelectionValue => new CssSelector("#single_selected_value", "the single-selection value");

    public static ITarget MultiSelectionList => new CssSelector("#multiple_selection", "the multi selection list");

    public static ITarget MultiSelectionValue => new CssSelector("#multiple_selected_value", "the multi-selection value");

    public static ITarget ListOfItems => new CssSelector("#list_of_items", "the list of items");

    public static ILocatorBasedTarget ItemsInTheList => new CssSelector("#list_of_items li", "items in the list");

    public static ITarget JavaScriptResult => new ElementId("ScriptOutput", "the Javascript output");
  }
}
