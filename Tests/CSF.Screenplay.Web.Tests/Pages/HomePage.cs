using System;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Tests.Pages
{
  public class HomePage : Page
  {
    public override string GetName() => "the app home page";

    public override IUriProvider GetUri() => new AppUri("");

    public ITarget ImportantString => new IdTarget("important_string", "the important string");

    public ITarget ImportantNumber => new IdTarget("important_number", "the important number");

    public ITarget SecondPageLink => new ClassNameTarget("second_page_link", "the hyperlink to page two");
  }
}
