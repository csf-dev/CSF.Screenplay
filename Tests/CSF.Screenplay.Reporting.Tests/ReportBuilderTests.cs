using NUnit.Framework;
using System;
using CSF.Screenplay.Reporting.Builders;
using System.Linq;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Reporting.Models;
using Moq;
using System.Collections.Generic;

namespace CSF.Screenplay.Reporting.Tests
{
  [TestFixture,Parallelizable]
  public class ReportBuilderTests
  {
    [Test,AutoMoqData]
    public void BeginNewScenario_creates_single_scenario(string id,
                                                         string name,
                                                         string feature,
                                                         ReportBuilder sut,
                                                         Guid scenarioIdentity)
    {
      // Act
      sut.BeginNewScenario(id, name, feature, null, scenarioIdentity);

      // Assert
      var report = sut.GetReport();
      Assert.That(report.Scenarios, Is.Not.Null, "Scenarios is not null");
      Assert.That(report.Scenarios.Count, Is.EqualTo(1), "One scenario present");
      var scenario = report.Scenarios.Single();
      Assert.That(scenario.Id, Is.EqualTo(id), "Scenario ID is correct");
      Assert.That(scenario.FriendlyName, Is.EqualTo(name), "Scenario name is correct");
      Assert.That(scenario.FeatureName, Is.EqualTo(feature), "Scenario feature name is correct");
    }

    [Test,AutoMoqData]
    public void BeginNewScenario_passes_feature_name(string id,
                                                     string featureId,
                                                     ReportBuilder sut,
                                                     Guid scenarioIdentity)
    {
      // Act
      sut.BeginNewScenario(id, null, null, featureId, scenarioIdentity);

      // Assert
      var report = sut.GetReport();
      var scenario = report.Scenarios.Single();
      Assert.That(scenario.FeatureId, Is.EqualTo(featureId));
    }

    [Test,AutoMoqData]
    public void EndScenario_raises_exception_when_called_before_BeginScenario(ReportBuilder sut,
                                                                              bool success,
                                                                              Guid scenarioIdentity)
    {
      // Act & assert
      Assert.That(() => sut.EndScenario(success, scenarioIdentity), Throws.TypeOf<InvalidOperationException>());
    }

    [Test,AutoMoqData]
    public void EndScenario_closes_the_current_scenario(ReportBuilder sut,
                                                        bool success,
                                                        string idOne,
                                                        string idTwo,
                                                        Guid scenarioIdentity,
                                                        Guid otherIdentity)
    {
      // Arrange
      sut.BeginNewScenario(idOne, null, null, null, scenarioIdentity);

      // Act
      sut.EndScenario(success, scenarioIdentity);
      sut.BeginNewScenario(idTwo, null, null, null, otherIdentity);

      // Assert
      var report = sut.GetReport();
      var scenarioNames = report.Scenarios.Select(x => x.Id).ToArray();
      Assert.That(scenarioNames, Is.EquivalentTo(new [] { idOne, idTwo }));
    }

    [Test,AutoMoqData]
    public void BeginScenario_with_the_same_identity_twice_raises_an_exception(ReportBuilder sut,
                                                                               bool success,
                                                                               string idOne,
                                                                               string idTwo,
                                                                               Guid scenarioIdentity)
    {
      // Arrange
      sut.BeginNewScenario(idOne, null, null, null, scenarioIdentity);

      // Act & assert
      Assert.That(() => sut.BeginNewScenario(idTwo, null, null, null, scenarioIdentity), Throws.InvalidOperationException);
    }

    [Test,AutoMoqData]
    public void BeginPerformance_raises_exception_when_called_before_BeginScenario(INamed actor,
                                                                                   IPerformable performable,
                                                                                   ReportBuilder sut,
                                                                                   Guid scenarioIdentity)
    {
      // Act & assert
      Assert.That(() => sut.BeginPerformance(actor, performable, scenarioIdentity), Throws.TypeOf<InvalidOperationException>());
    }

    [Test,AutoMoqData]
    public void BeginPerformance_raises_exception_when_called_after_EndScenario(INamed actor,
                                                                                IPerformable performable,
                                                                                string id,
                                                                                bool success,
                                                                                ReportBuilder sut,
                                                                                Guid scenarioIdentity)
    {
      // Arrange
      sut.BeginNewScenario(id, null, null, null, scenarioIdentity);
      sut.EndScenario(success, scenarioIdentity);

      // Act & assert
      Assert.That(() => sut.BeginPerformance(actor, performable, scenarioIdentity), Throws.TypeOf<InvalidOperationException>());
    }

    [Test,AutoMoqData]
    public void BeginPerformanceType_raises_exception_when_called_before_BeginScenario(PerformanceType type,
                                                                                       ReportBuilder sut,
                                                                                       Guid scenarioIdentity)
    {
      // Act & assert
      Assert.That(() => sut.BeginPerformanceType(type, scenarioIdentity), Throws.TypeOf<InvalidOperationException>());
    }

    [Test,AutoMoqData]
    public void BeginPerformanceType_raises_exception_when_called_after_EndScenario(PerformanceType type,
                                                                                    string id,
                                                                                    bool success,
                                                                                    ReportBuilder sut,
                                                                                    Guid scenarioIdentity)
    {
      // Arrange
      sut.BeginNewScenario(id, null, null, null, scenarioIdentity);
      sut.EndScenario(success, scenarioIdentity);

      // Act & assert
      Assert.That(() => sut.BeginPerformanceType(type, scenarioIdentity), Throws.TypeOf<InvalidOperationException>());
    }

