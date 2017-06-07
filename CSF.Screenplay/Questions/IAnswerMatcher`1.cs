using System;
namespace CSF.Screenplay.Questions
{
  public interface IAnswerMatcher<TAnswer>
  {
    bool IsMatch(TAnswer answer);
  }
}
