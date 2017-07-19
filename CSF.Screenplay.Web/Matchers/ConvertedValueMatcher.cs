using System;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Matchers
{
  public class ConvertedValueMatcher<T> : ElementMatcher<T>
  {
    public override string GetDescription()
    {
      return "has matching value";
    }

    protected override T GetElementData(IWebElementAdapter adapter)
    {
      var stringValue = adapter.GetValue();
      return (T) Convert.ChangeType(stringValue, typeof(T));
    }

    public ConvertedValueMatcher() : base() {}

    public ConvertedValueMatcher(Func<T,bool> predicate) : base(predicate) {}
  }
}
