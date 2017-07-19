using System;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Matchers
{
  public class CssValueMatcher : ElementMatcher<string>
  {
    readonly string propertyName;

    public override string GetDescription()
    {
      return $"has a matching {propertyName} CSS property";
    }

    protected override string GetElementData(IWebElementAdapter adapter)
    {
      return adapter.GetCssValue(propertyName);
    }

    public CssValueMatcher(string propertyName) : base()
    {
      if(propertyName == null)
        throw new ArgumentNullException(nameof(propertyName));

      this.propertyName = propertyName;
    }

    public CssValueMatcher(Func<string,bool> predicate, string propertyName) : base(predicate)
    {
      if(propertyName == null)
        throw new ArgumentNullException(nameof(propertyName));

      this.propertyName = propertyName;
    }
  }
}
