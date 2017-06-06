using System;
using CSF.Screenplay;

namespace CSF.Screenplay.Example
{
  public static class ExpectationComposerExtensions
  {
    public static Expectation<string> WhichSay(this IExpectationComposer<string> composer, string type)
    {
      var matcher = new StringEqualityAnswerMatcher(type);
      return composer.Compose(matcher);
    }
  }
}
