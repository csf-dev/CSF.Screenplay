using NUnit.Framework;
using System;
using CSF.Screenplay.Reporting.Builders;
using System.Linq;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Reporting.Models;
using Moq;
using System.Collections.Generic;
using Ploeh.AutoFixture.NUnit3;
using CSF.Screenplay.Abilities;

namespace CSF.Screenplay.Reporting.Tests
{
  [TestFixture,Parallelizable]
  public class ReportBuilderTests
  {
    #region "happy path" tests

    [Test,AutoMoqData]
    public void BeginNewScenario_uses_factory_to_create_a_scenario_using_the_scenario_identity(Guid scenarioIdentity,
                                                                                               IBuildsScenario scenarioBuilder,
                                                                                               IGetsReport reportFactory)
    {
      // Arrange
      Guid capturedGuid = Guid.Empty;
      Func<Guid,IBuildsScenario> scenarioBuilderFactory = g => {
        capturedGuid = g;
        return scenarioBuilder;
      };
      var sut = new ReportBuilder(reportFactory, scenarioBuilderFactory);

      // Act
      sut.BeginNewScenario(null, null, null, null, scenarioIdentity, false, false);

      // Assert
      Assert.That(capturedGuid, Is.EqualTo(scenarioIdentity));
    }

    [Test,AutoMoqData]
    public void BeginNewScenario_sets_all_scenario_and_feature_properties(Guid scenarioIdentity,
                                                                          IBuildsScenario scenarioBuilder,
                                                                          IGetsReport reportFactory,
                                                                          string scenarioId,
                                                                          string scenarioName,
                                                                          string featureId,
                                                                          string featureName,
                                                                          bool scenarioIdGenerated,
                                                                          bool featureIdGenerated)
    {
      // Arrange
      Func<Guid,IBuildsScenario> scenarioBuilderFactory = g => scenarioBuilder;
      var sut = new ReportBuilder(reportFactory, scenarioBuilderFactory);

      // Act
      sut.BeginNewScenario(scenarioId,
                           scenarioName,
                           featureName,
                           featureId,
                           scenarioIdentity,
                           scenarioIdGenerated,
                           featureIdGenerated);

      // Assert
      Mock.Get(scenarioBuilder).VerifySet(x => x.ScenarioIdName = scenarioId, Times.Once, nameof(IBuildsScenario.ScenarioIdName));
      Mock.Get(scenarioBuilder).VerifySet(x => x.ScenarioFriendlyName = scenarioName, Times.Once, nameof(IBuildsScenario.ScenarioFriendlyName));
      Mock.Get(scenarioBuilder).VerifySet(x => x.FeatureIdName = featureId, Times.Once, nameof(IBuildsScenario.FeatureIdName));
      Mock.Get(scenarioBuilder).VerifySet(x => x.FeatureFriendlyName = featureName, Times.Once, nameof(IBuildsScenario.FeatureFriendlyName));
      Mock.Get(scenarioBuilder).VerifySet(x => x.ScenarioIdIsGenerated = scenarioIdGenerated, Times.Once, nameof(IBuildsScenario.ScenarioIdIsGenerated));
      Mock.Get(scenarioBuilder).VerifySet(x => x.FeatureIdIsGenerated = featureIdGenerated, Times.Once, nameof(IBuildsScenario.FeatureIdIsGenerated));
    }

    [Test,AutoMoqData]
    public void EndScenario_calls_finalise_for_the_appropriate_builder(IBuildsScenario scenarioBuilder,
                                                                       IGetsReport reportFactory,
                                                                       bool success,
                                                                       Guid scenarioIdentity)
    {
      // Arrange
      Func<Guid,IBuildsScenario> scenarioBuilderFactory = g => scenarioBuilder;
      var sut = new ReportBuilder(reportFactory, scenarioBuilderFactory);
      sut.BeginNewScenario(null, null, null, null, scenarioIdentity, false, false);

      // Act
      sut.EndScenario(success, scenarioIdentity);

      // Assert
      Mock.Get(scenarioBuilder).Verify(x => x.Finalise(success), Times.Once);
    }

