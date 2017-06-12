using System;
using CSF.Screenplay.Questions;
using CSF.Screenplay.Tasks;

namespace CSF.Screenplay.Actors
{
  public interface IGivenActor
  {
    void WasAbleTo(ITask task);

    TResult WasAbleTo<TResult>(ITask<TResult> task);

    object Saw(IQuestion question);

    TResult Saw<TResult>(IQuestion<TResult> question);
  }
}
