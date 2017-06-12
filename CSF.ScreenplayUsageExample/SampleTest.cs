using System;
using static CSF.Screenplay.StepComposer;

namespace CSF.Screenplay.Example
{
  public class SampleTest
  {
    readonly Actor joe;

    public void JoeShouldSeeLightsWhenHeDoesADifferentThing()
    {
      var doAThing = new DoAThing();
      var doADifferentThing = new DoADifferentThing();
      var thatThereAreLights = new ThereAreLights();

      Given(joe).WasAbleTo(doAThing);
      When(joe).AttemptsTo(doADifferentThing);

      var result = Then(joe).ShouldSee(thatThereAreLights);
      // Perform assertion on result
    }
  }
}
