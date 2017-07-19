using System;
using System.Collections.Generic;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Matchers
{
  public class OptionsMatcher : ElementMatcher<IReadOnlyList<Option>>
  {
    public override string GetDescription()
    {
      return "has matching options";
    }

    protected override IReadOnlyList<Option> GetElementData(IWebElementAdapter adapter)
    {
      return adapter.GetAllOptions();
    }

    public OptionsMatcher() : base() {}

    public OptionsMatcher(Func<IReadOnlyList<Option>,bool> predicate) : base(predicate) {}
  }
}
