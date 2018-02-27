using System;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace CSF.Screenplay.SpecFlow.Tests
{
  [Binding]
  public class ResolvingATestRunnerSteps
  {
    readonly Lazy<ITestRunner> testRunner;
    readonly Lazy<BindingWhichDerivesFromSteps> stepsBinding;
    readonly ScenarioContext context;

    [Given("I have an instance of a SpecFlow binding class which includes a Lazy ITestRunner")]
    public void GivenIHaveASpecFlowBindingWhichIncludesALazyITestRunner()
    {
      // Intentional no-op, this very step instantiates the class
    }

    [Given(@"I have an instance of a SpecFlow binding class which derives from the Steps class")]
    public void GivenAnInstanceOfBindingWhichDerivesFromSteps()
    {
#pragma warning disable RECS0156 // Local variable is never used
      var val = stepsBinding.Value;
#pragma warning restore RECS0156 // Local variable is never used
    }

    [When(@"I resolve that test runner instance")]
    public void WhenIResolveTheTestRunner()
    {
#pragma warning disable RECS0156 // Local variable is never used
      var val = testRunner.Value;
#pragma warning restore RECS0156 // Local variable is never used
    }

    [When(@"I make use of a sample method from that binding class which returns the string '([^']+)'")]
    public void WhenIMakeUseOfAMethodFromTheBindingClass(string parameter)
    {
      var result = stepsBinding.Value.SampleMethod(parameter);
      context.Set(result, nameof(BindingWhichDerivesFromSteps.SampleMethod));
    }

    [Then(@"the test runner instance should not be null")]
    public void ThenTheTestRunnerShouldNotBeNull()
    {
      Assert.That(testRunner.Value, Is.Not.Null);
    }

    [Then(@"the returned value should be '([^']+)'")]
    public void ThenTheSampleMethodValueShouldBe(string expected)
    {
      var result = context.Get<string>(nameof(BindingWhichDerivesFromSteps.SampleMethod));
      Assert.That(result, Is.EqualTo(expected));
    }

    [Then(@"if nothing crashed then the test passes")]
    public void ThenPassTheTest()
    {
      Assert.Pass("The test passed");
    }

    public ResolvingATestRunnerSteps(Lazy<ITestRunner> testRunner,
                                     Lazy<BindingWhichDerivesFromSteps> stepsBinding,
                                     ScenarioContext context)
    {
      if(context == null)
        throw new ArgumentNullException(nameof(context));
      if(stepsBinding == null)
        throw new ArgumentNullException(nameof(stepsBinding));
      if(testRunner == null)
        throw new ArgumentNullException(nameof(testRunner));

      this.context = context;
      this.stepsBinding = stepsBinding;
      this.testRunner = testRunner;
    }
  }
}
