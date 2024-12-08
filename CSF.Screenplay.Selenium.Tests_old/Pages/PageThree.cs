using System;
using CSF.Screenplay.Selenium.Models;

namespace CSF.Screenplay.Selenium.Tests.Pages
{
  public class PageThree : Page
  {
    public override string GetName() => "page three";

    public override IUriProvider GetUriProvider() => new AppUri("PageThree");

    public static ITarget DelayedButtonOne => new CssSelector("#delay_click_one", "the first delay button");

    public static ITarget DelayedLinkOne => new CssSelector("#delay_appear_target_one .appeared", "the first delay-appearance link");
  }
}
