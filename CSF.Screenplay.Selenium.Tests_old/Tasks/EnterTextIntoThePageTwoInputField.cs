using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Selenium.Builders;
using CSF.Screenplay.Selenium.Tests.Pages;

namespace CSF.Screenplay.Selenium.Tests.Tasks
{
  public class EnterTextIntoThePageTwoInputField : Performable
  {
    readonly string text;

    protected override string GetReport(INamed actor)
      => $"{actor.Name} enters '{text}' into the input field on page two";

    protected override void PerformAs(IPerformer actor)
    {
      actor.Perform(OpenTheirBrowserOn.ThePage<PageTwo>());
      actor.Perform(Enter.TheText(text).Into(PageTwo.SpecialInputField));
    }

    public EnterTextIntoThePageTwoInputField(string text)
    {
      if(text == null)
        throw new ArgumentNullException(nameof(text));

      this.text = text;
    }
  }
}
