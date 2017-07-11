using System;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Tests.Pages
{
  public class PageTwo : Page
  {
    public override string GetName() => "the second page";

    public override IUriProvider GetUri() => new AppUri("PageTwo");

    public ITarget SpecialInputField => new CssSelector(".special_text input", "the special input field");

    public ITarget TheDynamicTextArea => new ElementId("dynamic_value", "the dynamic value");

    public ITarget SingleSelectionList => new CssSelector("#single_selection", "the single selection list");

    public ITarget SingleSelectionValue => new CssSelector("#single_selected_value", "the single selection value");
  }
}
