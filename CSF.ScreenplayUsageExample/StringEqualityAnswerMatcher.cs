using System;
using CSF.Screenplay;

namespace CSF.Screenplay.Example
{
  public class StringEqualityAnswerMatcher : IAnswerMatcher<string>
  {
    readonly string type;

    public bool IsMatch(string answer)
    {
      return answer.Equals(type, StringComparison.InvariantCulture);
    }

    public StringEqualityAnswerMatcher(string type)
    {
      if(type == null)
        throw new ArgumentNullException(nameof(type));
      this.type = type;
    }
  }
}
