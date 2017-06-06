namespace CSF.Screenplay
{
  public interface IActor : IGivenActor, IWhenActor, IThenActor {}

  public interface IGivenActor
  {
    void WasAbleTo(ITask task);
  }

  public interface IWhenActor
  {
    void AttemptsTo(ITask task);
  }

  public interface IThenActor
  {
    void Should(ITask task);

    void Should(IExpectation expectation);
  }
}
