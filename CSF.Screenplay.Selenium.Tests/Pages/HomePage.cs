using System;
using CSF.Screenplay.Selenium.Models;

namespace CSF.Screenplay.Selenium.Tests.Pages
{
  public class HomePage : Page
  {
    public override string GetName() => "the app home page";

    public override IUriProvider GetUriProvider() => new AppUri("Home");

    public static ITarget ImportantString => new ElementId("important_string", "the important string");

    public static ITarget ImportantNumber => new ElementId("important_number", "the important number");

    public static ITarget SecondPageLink => new ClassName("second_page_link", "the hyperlink to page two");

    public static ITarget SlowLoadingLink => new ElementId("load_in_2_seconds", "the link to reload with a 2-second delay");

    public static ITarget LoadDelay => new ElementId("load_delay", "the readout of the page-load delay");
  }
}
