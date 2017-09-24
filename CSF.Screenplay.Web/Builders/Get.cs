using System;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Web.Models;
using CSF.Screenplay.Web.Questions;

namespace CSF.Screenplay.Web.Builders
{
  public class Get
  {
    public static IQuestion<IWebElementAdapter> TheElement(ILocatorBasedTarget target)
      => new GetElement(target);
  }
}
