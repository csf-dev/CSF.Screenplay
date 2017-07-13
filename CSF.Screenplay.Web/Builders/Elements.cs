using System;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Models;
using CSF.Screenplay.Web.Questions;

namespace CSF.Screenplay.Web.Builders
{
  public class Elements
  {
    public static IQuestion<ElementCollection> OnThePage()
    {
      return new FindElementsOnPage();
    }

    public static IQuestion<ElementCollection> In(ITarget target)
    {
      return new FindElements(target);
    }
  }
}
