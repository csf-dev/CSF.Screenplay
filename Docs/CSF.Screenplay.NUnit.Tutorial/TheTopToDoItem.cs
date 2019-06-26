using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Builders;
using CSF.Screenplay.Selenium.Models;

public class TheTopToDoItem : Performable<string>, IQuestion<string>
{
  protected override string GetReport(INamed actor)
  {
    return $"{actor.Name} reads the top to-do item.";
  }

  protected override string PerformAs(IPerformer actor)
  {
    var topItem = new CssSelector("#toDoList :first-child", "the top to-do item");
    var text = actor.Perform(TheText.Of(topItem));
    return text;
  }

  public static IQuestion<string> FromTheList() => new TheTopToDoItem();
}