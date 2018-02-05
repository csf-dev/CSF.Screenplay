using System;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Tests.Pages
{
  public class GoogleHomePage : Page
  {
    public override string GetName() => "Google's US home page";

    public override IUriProvider GetUriProvider() => AppUri.Absolute("https://google.com/", "Google");
  }
}
