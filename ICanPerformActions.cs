public interface ICanPerformActions
{
  IEnumerable<Type> GetAllActionTypes();

  TAction GetAction<TAction>();
}