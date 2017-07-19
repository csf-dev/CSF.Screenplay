using System;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Matchers
{
  public class SizeMatcher : ElementMatcher<Size>
  {
    public override string GetDescription()
    {
      return "has matching size";
    }

    protected override Size GetElementData(IWebElementAdapter adapter)
    {
      return adapter.GetSize();
    }

    public SizeMatcher() : base() {}

    public SizeMatcher(Func<Size,bool> predicate) : base(predicate) {}
  }
}
