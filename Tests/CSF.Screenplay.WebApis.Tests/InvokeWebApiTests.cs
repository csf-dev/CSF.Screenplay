using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.NUnit;
using CSF.Screenplay.WebApis.Builders;
using CSF.Screenplay.WebApis.Tests.Endpoints;
using CSF.Screenplay.WebTestWebsite.ApiControllers;
using CSF.Screenplay.WebTestWebsite.Models;
using NUnit.Framework;
using static CSF.Screenplay.StepComposer;

namespace CSF.Screenplay.WebApis.Tests
{
  [TestFixture]
  public class InvokeWebApiTests
  {
    [Test,Screenplay]
    public void Using_SetTheNumber_does_not_raise_exception(ICast cast)
    {
      // Arrange
      var joe = cast.Get<Joe>();
      var theData = new SampleApiData() { NewNumber = 5 };

      // Act & assert
      Assert.That(() => When(joe).AttemptsTo(Invoke.TheJsonWebService<SetNumber>().WithTheData(theData).AndVerifyItSucceeds()), Throws.Nothing);
    }

    [Test,Screenplay]
    public void Using_SetTheNumber_then_GetTheNumber_returns_the_correct_number(ICast cast)
    {
      // Arrange
      var theNumber = 42;
      var theData = new SampleApiData() { NewNumber = theNumber };
      var joe = cast.Get<Joe>();
      Given(joe).WasAbleTo(Invoke.TheJsonWebService<SetNumber>().WithTheData(theData).AndVerifyItSucceeds());

      // Act
      var result = When(joe).AttemptsTo(Invoke.TheJsonWebService<GetNumber>().AndReadTheResultAs<int>());

      // Assert
      Assert.That(result, Is.EqualTo(theNumber));
    }

    [Test,Screenplay]
    public void Using_CheckData_does_not_raise_exception_for_valid_data(ICast cast)
    {
      // Arrange
      var joe = cast.Get<Joe>();
      var theData = new SampleApiData { Name = ExecutionController.ValidName, DateAndTime = DateTime.Today };

      // Act & assert
      Assert.That(() => When(joe).AttemptsTo(Invoke.TheJsonWebService<CheckData>().WithTheData(theData).AndVerifyItSucceeds()), Throws.Nothing);
    }

    [Test,Screenplay]
    public void Using_CheckData_raises_exception_for_invalid_data(ICast cast)
    {
      // Arrange
      var joe = cast.Get<Joe>();
      var theData = new SampleApiData { Name = "Invalid, crash expected", DateAndTime = DateTime.Today };

      // Act & assert
      Assert.That(() => When(joe).AttemptsTo(Invoke.TheJsonWebService<CheckData>().WithTheData(theData).AndVerifyItSucceeds()),
                  Throws.InstanceOf<WebApiException>());
    }

    [Test,Screenplay]
    public void Using_GetData_returns_expected_result(ICast cast)
    {
      // Arrange
      var joe = cast.Get<Joe>();

      // Act
      var result = When(joe).AttemptsTo(Invoke.TheJsonWebService(GetData.For(joe.Name)).AndReadTheResultAs<SampleApiData>());

      // Assert
      Assert.That(result, Is.Not.Null, "Result should not be null");
      Assert.That(result.Name, Is.EqualTo(joe.Name), "Result name should be as expected");
      Assert.That(result.DateAndTime, Is.EqualTo(DataController.SampleDateTime), "Result date should be as expected");
    }

    [Test,Screenplay]
    public void Using_GetDataSlowly_does_not_raise_exception_if_timeout_is_30_seconds(ICast cast)
    {
      // Arrange
      var joe = cast.Get<Joe>();

      // Act & assert
      Assert.That(() => When(joe).AttemptsTo(Invoke.TheJsonWebService(SlowlyGetData.For(joe.Name)).WithATimeoutOf(30).Seconds().AndReadTheResultAs<SampleApiData>()),
                  Throws.Nothing);
    }

    [Test,Screenplay]
    public void Using_GetDataSlowly_raises_exception_if_timeout_is_1_second(ICast cast)
    {
      // Arrange
      var joe = cast.Get<Joe>();

      // Act & assert
      Assert.That(() => When(joe).AttemptsTo(Invoke.TheJsonWebService(SlowlyGetData.For(joe.Name)).WithATimeoutOf(1).Seconds().AndReadTheResultAs<SampleApiData>()),
                  Throws.InstanceOf<TimeoutException>());
    }
  }
}
