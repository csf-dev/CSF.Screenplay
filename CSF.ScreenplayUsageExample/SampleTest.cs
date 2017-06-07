using System;
using static CSF.Screenplay.StepComposer;
using static CSF.Screenplay.AnswerMatcherComposer;

namespace CSF.Screenplay.Example
{
  public class SampleTest
  {
    readonly Actor joe;

    public void JoeShouldSeeSomethingHappenWhenHeDoesADifferentThing()
    {
      Given(joe).WasAbleTo(PerformTask<DoAThing>());
      When(joe).AttemptsTo(PerformTask<DoADifferentThing>());
      Then(joe).Should(SeeThat<ThereAreLights,string>().Which(Are("Bright")));
    }
  }
}
