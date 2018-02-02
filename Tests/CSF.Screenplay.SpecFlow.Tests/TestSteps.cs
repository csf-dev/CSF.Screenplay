using System;
using static CSF.Screenplay.StepComposer;

using TechTalk.SpecFlow;
using CSF.Screenplay.SpecFlow.Tests.Actions;
using NUnit.Framework;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.SpecFlow.Tests
{
  [Binding]
  public class TestSteps
  {
    readonly ICast cast;

    [Given(@"([^ ]+) has the number (-?\d+)")]
    public void GivenJoeStartsWithTheNumber(string actorName, int number)
    {
      var joe = cast.GetMathsWhiz(actorName);

      Given(joe).WasAbleTo(StartWith.TheNumber(number));
    }

    [Given(@"([^ ]+) adds (-?\d+)")]
    public void GivenJoeAdded(string actorName, int number)
    {
      var joe = cast.GetMathsWhiz(actorName);

      Given(joe).WasAbleTo(Add.TheNumber(number));
    }

    [When(@"([^ ]+) adds (-?\d+)")]
    public void WhenJoeAdds(string actorName, int number)
    {
      var joe = cast.GetMathsWhiz(actorName);

      When(joe).AttemptsTo(Add.TheNumber(number));
    }

    [Then(@"([^ ]+) should see the total (-?\d+)")]
    public void ThenJoeShouldSeeTheResult(string actorName, int number)
    {
      var joe = cast.GetMathsWhiz(actorName);

      var result = Then(joe).ShouldSee(Get.TheNumber());
      Assert.That(result, Is.EqualTo(number));
    }

    public TestSteps(ICast cast)
    {
      if(cast == null)
        throw new ArgumentNullException(nameof(cast));

      this.cast = cast;
    }
  }
}
