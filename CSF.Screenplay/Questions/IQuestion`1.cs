using System;
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Questions
{
  public interface IQuestion<TAnswer> : IQuestion
  {
    new TAnswer GetAnswer(IPerformer actor);
  }
}
