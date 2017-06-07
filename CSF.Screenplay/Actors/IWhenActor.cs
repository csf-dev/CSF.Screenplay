using System;
using CSF.Screenplay.Tasks;

namespace CSF.Screenplay.Actors
{
  public interface IWhenActor
  {
    void AttemptsTo(ITask task);

    TResult AttemptsTo<TResult>(ITask<TResult> task);
  }
}
