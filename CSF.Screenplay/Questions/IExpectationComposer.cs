using System;
using CSF.Screenplay.Questions;

namespace CSF.Screenplay.Questions
{
  public interface IExpectationComposer<TAnswer>
  {
    Expectation<TAnswer> Compose(IAnswerMatcher<TAnswer> matcher);
  }
}
