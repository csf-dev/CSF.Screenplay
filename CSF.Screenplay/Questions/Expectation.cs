using CSF.Screenplay.Abilities;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Questions
{
  public class Expectation<TAnswer> : IExpectation
  {
    readonly IAnswerMatcher<TAnswer> matcher;
    readonly IQuestion<TAnswer> question;
    
    public virtual void Verify(IPerformer actor)
    {
      var answer = GetAnswer(actor);
      var success = IsMatch(answer);
      // Perform assertion that isMatch is true
    }

    protected virtual TAnswer GetAnswer(IPerformer actor)
    {
      return question.GetAnswer(actor);
    }

    protected virtual bool IsMatch(TAnswer answer)
    {
      return matcher.IsMatch(answer);
    }

    public Expectation(IQuestion<TAnswer> question, IAnswerMatcher<TAnswer> matcher)
    {
      if(matcher == null)
        throw new System.ArgumentNullException(nameof(matcher));
      if(question == null)
        throw new System.ArgumentNullException(nameof(question));
      this.matcher = matcher;
      this.question = question;
    }
  }
}