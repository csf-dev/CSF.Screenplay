using System;
using static CSF.Screenplay.StepComposer;

// This type is decorated with comments showing what it might look like using NUnit

namespace CSF.Screenplay.SampleTests
{
  // [TestFixture]
  public class SampleTest
  {
    Actor joe = new Actor("joe");

    // [Setup]
    // public void Setup()
    // {
    //   joe = new Actor("joe");
    //   joe.IsAbleTo<SampleAbility>();
    // }

    // [Test]
    public void JoeShouldSeeLightsWhenHeDoesADifferentThing()
    {
      var doAThing = new DoAThing();
      var doADifferentThing = new DoADifferentThing();
      var thatThereAreLights = new ThereAreLights();

      joe.IsAbleTo<SampleAbility>();

      Given(joe).WasAbleTo(doAThing);
      When(joe).AttemptsTo(doADifferentThing);
      var result = Then(joe).ShouldSee(thatThereAreLights);

      // In the real world, you'd use the assertion mechanism built into your test framework of choice
      // Assert.AreEqual("Bright", result);
      if(result != "Bright") throw new Exception("Assertion failure");
    }
  }
}
