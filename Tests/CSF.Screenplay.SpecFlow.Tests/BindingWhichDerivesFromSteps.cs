using System;

using TechTalk.SpecFlow;

namespace CSF.Screenplay.SpecFlow.Tests
{
  [Binding]
  public class BindingWhichDerivesFromSteps : Steps
  {
    public string SampleMethod(string param) => param;

    [When(@"I execute the first part of a composite binding")]
    public void WhenIExecuteTheFirstPartOfACompositeBinding()
    {
      // Intentional no-op
    }

    [When(@"I execute the second part of a composite binding")]
    public void WhenIExecuteTheSecondPartOfACompositeBinding()
    {
      // Intentional no-op
    }

    [When(@"I execute a composite binding from the steps binding")]
    public void WhenIExecuteACompositeBinding()
    {
      When(@"I execute the first part of a composite binding");
      When(@"I execute the second part of a composite binding");
    }
  }
}
