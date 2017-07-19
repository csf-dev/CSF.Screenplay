using System;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Matchers
{
  public class ValueMatcher : ElementMatcher<string>
  {
    public override string GetDescription()
    {
      return "has matching value";
    }

    protected override string GetElementData(IWebElementAdapter adapter)
    {
      return adapter.GetValue();
    }

    public ValueMatcher() : base() {}

    public ValueMatcher(Func<string,bool> predicate) : base(predicate) {}
  }
}