    [Test,AutoMoqData]
    public void BeginPerformance_calls_BeginPerformance_from_builder(IBuildsScenario scenarioBuilder,
                                                                     IGetsReport reportFactory,
                                                                     Guid scenarioIdentity,
                                                                     IActor actor,
                                                                     IPerformable performable)
    {
      // Arrange
      Func<Guid,IBuildsScenario> scenarioBuilderFactory = g => scenarioBuilder;
      var sut = new ReportBuilder(reportFactory, scenarioBuilderFactory);
      sut.BeginNewScenario(null, null, null, null, scenarioIdentity, false, false);

      // Act
      sut.BeginPerformance(actor, performable, scenarioIdentity);

      // Assert
      Mock.Get(scenarioBuilder).Verify(x => x.BeginPerformance(actor, performable), Times.Once);
    }

    [Test,AutoMoqData]
    public void BeginPerformanceType_calls_BeginPerformanceType_from_builder(IBuildsScenario scenarioBuilder,
                                                                             IGetsReport reportFactory,
                                                                             Guid scenarioIdentity,
                                                                             ReportableCategory category)
    {
      // Arrange
      Func<Guid,IBuildsScenario> scenarioBuilderFactory = g => scenarioBuilder;
      var sut = new ReportBuilder(reportFactory, scenarioBuilderFactory);
      sut.BeginNewScenario(null, null, null, null, scenarioIdentity, false, false);

      // Act
      sut.BeginPerformanceCategory(category, scenarioIdentity);

      // Assert
      Mock.Get(scenarioBuilder).Verify(x => x.BeginPerformanceCategory(category), Times.Once);
    }

    [Test,AutoMoqData]
    public void RecordResult_calls_RecordResult_from_builder(IBuildsScenario scenarioBuilder,
                                                             IGetsReport reportFactory,
                                                             IActor actor,
                                                             IPerformable performable,
                                                             object result,
                                                             Guid scenarioIdentity)
    {
      // Arrange
      Func<Guid,IBuildsScenario> scenarioBuilderFactory = g => scenarioBuilder;
      var sut = new ReportBuilder(reportFactory, scenarioBuilderFactory);
      sut.BeginNewScenario(null, null, null, null, scenarioIdentity, false, false);
      sut.BeginPerformance(actor, performable, scenarioIdentity);

      // Act
      sut.RecordResult(performable, result, scenarioIdentity);

      // Assert
      Mock.Get(scenarioBuilder).Verify(x => x.RecordResult(performable, result), Times.Once);
    }

    [Test,AutoMoqData]
    public void RecordFailure_calls_RecordFailure_from_builder(IBuildsScenario scenarioBuilder,
                                                               IGetsReport reportFactory,
                                                               IActor actor,
                                                               IPerformable performable,
                                                               Exception exception,
                                                               Guid scenarioIdentity)
    {
      // Arrange
      Func<Guid,IBuildsScenario> scenarioBuilderFactory = g => scenarioBuilder;
      var sut = new ReportBuilder(reportFactory, scenarioBuilderFactory);
      sut.BeginNewScenario(null, null, null, null, scenarioIdentity, false, false);
      sut.BeginPerformance(actor, performable, scenarioIdentity);

      // Act
      sut.RecordFailure(performable, exception, scenarioIdentity);

      // Assert
      Mock.Get(scenarioBuilder).Verify(x => x.RecordFailure(performable, exception), Times.Once);
    }

    [Test,AutoMoqData]
    public void RecordSuccess_calls_RecordSuccess_from_builder(IBuildsScenario scenarioBuilder,
                                                               IGetsReport reportFactory,
                                                               IActor actor,
                                                               IPerformable performable,
                                                               Guid scenarioIdentity)
    {
      // Arrange
      Func<Guid,IBuildsScenario> scenarioBuilderFactory = g => scenarioBuilder;
      var sut = new ReportBuilder(reportFactory, scenarioBuilderFactory);
      sut.BeginNewScenario(null, null, null, null, scenarioIdentity, false, false);
      sut.BeginPerformance(actor, performable, scenarioIdentity);

      // Act
      sut.RecordSuccess(performable, scenarioIdentity);

      // Assert
      Mock.Get(scenarioBuilder).Verify(x => x.RecordSuccess(performable), Times.Once);
    }

    [Test,AutoMoqData]
    public void EndPerformanceType_calls_EndPerformanceType_from_builder(IBuildsScenario scenarioBuilder,
                                                                         IGetsReport reportFactory,
                                                                           Guid scenarioIdentity)
    {
      // Arrange
      Func<Guid,IBuildsScenario> scenarioBuilderFactory = g => scenarioBuilder;
      var sut = new ReportBuilder(reportFactory, scenarioBuilderFactory);
      sut.BeginNewScenario(null, null, null, null, scenarioIdentity, false, false);

      // Act
      sut.EndPerformanceCategory(scenarioIdentity);

      // Assert
      Mock.Get(scenarioBuilder).Verify(x => x.EndPerformanceCategory(), Times.Once);
    }

