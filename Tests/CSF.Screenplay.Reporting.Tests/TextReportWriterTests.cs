using System;
using NUnit.Framework;
using CSF.Screenplay.Reporting;
using System.Text;
using System.IO;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using Moq;
using CSF.Screenplay.Reporting.Models;

namespace CSF.Screenplay.Reporting.Tests
{
  [TestFixture]
  public class TextReportWriterTests
  {
    string ExerciseSut(Report report)
    {
      var builder = new StringBuilder();

      using(var writer = new StringWriter(builder))
      {
        var sut = new TextReportWriter(writer);
        sut.Write(report);
      }

      return builder.ToString();
    }

    [Test,AutoMoqData]
    public void Can_create_report_with_one_performance(string id,
                                                       string name,
                                                       string feature,
                                                       IActor actor,
                                                       IPerformable performable)
    {
      // Arrange
      var expected = $@"
Feature:  {feature}
Scenario: {name}
**** Success ****
Given Joe does a thing
";

      Mock.Get(performable)
          .Setup(x => x.GetReport(actor))
          .Returns("Joe does a thing");

      var scenario = new Models.Scenario(id, name, feature);
      scenario.Reportables.Add(new Performance(actor, Outcome.Success, performable, PerformanceType.Given));
      var report = new Report(new [] { scenario });

      // Act
      var result = ExerciseSut(report);

      // Assert
      Assert.That(result, Is.EqualTo(expected));
    }

    [Test,AutoMoqData]
    public void Can_create_report_with_scenario_failure(IActor actor,
                                                        IPerformable performable,
                                                        string id,
                                                        string name,
                                                        string feature)
    {
      // Arrange
      var expected = $@"
Feature:  {feature}
Scenario: {name}
**** Failure ****
Given Joe does a thing
";

      Mock.Get(performable)
          .Setup(x => x.GetReport(actor))
          .Returns("Joe does a thing");

      var scenario = new Models.Scenario(id, name, feature) { Outcome = false };
      scenario.Reportables.Add(new Performance(actor, Outcome.Success, performable, PerformanceType.Given));
      var report = new Report(new [] { scenario });

      // Act
      var result = ExerciseSut(report);

      // Assert
      Assert.That(result, Is.EqualTo(expected));
    }

    [Test,AutoMoqData]
    public void Reported_exceptions_should_not_be_duplicated_up_the_reporting_chain(IActor actor,
                                                                                    IPerformable performable,
                                                                                    IPerformable childPerformable,
                                                                                    string id,
                                                                                    string name,
                                                                                    string feature,
                                                                                    Exception error)
    {
      // Arrange
      var expected = $@"
Feature:  {feature}
Scenario: {name}
**** Success ****
Given Joe does a thing
          Joe does a different thing
          FAILED with an exception:
{error.ToString()}
";

      Mock.Get(performable).Setup(x => x.GetReport(actor)).Returns("Joe does a thing");
      Mock.Get(childPerformable).Setup(x => x.GetReport(actor)).Returns("Joe does a different thing");

      var scenario = new Models.Scenario(id, name, feature);
      var parentPerformance = new Performance(actor,
                                              Outcome.FailureWithException,
                                              performable,
                                              PerformanceType.Given,
                                              exception: error);
      var childPerformance = new Performance(actor,
                                             Outcome.FailureWithException,
                                             childPerformable,
                                             PerformanceType.Given,
                                             exception: error);
      parentPerformance.Reportables.Add(childPerformance);
      scenario.Reportables.Add(parentPerformance);
      var report = new Report(new [] { scenario });

      // Act
      var result = ExerciseSut(report);

      // Assert
      Assert.That(result, Is.EqualTo(expected));
    }

    [Test,AutoMoqData]
    public void Feature_name_is_omitted_if_not_provided(IActor actor,
                                                        IPerformable performable,
                                                        string id,
                                                        string name)
    {
      // Arrange
      var expected = $@"
Scenario: {name}
**** Success ****
Given Joe does a thing
";

      Mock.Get(performable)
          .Setup(x => x.GetReport(actor))
          .Returns("Joe does a thing");

      var scenario = new Models.Scenario(id, name, null);
      scenario.Reportables.Add(new Performance(actor, Outcome.Success, performable, PerformanceType.Given));
      var report = new Report(new [] { scenario });

      // Act
      var result = ExerciseSut(report);

      // Assert
      Assert.That(result, Is.EqualTo(expected));
    }

    [Test,AutoMoqData]
    public void Scenario_id_is_used_if_name_if_omitted(IActor actor,
                                                       IPerformable performable,
                                                       string id,
                                                       string feature)
    {
      // Arrange
      var expected = $@"
Feature:  {feature}
Scenario: {id}
**** Success ****
Given Joe does a thing
";

      Mock.Get(performable)
          .Setup(x => x.GetReport(actor))
          .Returns("Joe does a thing");

      var scenario = new Models.Scenario(id, null, feature);
      scenario.Reportables.Add(new Performance(actor, Outcome.Success, performable, PerformanceType.Given));
      var report = new Report(new [] { scenario });

      // Act
      var result = ExerciseSut(report);

      // Assert
      Assert.That(result, Is.EqualTo(expected));
    }

