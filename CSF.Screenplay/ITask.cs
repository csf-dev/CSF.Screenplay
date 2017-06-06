namespace CSF.Screenplay
{
  public interface ITask
  {
    void Execute(ICanPerformActions actor);
  }
}