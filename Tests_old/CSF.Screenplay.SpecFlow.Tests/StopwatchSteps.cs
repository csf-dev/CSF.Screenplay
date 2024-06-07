using System;
using System.Threading;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Stopwatch;
using NUnit.Framework;
using TechTalk.SpecFlow;
using static CSF.Screenplay.StepComposer;

namespace CSF.Screenplay.SpecFlow.Tests
{
  [Binding]
  public class StopwatchSteps
  {
    readonly ICast cast;
    readonly IStage stage;

    [Given(@"Joe has a stopwatch")]
    public void GivenJoeHasAStopwatch()
    {
      var joe = cast.Get("Joe");
      joe.IsAbleTo<UseAStopwatch>();
      stage.ShineTheSpotlightOn(joe);
    }

    [Given(@"he has started timing")]
    public void GivenHeHasStartedTiming()
    {
      var actor = stage.GetTheActorInTheSpotlight();
      Given(actor).WasAbleTo<StartTheStopwatch>();
    }

    [When(@"he waits for (.*) milliseconds")]
    public void WhenHeWaitsForMilliseconds(int milliseconds)
    {
      Thread.Sleep(milliseconds);
    }

    [When(@"he stops the stopwatch")]
    public void WhenHeReadsTheStopwatch()
    {
      var actor = stage.GetTheActorInTheSpotlight();
      When(actor).AttemptsTo<StopTheStopwatch>();
    }

    [Then(@"the stopwatch should read at least (.*) milliseconds")]
    public void ThenTheStopwatchShouldReadAtLeastMilliseconds(int milliseconds)
    {
      var actor = stage.GetTheActorInTheSpotlight();
      var time = Then(actor).ShouldSee(Read.TheStopwatch());

      Assert.That(time.TotalMilliseconds, Is.GreaterThanOrEqualTo(milliseconds));
    }

    public StopwatchSteps(ICast cast, IStage stage)
    {
      if(stage == null)
        throw new ArgumentNullException(nameof(stage));
      if(cast == null)
        throw new ArgumentNullException(nameof(cast));

      this.cast = cast;
      this.stage = stage;
    }
  }
}
