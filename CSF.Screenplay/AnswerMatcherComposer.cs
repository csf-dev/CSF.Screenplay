using System;
using CSF.Screenplay.Questions;

namespace CSF.Screenplay
{
  public static class AnswerMatcherComposer
  {
    public static IAnswerMatcher<T> Is<T>(T val)
    {
      // TODO: Write this implementation
      throw new NotImplementedException();
    }

    public static IAnswerMatcher<T> Are<T>(T val)
    {
      return Is(val);
    }
  }
}
