using System;
using CSF.Screenplay;

using static CSF.Screenplay.StepComposer;

namespace CSF.Screenplay.Example
{
  public class SampleTest
  {
    readonly Actor joe;
    readonly DoAThingTask doAThing;
    readonly DoADifferentThingTask doADifferentThing;
    readonly SeeSomethingHappenExpectation seeSomethingHappen;
    readonly LightsQuestion thereAreLights;

    public void JoeShouldSeeSomethingHappenWhenHeDoesADifferentThing()
    {
      Given(joe).WasAbleTo(doAThing);
      When(joe).AttemptsTo(doADifferentThing);
      Then(joe).Should(SeeThat(thereAreLights).WhichSay("Bright"));
    }
  }
}
