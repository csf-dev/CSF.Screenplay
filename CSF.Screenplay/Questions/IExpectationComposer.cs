using System;
using CSF.Screenplay.Questions;

namespace CSF.Screenplay.Questions
{
  public interface IExpectationComposer<TAnswer>
  {
    Expectation<TAnswer> Which(IAnswerMatcher<TAnswer> matcher);
  }
}
