using System;
using CSF.Screenplay.Tasks;

namespace CSF.Screenplay.Actors
{
  public interface IGivenActor
  {
    void WasAbleTo(ITask task);

    TResult WasAbleTo<TResult>(ITask<TResult> task);
  }
}
