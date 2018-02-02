using System;
using static CSF.Screenplay.StepComposer;

using TechTalk.SpecFlow;
using CSF.Screenplay.SpecFlow.Tests.Actions;
using NUnit.Framework;

namespace CSF.Screenplay.SpecFlow.Tests
{
  [Binding]
  public class TestSteps
  {
    readonly IScenario ctx;

    [Given(@"([^ ]+) has the number (-?\d+)")]
    public void GivenJoeStartsWithTheNumber(string actorName, int number)
    {
      var joe = ctx.GetMathsWhiz(actorName);

      Given(joe).WasAbleTo(StartWith.TheNumber(number));
    }

    [Given(@"([^ ]+) adds (-?\d+)")]
    public void GivenJoeAdded(string actorName, int number)
    {
      var joe = ctx.GetMathsWhiz(actorName);

      Given(joe).WasAbleTo(Add.TheNumber(number));
    }

    [When(@"([^ ]+) adds (-?\d+)")]
    public void WhenJoeAdds(string actorName, int number)
    {
      var joe = ctx.GetMathsWhiz(actorName);

      When(joe).AttemptsTo(Add.TheNumber(number));
    }

    [Then(@"([^ ]+) should see the total (-?\d+)")]
    public void ThenJoeShouldSeeTheResult(string actorName, int number)
    {
      var joe = ctx.GetMathsWhiz(actorName);

      var result = Then(joe).ShouldSee(Get.TheNumber());
      Assert.That(result, Is.EqualTo(number));
    }

    public TestSteps(IScenario context)
    {
      if(context == null)
        throw new ArgumentNullException(nameof(context));

      ctx = context;
    }
  }
}
