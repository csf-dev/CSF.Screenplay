using System;
using static CSF.Screenplay.StepComposer;
using TechTalk.SpecFlow;
using CSF.Screenplay.SpecFlow.Tests.Actions;
using NUnit.Framework;
using CSF.Screenplay.SpecFlow.Tests.Personas;

namespace CSF.Screenplay.SpecFlow.Tests
{
  [Binding]
  public class TestSteps
  {
    readonly IStage stage;

    [Given(@"Mathias has the number (-?\d+)")]
    public void GivenJoeStartsWithTheNumber(int number)
    {
      var mathias = stage.ShineTheSpotlightOn<Mathias>();

      Given(mathias).WasAbleTo(StartWith.TheNumber(number));
    }

    [Given(@"Sarah is an actor created from a persona")]
    public void GivenSarahIsAnActorFromAPersona()
    {
      var sarah = stage.ShineTheSpotlightOn<Sarah>();
    }

    [Given(@"(?:he|she|they) (?:has|have) the number (-?\d+)")]
    public void GivenTheyStartWithANumber(int number)
    {
      var actor = stage.GetTheActorInTheSpotlight();

      Given(actor).WasAbleTo(StartWith.TheNumber(number));
    }

    [Given(@"(?:he|she|they) adds? (-?\d+)")]
    public void GivenTheyAdd(int number)
    {
      var joe = stage.GetTheActorInTheSpotlight();

      Given(joe).WasAbleTo(Add.TheNumber(number));
    }

    [When(@"(?:he|she|they) adds? (-?\d+)")]
    public void WhenTheyAdd(int number)
    {
      var joe = stage.GetTheActorInTheSpotlight();

      When(joe).AttemptsTo(Add.TheNumber(number));
    }

    [Then(@"(?:he|she|they) should see the total (-?\d+)")]
    public void ThenTheyShouldSeeTheResult(int number)
    {
      var joe = stage.GetTheActorInTheSpotlight();

      var result = Then(joe).ShouldSee(Get.TheNumber());
      Assert.That(result, Is.EqualTo(number));
    }

    public TestSteps(IStage stage)
    {
      if(stage == null)
        throw new ArgumentNullException(nameof(stage));

      this.stage = stage;
    }
  }
}
