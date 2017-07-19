using System;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Builders
{
  public class Elements
  {
    public static ElementsOnPage OnThePage()
    {
      return new ElementsOnPage();
    }

    public static ElementsWithin In(ITarget target)
    {
      return new ElementsWithin(target);
    }
  }
}
