using System;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Matchers
{
  public class AttributeMatcher : ElementMatcher<string>
  {
    readonly string attributeName;

    public override string GetDescription()
    {
      return $"has a matching {attributeName} attribute";
    }

    protected override string GetElementData(IWebElementAdapter adapter)
    {
      return adapter.GetAttributeValue(attributeName);
    }

    public AttributeMatcher(string attributeName) : base()
    {
      if(attributeName == null)
        throw new ArgumentNullException(nameof(attributeName));

      this.attributeName = attributeName;
    }

    public AttributeMatcher(Func<string,bool> predicate, string attributeName) : base(predicate)
    {
      if(attributeName == null)
        throw new ArgumentNullException(nameof(attributeName));

      this.attributeName = attributeName;
    }
  }
}