    [Test,AutoMoqData]
    public void GainAbility_calls_GainAbility_from_builder(IBuildsScenario scenarioBuilder,
                                                           IGetsReport reportFactory,
                                                                    INamed actor,
                                                                    IAbility ability,
                                                                    Guid scenarioIdentity)
    {
      // Arrange
      Func<Guid,IBuildsScenario> scenarioBuilderFactory = g => scenarioBuilder;
      var sut = new ReportBuilder(reportFactory, scenarioBuilderFactory);
      sut.BeginNewScenario(null, null, null, null, scenarioIdentity, false, false);

      // Act
      sut.GainAbility(actor, ability, scenarioIdentity);

      // Assert
      Mock.Get(scenarioBuilder).Verify(x => x.GainAbility(actor, ability), Times.Once);
    }

    [Test,AutoMoqData]
    public void GetReport_passes_builder_created_from_scenario_factory_to_report_factory(IBuildsScenario scenarioBuilder,
                                                                                         IGetsReport reportFactory,
                                                                                         Guid scenarioIdentity)
    {
      // Arrange
      Func<Guid,IBuildsScenario> scenarioBuilderFactory = g => scenarioBuilder;
      var sut = new ReportBuilder(reportFactory, scenarioBuilderFactory);
      sut.BeginNewScenario(null, null, null, null, scenarioIdentity, false, false);

      // Act
      sut.GetReport();

      // Assert
      Mock.Get(reportFactory)
          .Verify(x => x.GetReport(It.Is<IEnumerable<IBuildsScenario>>(b => b.Contains(scenarioBuilder))), Times.Once());
    }

    [Test,AutoMoqData]
    public void GetReport_passes_same_number_of_builders_as_scenarios(IBuildsScenario scenarioBuilder,
                                                                      IGetsReport reportFactory,
                                                                      [Values(1, 2, 5, 10)] int howManyScenarios)
    {
      // Arrange
      Func<Guid,IBuildsScenario> scenarioBuilderFactory = g => scenarioBuilder;
      var sut = new ReportBuilder(reportFactory, scenarioBuilderFactory);
      foreach(var iterations in Enumerable.Range(0, howManyScenarios))
        sut.BeginNewScenario(null, null, null, null, Guid.NewGuid(), false, false);

      // Act
      sut.GetReport();

      // Assert
      Mock.Get(reportFactory)
          .Verify(x => x.GetReport(It.Is<IEnumerable<IBuildsScenario>>(b => b.Count() == howManyScenarios)), Times.Once());
    }

    [Test,AutoMoqData]
    public void GetReport_returns_result_from_factory(IBuildsScenario scenarioBuilder,
                                                      IGetsReport reportFactory,
                                                      Report report)
    {
      // Arrange
      Func<Guid,IBuildsScenario> scenarioBuilderFactory = g => scenarioBuilder;
      var sut = new ReportBuilder(reportFactory, scenarioBuilderFactory);
      Mock.Get(reportFactory)
          .Setup(x => x.GetReport(It.IsAny<IEnumerable<IBuildsScenario>>()))
          .Returns(report);

      // Act
      var result = sut.GetReport();

      // Assert
      Assert.That(result, Is.SameAs(report));
    }

    #endregion

    #region error: scenario has not begun yet

    [Test,AutoMoqData]
    public void EndScenario_raises_exception_when_called_for_a_scenario_which_has_not_begun(ReportBuilder sut,
                                                                                            bool success,
                                                                                            Guid scenarioIdentity)
    {
      // Act & assert
      Assert.That(() => sut.EndScenario(success, scenarioIdentity),
                  Throws.TypeOf<ScenarioHasNotBegunException>());
    }

    [Test,AutoMoqData]
    public void BeginPerformance_raises_exception_when_called_for_a_scenario_which_has_not_begun(ReportBuilder sut,
                                                                                                 INamed actor,
                                                                                                 IPerformable performable,
                                                                                                 Guid scenarioIdentity)
    {
      // Act & assert
      Assert.That(() => sut.BeginPerformance(actor, performable, scenarioIdentity),
                  Throws.TypeOf<ScenarioHasNotBegunException>());
    }

