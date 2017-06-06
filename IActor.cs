public interface IActor
{
  void WasAbleTo(ITask task);

  void AttemptsTo(ITask task);

  void Should(ITask task);

  void Should(IExpectation expectation);
}