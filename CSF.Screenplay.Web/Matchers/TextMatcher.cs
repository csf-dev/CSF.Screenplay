using System;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Matchers
{
  public class TextMatcher : ElementMatcher<string>
  {
    public override string GetDescription()
    {
      return "has matching text";
    }

    protected override string GetElementData(IWebElementAdapter adapter)
    {
      return adapter.GetText();
    }

    public TextMatcher() : base() {}

    public TextMatcher(Func<string,bool> predicate) : base(predicate) {}
  }
}
