namespace CSF.Screenplay
{
  public interface IExpectation
  {
    void Verify(ICanPerformActions actor);
  }
}