using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Abilities;
using OpenQA.Selenium.Html5;

namespace CSF.Screenplay.Web.Actions
{
  public class ClearLocalStorage : Performable
  {
    protected override string GetReport(INamed actor)
      => $"{actor.Name} clears their browser local storage for the current site.";

    protected override void PerformAs(IPerformer actor)
    {
      var browseTheWeb = actor.GetAbility<BrowseTheWeb>();
      var driver = browseTheWeb.WebDriver;

      var hasStorageDriver = driver as IHasWebStorage;
      if(hasStorageDriver == null
         || !hasStorageDriver.HasWebStorage)
        return;

      hasStorageDriver.WebStorage.LocalStorage.Clear();
    }
  }
}
