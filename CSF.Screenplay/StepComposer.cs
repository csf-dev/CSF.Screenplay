using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Questions;
using CSF.Screenplay.Tasks;

namespace CSF.Screenplay
{
  public static class StepComposer
  {
    public static IGivenActor Given(Actor actor)
    {
      return actor;
    }

    public static IWhenActor When(Actor actor)
    {
      return actor;
    }

    public static IThenActor Then(Actor actor)
    {
      return actor;
    }

    public static TTask PerformTask<TTask>() where TTask : ITask,new()
    {
      return new TTask();
    }

    public static TTask PerformTaskAndGetResult<TTask>() where TTask : ITaskWithResult,new()
    {
      return new TTask();
    }

    public static IExpectationComposer<TAnswer> SeeThat<TAnswer>(IQuestion<TAnswer> question)
    {
      return new ExpectationComposer<TAnswer>(question);
    }

    public static IExpectationComposer<TAnswer> SeeThat<TQuestion,TAnswer>() where TQuestion : IQuestion,new()
    {
      return new ExpectationComposer<TAnswer>((IQuestion<TAnswer>) new TQuestion());
    }
  }
}
