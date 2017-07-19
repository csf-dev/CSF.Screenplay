using System;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Matchers
{
  public class LocationMatcher : ElementMatcher<Position>
  {
    public override string GetDescription()
    {
      return "has matching screen location";
    }

    protected override Position GetElementData(IWebElementAdapter adapter)
    {
      return adapter.GetLocation();
    }

    public LocationMatcher() : base() {}

    public LocationMatcher(Func<Position,bool> predicate) : base(predicate) {}
  }
}
