using System;
namespace CSF.Screenplay
{
  public interface IQuestion<TAnswer> : IQuestion
  {
    new TAnswer GetAnswer(ICanPerformActions actor);
  }
}
