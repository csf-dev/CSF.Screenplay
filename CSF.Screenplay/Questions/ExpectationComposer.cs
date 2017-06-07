using System;
namespace CSF.Screenplay.Questions
{
  public class ExpectationComposer<TAnswer> : IExpectationComposer<TAnswer>
  {
    readonly IQuestion<TAnswer> question;

    Expectation<TAnswer> IExpectationComposer<TAnswer>.Compose(IAnswerMatcher<TAnswer> matcher)
    {
      if(matcher == null)
        throw new ArgumentNullException(nameof(matcher));

      return new Expectation<TAnswer>(question, matcher);
    }

    public ExpectationComposer(IQuestion<TAnswer> question)
    {
      if(question == null)
        throw new ArgumentNullException(nameof(question));
      this.question = question;
    }
  }
}
