using System;
namespace CSF.Screenplay
{
  public interface IAnswerMatcher<TAnswer>
  {
    bool IsMatch(TAnswer answer);
  }
}
