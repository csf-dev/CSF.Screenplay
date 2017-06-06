namespace CSF.Screenplay
{
  public interface IQuestion
  {
    object GetAnswer(ICanPerformActions actor);
  }

  public interface IQuestion<TAnswer> : IQuestion
  {
    TAnswer GetAnswer(ICanPerformActions actor);
  }
}