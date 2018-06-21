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
    public void Can_create_report_with_one_performance(Scenario scenario, Reportable reportable)
    {
      // Arrange
      var expected = @"
Feature:  Feature name
Scenario: Scenario name
**** Success ****
Given Joe does a thing
";

      scenario.Name.Name = "Scenario name";
      scenario.Outcome = true;
      scenario.Feature.Name.Name = "Feature name";

      reportable.Category = ReportableCategory.Given;
      reportable.Report = "Joe does a thing";
      reportable.Type = ReportableType.Success;

      scenario.Reportables.Clear();
      scenario.Reportables.Add(reportable);

      var report = new Report();
      report.Scenarios.Add(scenario);

      // Act
      var result = ExerciseSut(report);

      // Assert
      Assert.That(result, Is.EqualTo(expected));
    }

    [Test,AutoMoqData]
    public void Can_create_report_with_scenario_failure(Scenario scenario, Reportable reportable)
    {
      // Arrange
      var expected = @"
Feature:  Feature name
Scenario: Scenario name
**** Failure ****
Given Joe does a thing
";
      scenario.Name.Name = "Scenario name";
      scenario.Outcome = false;
      scenario.Feature.Name.Name = "Feature name";

      reportable.Category = ReportableCategory.Given;
      reportable.Report = "Joe does a thing";
      reportable.Type = ReportableType.Success;

      scenario.Reportables.Clear();
      scenario.Reportables.Add(reportable);

      var report = new Report();
      report.Scenarios.Add(scenario);

      // Act
      var result = ExerciseSut(report);

      // Assert
      Assert.That(result, Is.EqualTo(expected));
    }

    [Test,AutoMoqData]
    public void Can_create_report_with_inconclusive_outcome(Scenario scenario, Reportable reportable)
    {
      // Arrange
      var expected = @"
Feature:  Feature name
Scenario: Scenario name
**** Inconclusive ****
Given Joe does a thing
";

      scenario.Name.Name = "Scenario name";
      scenario.Outcome = null;
      scenario.Feature.Name.Name = "Feature name";

      reportable.Category = ReportableCategory.Given;
      reportable.Report = "Joe does a thing";
      reportable.Type = ReportableType.Success;

      scenario.Reportables.Clear();
      scenario.Reportables.Add(reportable);

      var report = new Report();
      report.Scenarios.Add(scenario);

      // Act
      var result = ExerciseSut(report);

      // Assert
      Assert.That(result, Is.EqualTo(expected));
    }

    [Test,AutoMoqData]
    public void Reported_exceptions_should_not_be_duplicated_up_the_reporting_chain(Scenario scenario,
                                                                                    Reportable reportableOne,
                                                                                    Reportable reportableTwo)
    {
      // Arrange
      reportableOne.Category = ReportableCategory.Given;
      reportableOne.Type = ReportableType.FailureWithError;
      reportableOne.Report = "Joe does a thing";
      reportableOne.Error = "Error text";

      reportableTwo.Category = ReportableCategory.Given;
      reportableTwo.Type = ReportableType.FailureWithError;
      reportableTwo.Report = "Joe does a different thing";
      reportableTwo.Error = "Error text";

      reportableOne.Reportables.Add(reportableTwo);
      scenario.Reportables.Clear();
      scenario.Reportables.Add(reportableOne);

      var report = new Report();
      report.Scenarios.Add(scenario);

      // Act
      var result = ExerciseSut(report);

      // Assert
      Assert.That(result, Contains.Substring(@"Given Joe does a thing
          Joe does a different thing
          FAILED: Error text"));
    }

    [Test,AutoMoqData]
    public void Feature_name_is_omitted_if_not_provided(Scenario scenario, Reportable reportable)
    {
      // Arrange
      scenario.Name.Name = "Scenario name";
      scenario.Outcome = true;
      scenario.Feature.Name = new IdAndName();

      reportable.Category = ReportableCategory.Given;
      reportable.Report = "Joe does a thing";

      scenario.Reportables.Clear();
      scenario.Reportables.Add(reportable);

      var report = new Report();
      report.Scenarios.Add(scenario);

      // Act
      var result = ExerciseSut(report);

      // Assert
      Assert.That(result, Does.Not.Contain("Feature:"));
    }

    [Test,AutoMoqData]
    public void The_Scenario_id_is_used_instead_of_the_name_if_the_name_is_omitted(Scenario scenario,
                                                                                   Reportable reportable)
    {
      // Arrange
      scenario.Name.Name = null;
      scenario.Name.Id = "ScenarioId";
      scenario.Outcome = true;

      reportable.Category = ReportableCategory.Given;
      reportable.Report = "Joe does a thing";

      scenario.Reportables.Clear();
      scenario.Reportables.Add(reportable);

      var report = new Report();
      report.Scenarios.Add(scenario);

      // Act
      var result = ExerciseSut(report);

      // Assert
      Assert.That(result, Contains.Substring("Scenario: ScenarioId"));
    }

    [Test,AutoMoqData]
    public void Can_create_report_with_two_nested_performances(Scenario scenario,
                                                               Reportable reportableOne,
                                                               Reportable reportableTwo)
    {
      // Arrange
      reportableOne.Category = ReportableCategory.Given;
      reportableOne.Type = ReportableType.Success;
      reportableOne.Report = "Joe does a thing";
      reportableOne.Reportables.Clear();

      reportableTwo.Category = ReportableCategory.Given;
      reportableTwo.Type = ReportableType.Success;
      reportableTwo.Report = "Joe does a different thing";
      reportableTwo.Reportables.Clear();

      reportableOne.Reportables.Add(reportableTwo);
      scenario.Reportables.Clear();
      scenario.Reportables.Add(reportableOne);

      var report = new Report();
      report.Scenarios.Add(scenario);

      // Act
      var result = ExerciseSut(report);

      // Assert
      Assert.That(result, Contains.Substring(@"Given Joe does a thing
          Joe does a different thing"));
    }

    [Test,AutoMoqData]
    public void Can_create_report_with_three_nested_performances(Scenario scenario,
                                                                 Reportable reportableOne,
                                                                 Reportable reportableTwo,
                                                                 Reportable reportableThree)
    {
      // Arrange
      reportableOne.Category = ReportableCategory.Given;
      reportableOne.Type = ReportableType.Success;
      reportableOne.Report = "Joe does a thing";
      reportableOne.Reportables.Clear();

      reportableTwo.Category = ReportableCategory.Given;
      reportableTwo.Type = ReportableType.Success;
      reportableTwo.Report = "Joe does a different thing";
      reportableTwo.Reportables.Clear();

      reportableThree.Category = ReportableCategory.Given;
      reportableThree.Type = ReportableType.Success;
      reportableThree.Report = "Joe does a totally different thing";
      reportableThree.Reportables.Clear();

      reportableOne.Reportables.Add(reportableTwo);
      reportableTwo.Reportables.Add(reportableThree);
      scenario.Reportables.Clear();
      scenario.Reportables.Add(reportableOne);

      var report = new Report();
      report.Scenarios.Add(scenario);

      // Act
      var result = ExerciseSut(report);

      // Assert
      Assert.That(result, Contains.Substring(@"Given Joe does a thing
          Joe does a different thing
              Joe does a totally different thing"));
    }

    [Test,AutoMoqData]
    public void Can_create_report_with_complex_nested_performances(Scenario scenario,
                                                                   Reportable reportable1,
                                                                   Reportable reportable2,
                                                                   Reportable reportable3,
                                                                   Reportable reportable4,
                                                                   Reportable reportable5)
    {
      // Arrange
      reportable1.Category = ReportableCategory.Given;
      reportable1.Type = ReportableType.Success;
      reportable1.Report = "Joe does a thing";
      reportable1.Reportables.Clear();

      reportable2.Category = ReportableCategory.Given;
      reportable2.Type = ReportableType.Success;
      reportable2.Report = "Joe does a different thing";
      reportable2.Reportables.Clear();

      reportable3.Category = ReportableCategory.Given;
      reportable3.Type = ReportableType.Success;
      reportable3.Report = "Joe does a totally different thing";
      reportable3.Reportables.Clear();

      reportable4.Category = ReportableCategory.Given;
      reportable4.Type = ReportableType.Success;
      reportable4.Report = "Joe does an unrelated thing";
      reportable4.Reportables.Clear();

      reportable5.Category = ReportableCategory.When;
      reportable5.Type = ReportableType.Success;
      reportable5.Report = "Joe takes some kind of action";
      reportable5.Reportables.Clear();

      reportable1.Reportables.Add(reportable2);
      reportable1.Reportables.Add(reportable4);
      reportable2.Reportables.Add(reportable3);
      scenario.Reportables.Clear();
      scenario.Reportables.Add(reportable1);
      scenario.Reportables.Add(reportable5);

      var report = new Report();
      report.Scenarios.Add(scenario);

      // Act
      var result = ExerciseSut(report);

      // Assert
      Assert.That(result, Contains.Substring(@"Given Joe does a thing
          Joe does a different thing
              Joe does a totally different thing
          Joe does an unrelated thing
 When Joe takes some kind of action"));
    }
  }
}
