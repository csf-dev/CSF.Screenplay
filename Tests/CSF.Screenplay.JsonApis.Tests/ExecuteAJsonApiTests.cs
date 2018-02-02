using System;
using CSF.Screenplay.JsonApis.Builders;
using CSF.Screenplay.JsonApis.Tests.Builders;
using CSF.Screenplay.JsonApis.Tests.Services;
using CSF.Screenplay.NUnit;
using CSF.Screenplay.WebTestWebsite.ApiControllers;
using CSF.Screenplay.WebTestWebsite.Models;
using NUnit.Framework;
using static CSF.Screenplay.StepComposer;

namespace CSF.Screenplay.JsonApis.Tests
{
  [TestFixture]
  public class ExecuteAJsonApiTests
  {
    [Test,Screenplay]
    public void Using_SetTheNumber_does_not_raise_exception(IScenario scenario)
    {
      // Arrange
      var joe = scenario.GetJoe();

      // Act & assert
      Assert.That(() => When(joe).AttemptsTo(Execute.AJsonApi(Set.TheNumberTo(5))), Throws.Nothing);
    }

    [Test,Screenplay]
    public void Using_SetTheNumber_then_GetTheNumber_returns_the_correct_number(IScenario scenario)
    {
      // Arrange
      var theNumber = 42;
      var joe = scenario.GetJoe();
      Given(joe).WasAbleTo(Execute.AJsonApi(Set.TheNumberTo(theNumber)));

      // Act
      var result = When(joe).AttemptsTo(Execute.AJsonApiAndGetTheResult(Get.TheNumber()));

      // Assert
      Assert.That(result, Is.EqualTo(theNumber));
    }

    [Test,Screenplay]
    public void Using_CheckData_does_not_raise_exception_for_valid_data(IScenario scenario)
    {
      // Arrange
      var joe = scenario.GetJoe();
      var theData = new SampleApiData { Name = ExecutionController.ValidName, DateAndTime = DateTime.Today };

      // Act & assert
      Assert.That(() => When(joe).AttemptsTo(Execute.AJsonApi(Validate.TheData(theData))), Throws.Nothing);
    }

    [Test,Screenplay]
    public void Using_CheckData_raises_exception_for_invalid_data(IScenario scenario)
    {
      // Arrange
      var joe = scenario.GetJoe();
      var theData = new SampleApiData { Name = "Invalid, crash expected", DateAndTime = DateTime.Today };

      // Act & assert
      Assert.That(() => When(joe).AttemptsTo(Execute.AJsonApi(Validate.TheData(theData))),
                  Throws.InstanceOf<JsonApiException>());
    }

    [Test,Screenplay]
    public void Using_GetData_returns_expected_result(IScenario scenario)
    {
      // Arrange
      var joe = scenario.GetJoe();

      // Act
      var result = When(joe).AttemptsTo(Execute.AJsonApiAndGetTheResult(Get.TheSampleDataFor(joe.Name)));

      // Assert
      Assert.That(result, Is.Not.Null, "Result should not be null");
      Assert.That(result.Name, Is.EqualTo(joe.Name), "Result name should be as expected");
      Assert.That(result.DateAndTime, Is.EqualTo(DataController.SampleDateTime), "Result date should be as expected");
    }

    [Test,Screenplay]
    public void Using_GetDataSlowly_does_not_raise_exception_if_timeout_is_30_seconds(IScenario scenario)
    {
      // Arrange
      var joe = scenario.GetJoe();
      var timeout = TimeSpan.FromSeconds(30);

      // Act & assert
      Assert.That(() => When(joe).AttemptsTo(Execute.AJsonApiAndGetTheResult(Get.TheSampleDataSlowlyFor(joe.Name, timeout))),
                  Throws.Nothing);
    }

    [Test,Screenplay]
    public void Using_GetDataSlowly_raises_exception_if_timeout_is_1_second(IScenario scenario)
    {
      // Arrange
      var joe = scenario.GetJoe();
      var timeout = TimeSpan.FromSeconds(1);

      // Act & assert
      Assert.That(() => When(joe).AttemptsTo(Execute.AJsonApiAndGetTheResult(Get.TheSampleDataSlowlyFor(joe.Name, timeout))),
                  Throws.InstanceOf<TimeoutException>());
    }
  }
}
