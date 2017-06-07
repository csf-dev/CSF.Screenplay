using System;
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Questions
{
  public abstract class Question<TAnswer> : IQuestion<TAnswer>, IQuestion
  {
    public abstract TAnswer GetAnswer(IPerformer actor);

    object IQuestion.GetAnswer(IPerformer actor)
    {
      return GetAnswer(actor);
    }
  }
}
