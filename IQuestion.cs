public interface IQuestion
{
  object GetAnswer();
}

public interface IQuestion<TAnswer> : IQuestion
{
  TAnswer GetAnswer();
}