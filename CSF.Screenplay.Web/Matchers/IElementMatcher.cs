using System;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Matchers
{
  public interface IElementMatcher
  {
    ITarget TargetMatch { get; }

    Func<IWebElementAdapter,bool> GetMatchPredicate();

    string GetDescription();
  }
}
