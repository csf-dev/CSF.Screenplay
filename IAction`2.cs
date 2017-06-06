public interface IAction<TParams,TResult>
{
  TResult Perform(TParams parameters);
}