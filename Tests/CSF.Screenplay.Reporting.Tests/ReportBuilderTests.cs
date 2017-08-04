using NUnit.Framework;
using System;
using CSF.Screenplay.Reporting.Builders;
using System.Linq;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Reporting.Models;

namespace CSF.Screenplay.Reporting.Tests
{
  [TestFixture]
  public class ReportBuilderTests
  {
    [Test,AutoMoqData]
    public void BeginNewScenario_creates_single_scenario(string id,
                                                         string name,
                                                         string feature,
                                                         ReportBuilder sut)
    {
      // Act
      sut.BeginNewScenario(id, name, feature);

      // Assert
      var report = sut.GetReport();
      Assert.That(report.Scenarios, Is.Not.Null, "Scenarios is not null");
      Assert.That(report.Scenarios.Count, Is.EqualTo(1), "One scenario present");
      var scenario = report.Scenarios.Single();
      Assert.That(scenario.Id, Is.EqualTo(id), "Scenario ID is correct");
      Assert.That(scenario.FriendlyName, Is.EqualTo(name), "Scenario name is correct");
      Assert.That(scenario.Feature, Is.EqualTo(feature), "Scenario feature name is correct");
    }

    [Test,AutoMoqData]
    public void EndScenario_raises_exception_when_called_before_BeginScenario(ReportBuilder sut,
                                                                              bool success)
    {
      // Act & assert
      Assert.That(() => sut.EndScenario(success), Throws.TypeOf<InvalidOperationException>());
    }

    [Test,AutoMoqData]
    public void EndScenario_closes_the_current_scenario(ReportBuilder sut, bool success, string idOne, string idTwo)
    {
      // Arrange
      sut.BeginNewScenario(idOne);

      // Act
      sut.EndScenario(success);
      sut.BeginNewScenario(idTwo);

      // Assert
      var report = sut.GetReport();
      var scenarioNames = report.Scenarios.Select(x => x.Id).ToArray();
      Assert.That(scenarioNames, Is.EquivalentTo(new [] { idOne, idTwo }));
    }

    [Test,AutoMoqData]
    public void BeginPerformance_raises_exception_when_called_before_BeginScenario(INamed actor,
                                                                                   IPerformable performable,
                                                                                   ReportBuilder sut)
    {
      // Act & assert
      Assert.That(() => sut.BeginPerformance(actor, performable), Throws.TypeOf<InvalidOperationException>());
    }

    [Test,AutoMoqData]
    public void BeginPerformance_raises_exception_when_called_after_EndScenario(INamed actor,
                                                                                IPerformable performable,
                                                                                string id,
                                                                                bool success,
                                                                                ReportBuilder sut)
    {
      // Arrange
      sut.BeginNewScenario(id);
      sut.EndScenario(success);

      // Act & assert
      Assert.That(() => sut.BeginPerformance(actor, performable), Throws.TypeOf<InvalidOperationException>());
    }

    [Test,AutoMoqData]
    public void BeginPerformanceType_raises_exception_when_called_before_BeginScenario(PerformanceType type,
                                                                                       ReportBuilder sut)
    {
      // Act & assert
      Assert.That(() => sut.BeginPerformanceType(type), Throws.TypeOf<InvalidOperationException>());
    }

    [Test,AutoMoqData]
    public void BeginPerformanceType_raises_exception_when_called_after_EndScenario(PerformanceType type,
                                                                                    string id,
                                                                                    bool success,
                                                                                    ReportBuilder sut)
    {
      // Arrange
      sut.BeginNewScenario(id);
      sut.EndScenario(success);

      // Act & assert
      Assert.That(() => sut.BeginPerformanceType(type), Throws.TypeOf<InvalidOperationException>());
    }

    [Test,AutoMoqData]
    public void BeginPerformanceType_marks_a_performance_with_the_appropriate_type(ReportBuilder sut,
                                                                                   string id,
                                                                                   INamed actor,
                                                                                   IPerformable performable)
    {
      // Arrange
      var type = PerformanceType.Then;
      sut.BeginNewScenario(id);

      // Act
      sut.BeginPerformanceType(type);
      sut.BeginPerformance(actor, performable);
      sut.RecordSuccess(performable);

      // Assert
      var report = sut.GetReport();
      Assert.That(report.Scenarios.Single().Reportables.Single().PerformanceType, Is.EqualTo(type));
    }

    [Test,AutoMoqData]
    public void BeginPerformance_records_the_actor(ReportBuilder sut,
                                                   string id,
                                                   INamed actor,
                                                   IPerformable performable)
    {
      // Arrange
      sut.BeginNewScenario(id);

      // Act
      sut.BeginPerformance(actor, performable);
      sut.RecordSuccess(performable);

      // Assert
      var report = sut.GetReport();
      Assert.That(report.Scenarios.Single().Reportables.Single().Actor, Is.SameAs(actor));
    }

    [Test,AutoMoqData]
    public void BeginPerformance_creates_a_performance_report(ReportBuilder sut,
                                                              string id,
                                                              INamed actor,
                                                              IPerformable performable)
    {
      // Arrange
      sut.BeginNewScenario(id);

      // Act
      sut.BeginPerformance(actor, performable);
      sut.RecordSuccess(performable);

      // Assert
      var report = sut.GetReport();
      var performance = report.Scenarios.Single().Reportables.Single() as Performance;
      Assert.That(performable, Is.Not.Null);
    }

    [Test,AutoMoqData]
    public void BeginPerformance_records_the_performable(ReportBuilder sut,
                                                         string id,
                                                         INamed actor,
                                                         IPerformable performable)
    {
      // Arrange
      sut.BeginNewScenario(id);

      // Act
      sut.BeginPerformance(actor, performable);
      sut.RecordSuccess(performable);

      // Assert
      var report = sut.GetReport();
      var performance = report.Scenarios.Single().Reportables.Single() as Performance;
      Assert.That(performance.Performable, Is.SameAs(performable));
    }

    [Test,AutoMoqData]
    public void BeginPerformance_can_create_nested_reportables(ReportBuilder sut,
                                                               string id,
                                                               INamed actor,
                                                               IPerformable performable,
                                                               IPerformable differentPerformable)
    {
      // Arrange
      sut.BeginNewScenario(id);
      sut.BeginPerformance(actor, performable);

      // Act
      sut.BeginPerformance(actor, differentPerformable);
      sut.RecordSuccess(differentPerformable);
      sut.RecordSuccess(performable);

      // Assert
      var report = sut.GetReport();
      var performance = report.Scenarios.Single().Reportables.Single() as Performance;
      var childPerformance = performance.Reportables.Single() as Performance;
      Assert.That(childPerformance.Performable, Is.SameAs(differentPerformable));
    }
  }
}
