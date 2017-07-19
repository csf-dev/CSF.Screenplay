using System;
using System.Collections.Generic;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Matchers
{
  public class SelectedOptionsMatcher : ElementMatcher<IReadOnlyList<Option>>
  {
    public override string GetDescription()
    {
      return "has matching selected options";
    }

    protected override IReadOnlyList<Option> GetElementData(IWebElementAdapter adapter)
    {
      return adapter.GetSelectedOptions();
    }

    public SelectedOptionsMatcher() : base() {}

    public SelectedOptionsMatcher(Func<IReadOnlyList<Option>,bool> predicate) : base(predicate) {}
  }
}
