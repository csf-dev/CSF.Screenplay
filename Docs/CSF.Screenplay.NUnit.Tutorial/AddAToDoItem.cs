using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Builders;
using CSF.Screenplay.Selenium.Models;

public class AddAToDoItem : Performable
{
  readonly string item;

  protected override string GetReport(INamed actor)
  {
    return $"{actor.Name} adds a to-do item named '{item}'.";
  }

  protected override void PerformAs(IPerformer actor)
  {
    var newItemTextbox = new CssSelector("#newItemText", "the new-item text box");
    actor.Perform(Enter.TheText(item).Into(newItemTextbox));

    var theAddButton = new CssSelector("#newItemButton", "the add-item button");
    actor.Perform(Click.On(theAddButton));
  }

  public AddAToDoItem(string item)
  {
    this.item = item;
  }

  public static IPerformable Named(string item)
  {
    return new AddAToDoItem(item);
  }
}