using CalculatorApp;

namespace ReqnrollPlugins.DependencyInjection.StepDefinitions;

[Binding]
public sealed class CalculatorStepDefinitions
{
    private readonly ICalculator _calculator;
    private readonly IReqnrollOutputHelper _outputHelper;

    public CalculatorStepDefinitions(ICalculator calculator, IReqnrollOutputHelper outputHelper)
    {
        _calculator = calculator;
        _outputHelper = outputHelper;
        Assert.IsFalse(_calculator.HasResult);
    }

    [Given("the first number is {int}")]
    public void GivenTheFirstNumberIs(int number)
    {
        _calculator.Reset();
        _calculator.Enter(number);
    }

    [Given("the second number is {int}")]
    public void GivenTheSecondNumberIs(int number)
    {
        _calculator.Enter(number);
    }

    [When("the two numbers are added")]
    public void WhenTheTwoNumbersAreAdded()
    {
        _outputHelper.WriteLine("Add invoked");
        _calculator.Add();
    }

    [When("the two numbers are multiplied")]
    public void WhenTheTwoNumbersAreMultiplied()
    {
        _outputHelper.WriteLine("Multiply invoked");
        _calculator.Multiply();
    }

    [Then("the result should be {int}")]
    public void ThenTheResultShouldBe(int result)
    {
        Assert.AreEqual(result, _calculator.GetResult());
    }
}