    [Test,AutoMoqData]
    public void BeginPerformanceType_raises_exception_when_called_for_a_scenario_which_has_not_begun(ReportBuilder sut,
                                                                                                     ReportableCategory category,
                                                                                                     Guid scenarioIdentity)
    {
      // Act & assert
      Assert.That(() => sut.BeginPerformanceCategory(category, scenarioIdentity),
                  Throws.TypeOf<ScenarioHasNotBegunException>());
    }

    [Test,AutoMoqData]
    public void RecordResult_raises_exception_when_called_for_a_scenario_which_has_not_begun(ReportBuilder sut,
                                                                                             IPerformable performable,
                                                                                             object result,
                                                                                             Guid scenarioIdentity)
    {
      // Act & assert
      Assert.That(() => sut.RecordResult(performable, result, scenarioIdentity),
                  Throws.TypeOf<ScenarioHasNotBegunException>());
    }

    [Test,AutoMoqData]
    public void RecordFailure_raises_exception_when_called_for_a_scenario_which_has_not_begun(ReportBuilder sut,
                                                                                              IPerformable performable,
                                                                                              Exception exception,
                                                                                              Guid scenarioIdentity)
    {
      // Act & assert
      Assert.That(() => sut.RecordFailure(performable, exception, scenarioIdentity),
                  Throws.TypeOf<ScenarioHasNotBegunException>());
    }

    [Test,AutoMoqData]
    public void RecordSuccess_raises_exception_when_called_for_a_scenario_which_has_not_begun(ReportBuilder sut,
                                                                                              IPerformable performable,
                                                                                              Guid scenarioIdentity)
    {
      // Act & assert
      Assert.That(() => sut.RecordSuccess(performable, scenarioIdentity),
                  Throws.TypeOf<ScenarioHasNotBegunException>());
    }

    [Test,AutoMoqData]
    public void EndPerformanceType_raises_exception_when_called_for_a_scenario_which_has_not_begun(ReportBuilder sut,
                                                                                                   Guid scenarioIdentity)
    {
      // Act & assert
      Assert.That(() => sut.EndPerformanceCategory(scenarioIdentity),
                  Throws.TypeOf<ScenarioHasNotBegunException>());
    }

    [Test,AutoMoqData]
    public void GainAbility_raises_exception_when_called_for_a_scenario_which_has_not_begun(ReportBuilder sut,
                                                                                            INamed actor,
                                                                                            IAbility ability,
                                                                                            Guid scenarioIdentity)
    {
      // Act & assert
      Assert.That(() => sut.GainAbility(actor, ability, scenarioIdentity),
                  Throws.TypeOf<ScenarioHasNotBegunException>());
    }

    #endregion

    #region error: beginning the same scenario twice

    [Test,AutoMoqData]
    public void BeginScenario_raises_an_exception_if_called_twice_with_the_same_identity(ReportBuilder sut,
                                                                                         Guid scenarioIdentity)
    {
      // Arrange
      sut.BeginNewScenario(null, null, null, null, scenarioIdentity, false, false);

      // Act & assert
      Assert.That(() => sut.BeginNewScenario(null, null, null, null, scenarioIdentity, false, false),
                  Throws.InstanceOf<ScenarioHasBegunAlreadyException>());
    }

    #endregion

    #region error: scenario has already ended

    [Test,AutoMoqData]
    public void BeginPerformance_raises_exception_when_called_after_EndScenario(INamed actor,
                                                                                IPerformable performable,
                                                                                bool success,
                                                                                ReportBuilder sut,
                                                                                Guid scenarioIdentity)
    {
      // Arrange
      sut.BeginNewScenario(null, null, null, null, scenarioIdentity, false, false);
      sut.EndScenario(success, scenarioIdentity);

      // Act & assert
      Assert.That(() => sut.BeginPerformance(actor, performable, scenarioIdentity),
                  Throws.TypeOf<ScenarioHasEndedAlreadyException>());
    }

    [Test,AutoMoqData]
    public void BeginPerformanceType_raises_exception_when_called_after_EndScenario(ReportableCategory type,
                                                                                    bool success,
                                                                                    ReportBuilder sut,
                                                                                    Guid scenarioIdentity)
    {
      // Arrange
      sut.BeginNewScenario(null, null, null, null, scenarioIdentity, false, false);
      sut.EndScenario(success, scenarioIdentity);

      // Act & assert
      Assert.That(() => sut.BeginPerformanceCategory(type, scenarioIdentity),
                  Throws.TypeOf<ScenarioHasEndedAlreadyException>());
    }

    #endregion

























  }
}
