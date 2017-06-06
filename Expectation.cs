public class Expectation<TAnswer> : IExpectation
{
  readonly IAnswerMatcher<TAnswer> matcher;
  readonly IQuestion<TAnswer> question;
  
  public virtual void Verify(ICanPerformActions actor)
  {
    var answer = question.GetAnswer();
    var isMatch = matcher.Match(answer);
    // Perform assertion that isMatch is true
  }
  
  public Expectation(IAnswerMatcher<TAnswer> matcher, IQuestion<TAnswer> question)
  {
    // Usual ctor stuff omitted
  }
}