    [Test,AutoMoqData]
    public void BeginPerformanceType_marks_a_performance_with_the_appropriate_type(ReportBuilder sut,
                                                                                   string id,
                                                                                   INamed actor,
                                                                                   IPerformable performable,
                                                                                   Guid scenarioIdentity)
    {
      // Arrange
      var type = PerformanceType.Then;
      sut.BeginNewScenario(id, null, null, null, scenarioIdentity);

      // Act
      sut.BeginPerformanceType(type, scenarioIdentity);
      sut.BeginPerformance(actor, performable, scenarioIdentity);
      sut.RecordSuccess(performable, scenarioIdentity);

      // Assert
      var report = sut.GetReport();
      Assert.That(report.Scenarios.Single().Reportables.Single().PerformanceType, Is.EqualTo(type));
    }

    [Test,AutoMoqData]
    public void BeginPerformance_records_the_actor_name(ReportBuilder sut,
                                                        string id,
                                                        INamed actor,
                                                        IPerformable performable,
                                                        Guid scenarioIdentity)
    {
      // Arrange
      sut.BeginNewScenario(id, null, null, null, scenarioIdentity);

      // Act
      sut.BeginPerformance(actor, performable, scenarioIdentity);
      sut.RecordSuccess(performable, scenarioIdentity);

      // Assert
      var report = sut.GetReport();
      Assert.That(report.Scenarios.Single().Reportables.Single().Actor.Name, Is.EqualTo(actor.Name));
    }

    [Test,AutoMoqData]
    public void BeginPerformance_creates_a_performance_report(ReportBuilder sut,
                                                              string id,
                                                              INamed actor,
                                                              IPerformable performable,
                                                              Guid scenarioIdentity)
    {
      // Arrange
      sut.BeginNewScenario(id, null, null, null, scenarioIdentity);

      // Act
      sut.BeginPerformance(actor, performable, scenarioIdentity);
      sut.RecordSuccess(performable, scenarioIdentity);

      // Assert
      var report = sut.GetReport();
      var performance = report.Scenarios.Single().Reportables.Single() as Performance;
      Assert.That(performable, Is.Not.Null);
    }

    [Test,AutoMoqData]
    public void BeginPerformance_records_the_performable(ReportBuilder sut,
                                                         string id,
                                                         INamed actor,
                                                         IPerformable performable,
                                                         string reportString,
                                                         Guid scenarioIdentity)
    {
      // Arrange
      sut.BeginNewScenario(id, null, null, null, scenarioIdentity);
      Mock.Get(performable).Setup(x => x.GetReport(actor)).Returns(reportString);

      // Act
      sut.BeginPerformance(actor, performable, scenarioIdentity);
      sut.RecordSuccess(performable, scenarioIdentity);

      // Assert
      var report = sut.GetReport();
      var performance = report.Scenarios.Single().Reportables.Single() as Performance;
      Assert.That(performance.Report, Is.EqualTo(reportString));
    }

    [Test,AutoMoqData]
    public void BeginPerformance_can_create_nested_reportables(ReportBuilder sut,
                                                               string id,
                                                               INamed actor,
                                                               IPerformable performable,
                                                               IPerformable differentPerformable,
                                                               string reportString,
                                                               Guid scenarioIdentity)
    {
      // Arrange
      sut.BeginNewScenario(id, null, null, null, scenarioIdentity);
      sut.BeginPerformance(actor, performable, scenarioIdentity);
      Mock.Get(differentPerformable).Setup(x => x.GetReport(actor)).Returns(reportString);

      // Act
      sut.BeginPerformance(actor, differentPerformable, scenarioIdentity);
      sut.RecordSuccess(differentPerformable, scenarioIdentity);
      sut.RecordSuccess(performable, scenarioIdentity);

      // Assert
      var report = sut.GetReport();
      var performance = report.Scenarios.Single().Reportables.Single() as Performance;
      var childPerformance = performance.Reportables.Single() as Performance;
      Assert.That(childPerformance.Report, Is.EqualTo(reportString));
    }

    [Test,AutoMoqData]
    public void GetReport_uses_report_factory(IReportFactory factory,
                                              string id,
                                              INamed actor,
                                              IPerformable performable,
                                              Guid scenarioIdentity,
                                              Report report)
    {
      // Arrange
      var sut = new ReportBuilder(factory);
      sut.BeginNewScenario(id, null, null, null, scenarioIdentity);
      sut.BeginPerformance(actor, performable, scenarioIdentity);
      sut.RecordSuccess(performable, scenarioIdentity);

      Mock.Get(factory)
          .Setup(x => x.GetReport(It.IsAny<IReadOnlyCollection<Scenario>>()))
          .Returns(report);

      // Act
      var result = sut.GetReport();

      // Assert
      Assert.AreSame(report, result, "Result is same instance returned by factory");
      Mock.Get(factory)
          .Verify(x => x.GetReport(It.IsAny<IReadOnlyCollection<Scenario>>()), Times.Once());
    }
  }
}
