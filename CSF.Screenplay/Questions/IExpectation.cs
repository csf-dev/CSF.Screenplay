using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Questions
{
  public interface IExpectation
  {
    void Verify(IPerformer actor);
  }
}