    [Test,AutoMoqData]
    public void Can_create_report_with_two_nested_performances(IActor actor,
                                                               IPerformable performable,
                                                               IPerformable childPerformable,
                                                               string id,
                                                               string name,
                                                               string feature)
    {
      // Arrange
      var expected = $@"
Feature:  {feature}
Scenario: {name}
**** Success ****
Given Joe does a thing
          Joe does a different thing
";

      Mock.Get(performable).Setup(x => x.GetReport(actor)).Returns("Joe does a thing");
      Mock.Get(childPerformable).Setup(x => x.GetReport(actor)).Returns("Joe does a different thing");

      var scenario = new Models.Scenario(id, name, feature);
      var parentPerformance = new Performance(actor, Outcome.Success, performable, PerformanceType.Given);
      parentPerformance.Reportables.Add(new Performance(actor, Outcome.Success, childPerformable, PerformanceType.Given));
      scenario.Reportables.Add(parentPerformance);
      var report = new Report(new [] { scenario });

      // Act
      var result = ExerciseSut(report);

      // Assert
      Assert.That(result, Is.EqualTo(expected));
    }

    [Test,AutoMoqData]
    public void Can_create_report_with_three_nested_performances(IActor actor,
                                                                 IPerformable performable,
                                                                 IPerformable childPerformable,
                                                                 IPerformable grandChildPerformable,
                                                                 string id,
                                                                 string name,
                                                                 string feature)
    {
      // Arrange
      var expected = $@"
Feature:  {feature}
Scenario: {name}
**** Success ****
Given Joe does a thing
          Joe does a different thing
              Joe does a totally different thing
";

      Mock.Get(performable).Setup(x => x.GetReport(actor)).Returns("Joe does a thing");
      Mock.Get(childPerformable).Setup(x => x.GetReport(actor)).Returns("Joe does a different thing");
      Mock.Get(grandChildPerformable).Setup(x => x.GetReport(actor)).Returns("Joe does a totally different thing");

      var scenario = new Models.Scenario(id, name, feature);
      var parentPerformance = new Performance(actor, Outcome.Success, performable, PerformanceType.Given);
      var childPerformance = new Performance(actor, Outcome.Success, childPerformable, PerformanceType.Given);
      var grandchildPerformance = new Performance(actor, Outcome.Success, grandChildPerformable, PerformanceType.Given);
      parentPerformance.Reportables.Add(childPerformance);
      childPerformance.Reportables.Add(grandchildPerformance);
      scenario.Reportables.Add(parentPerformance);
      var report = new Report(new [] { scenario });

      // Act
      var result = ExerciseSut(report);

      // Assert
      Assert.That(result, Is.EqualTo(expected));
    }

    [Test,AutoMoqData]
    public void Can_create_report_with_complex_nested_performances(IActor actor,
                                                                   IPerformable performable,
                                                                   IPerformable childPerformable,
                                                                   IPerformable grandChildPerformable,
                                                                   IPerformable siblingPerformable,
                                                                   IPerformable secondPerformable,
                                                                   string id,
                                                                   string name,
                                                                   string feature)
    {
      // Arrange
      var expected = $@"
Feature:  {feature}
Scenario: {name}
**** Success ****
Given Joe does a thing
          Joe does a different thing
              Joe does a totally different thing
          Joe does an unrelated thing
 When Joe takes some kind of action
";

      Mock.Get(performable).Setup(x => x.GetReport(actor)).Returns("Joe does a thing");
      Mock.Get(childPerformable).Setup(x => x.GetReport(actor)).Returns("Joe does a different thing");
      Mock.Get(grandChildPerformable).Setup(x => x.GetReport(actor)).Returns("Joe does a totally different thing");
      Mock.Get(siblingPerformable).Setup(x => x.GetReport(actor)).Returns("Joe does an unrelated thing");
      Mock.Get(secondPerformable).Setup(x => x.GetReport(actor)).Returns("Joe takes some kind of action");

      var scenario = new Models.Scenario(id, name, feature);
      var parentPerformance = new Performance(actor, Outcome.Success, performable, PerformanceType.Given);
      var childPerformance = new Performance(actor, Outcome.Success, childPerformable, PerformanceType.Given);
      var grandchildPerformance = new Performance(actor, Outcome.Success, grandChildPerformable, PerformanceType.Given);
      var siblingPerformance = new Performance(actor, Outcome.Success, siblingPerformable, PerformanceType.Given);
      var secondPerformance = new Performance(actor, Outcome.Success, secondPerformable, PerformanceType.When);
      parentPerformance.Reportables.Add(childPerformance);
      parentPerformance.Reportables.Add(siblingPerformance);
      childPerformance.Reportables.Add(grandchildPerformance);
      scenario.Reportables.Add(parentPerformance);
      scenario.Reportables.Add(secondPerformance);
      var report = new Report(new [] { scenario });

      // Act
      var result = ExerciseSut(report);

      // Assert
      Assert.That(result, Is.EqualTo(expected));
    }
  }
}
