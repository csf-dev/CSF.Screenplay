using CSF.Screenplay.Abilities;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Tasks
{
  public interface ITask
  {
    void PerformAs(IPerformer actor);
  }
}