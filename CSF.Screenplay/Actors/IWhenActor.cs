using System;
using CSF.Screenplay.Questions;
using CSF.Screenplay.Tasks;

namespace CSF.Screenplay.Actors
{
  public interface IWhenActor
  {
    void AttemptsTo(ITask task);

    TResult AttemptsTo<TResult>(ITask<TResult> task);

    object Sees(IQuestion question);

    TResult Sees<TResult>(IQuestion<TResult> question);
  }
}
