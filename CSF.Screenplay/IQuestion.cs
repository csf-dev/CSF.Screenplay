namespace CSF.Screenplay
{
  public interface IQuestion
  {
    object GetAnswer(ICanPerformActions actor);
  }
}