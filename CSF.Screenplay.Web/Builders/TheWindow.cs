using System;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay.Web.Builders
{
  public class TheWindow
  {
    public static IPerformable<string> Title()
    {
      return new Questions.GetWindowTitle();
    }
  }
}
