public abstract class Actor : IActor
{
  void IActor.WasAbleTo(ITask task)
  {
    Perform(task);
  }

  void IActor.AttemptsTo(ITask task)
  {
    Perform(task);
  }

  void IActor.Should(ITask task)
  {
    Perform(task);
  }

  void IActor Should(IExpectation expectation)
  {
    Verify(expectation);
  }

  protected abstract ICanPerformActions GetActionProvider();

  protected virtual void Perform(ITask task)
  {
    var provider = GetActionProvider();
    task.Execute(provider);
  }
  
  protected virtual void Verify(IExpectation expectation)
  {
    var provider = GetActionProvider();
    expectation.Verify(provider);
  }
}