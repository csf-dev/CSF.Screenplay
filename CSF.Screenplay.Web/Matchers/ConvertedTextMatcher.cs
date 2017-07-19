using System;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Matchers
{
  public class ConvertedTextMatcher<T> : ElementMatcher<T>
  {
    public override string GetDescription()
    {
      return "has matching text";
    }

    protected override T GetElementData(IWebElementAdapter adapter)
    {
      var stringValue = adapter.GetText();
      return (T) Convert.ChangeType(stringValue, typeof(T));
    }

    public ConvertedTextMatcher() : base() {}

    public ConvertedTextMatcher(Func<T,bool> predicate) : base(predicate) {}
  }
}
