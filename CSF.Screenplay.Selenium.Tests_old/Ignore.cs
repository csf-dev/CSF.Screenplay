using System;
using System.Linq;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Selenium.Abilities;
using CSF.WebDriverExtras;
using NUnit.Framework;
using OpenQA.Selenium;

namespace CSF.Screenplay.Selenium.Tests
{
  public static class Ignore
  {
    public static void UnlessBrowserHasAnyOfTheFlags(BrowseTheWeb browser,
                                                     params string[] flags)
    {
      UnlessBrowserHasAnyOfTheFlags(browser.FlagsDriver, flags);
    }

    public static void UnlessBrowserHasAnyOfTheFlags(IHasFlags driver,
                                                     params string[] flags)
    {
      if(driver == null)
        throw new ArgumentNullException(nameof(driver));
      if(flags == null || flags.Length == 0)
        throw new ArgumentException("You must list at least one flag.", nameof(flags));
      
      var hasFlag = flags.Any(x => driver.HasFlag(x));
      if(!hasFlag)
        Assert.Ignore("The web browser does not have any of the required flags: {0}", String.Join(", ", flags));
    }

    public static void IfBrowserHasAnyOfTheFlags(BrowseTheWeb browser,
                                                 params string[] flags)
    {
      IfBrowserHasAnyOfTheFlags(browser.FlagsDriver, flags);
    }

    public static void IfBrowserHasAnyOfTheFlags(IHasFlags driver,
                                                 params string[] flags)
    {
      if(driver == null)
        throw new ArgumentNullException(nameof(driver));
      if(flags == null || flags.Length == 0)
        throw new ArgumentException("You must list at least one flag.", nameof(flags));

      var hasFlag = flags.Any(x => driver.HasFlag(x));
      if(hasFlag)
        Assert.Ignore("The web browser has one of a number of disallowed flags: {0}", String.Join(", ", flags));
    }

    public static void ShouldIgnoreThisTestIfTheirBrowserHasAnyOfTheFlags(this IActor actor,
                                                                          params string[] flags)
    {
      if(actor == null)
        throw new ArgumentNullException(nameof(actor));
      
      var browseTheWeb = actor.GetAbility<BrowseTheWeb>();
      if(browseTheWeb == null)
        throw new ArgumentException($"The actor must have the {nameof(BrowseTheWeb)} ability.", nameof(actor));

      IfBrowserHasAnyOfTheFlags(browseTheWeb, flags);
    }

    public static void ShouldIgnoreThisTestUnlessTheirBrowserHasAnyOfTheFlags(this IActor actor,
                                                                              params string[] flags)
    {
      if(actor == null)
        throw new ArgumentNullException(nameof(actor));

      var browseTheWeb = actor.GetAbility<BrowseTheWeb>();
      if(browseTheWeb == null)
        throw new ArgumentException($"The actor must have the {nameof(BrowseTheWeb)} ability.", nameof(actor));

      UnlessBrowserHasAnyOfTheFlags(browseTheWeb, flags);
    }
  }
}
