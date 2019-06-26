using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Builders;
using CSF.Screenplay.Selenium.Models;

public class OpenAnEmptyToDoList : Performable
{
  protected override string GetReport(INamed actor)
  {
    return $"{actor.Name} opens an empty to-do list.";
  }

  protected override void PerformAs(IPerformer actor)
  {
    // You may need to update the URL based on your test hosting
    actor.Perform(OpenTheirBrowserOn.TheUrl("http://localhost/"));
    var thePage = new CssSelector("body", "the page");
    actor.Perform(Wait.Until(thePage).IsVisible());
  }
}