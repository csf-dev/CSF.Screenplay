using CSF.Screenplay.Abilities;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Questions
{
  public interface IQuestion
  {
    object GetAnswer(IPerformer actor);
  }
}