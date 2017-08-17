using System;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Tests.Pages
{
  public class HomePage : Page
  {
    public override string GetName() => "the app home page";

    public override IUriProvider GetUriProvider() => new AppUri("Home");

    public static ITarget ImportantString => new ElementId("important_string", "the important string");

    public static ITarget ImportantNumber => new ElementId("important_number", "the important number");

    public static ITarget SecondPageLink => new ClassName("second_page_link", "the hyperlink to page two");
  }
}
