public interface IExpectation
{
  void Verify(ICanPerformActions actor);
}