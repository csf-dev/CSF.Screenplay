using System;
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Actions;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Tasks;

namespace CSF.Screenplay.Questions
{
  public abstract class Question<TAnswer> : IQuestion<TAnswer>, ITask<TAnswer>, ITaskWithResult
  {
    protected abstract TAnswer GetAnswer(IPerformer actor);

    TAnswer IQuestion<TAnswer>.GetAnswer(IPerformer actor)
    {
      return GetAnswer(actor);
    }

    object IQuestion.GetAnswer(IPerformer actor)
    {
      return GetAnswer(actor);
    }

    TAnswer ITask<TAnswer>.Execute(IPerformer actor)
    {
      return GetAnswer(actor);
    }

    object ITaskWithResult.Execute(IPerformer actor)
    {
      return GetAnswer(actor);
    }
  }
}
