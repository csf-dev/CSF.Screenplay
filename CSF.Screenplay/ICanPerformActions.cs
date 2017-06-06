using System;
using System.Collections.Generic;

namespace CSF.Screenplay
{
  public interface ICanPerformActions
  {
    IEnumerable<Type> GetAllActionTypes();

    TAction GetAction<TAction>() where TAction : class;
  }
}