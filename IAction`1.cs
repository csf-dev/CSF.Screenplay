public interface IAction<TParams>
{
  void Perform(TParams parameters);
}