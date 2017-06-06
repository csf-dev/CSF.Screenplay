using System;
namespace CSF.Screenplay
{
  public interface IExpectationComposer<TAnswer>
  {
    Expectation<TAnswer> Compose(IAnswerMatcher<TAnswer> matcher);
  }
}
