using System;
using CSF.Screenplay.Questions;
using CSF.Screenplay.Tasks;

namespace CSF.Screenplay.Actors
{
  public interface IThenActor
  {
    void Should(ITask task);

    TResult Should<TResult>(ITask<TResult> task);

    void Should(IExpectation expectation);
  }
}
