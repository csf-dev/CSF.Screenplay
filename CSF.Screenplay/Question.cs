using System;
namespace CSF.Screenplay
{
  public abstract class Question<TAnswer> : IQuestion<TAnswer>, IQuestion
  {
    public abstract TAnswer GetAnswer(ICanPerformActions actor);

    object IQuestion.GetAnswer(ICanPerformActions actor)
    {
      return GetAnswer(actor);
    }
  }
}
