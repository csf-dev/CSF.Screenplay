using System.Linq;
using CSF.Screenplay.Reporting.Models;
using CSF.Screenplay.Reporting.Tests.Autofixture;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace CSF.Screenplay.Reporting.Tests
{
  [TestFixture]
  public class ReportFactoryTests
  {
    [Test,AutoMoqData]
    public void GetReport_returns_all_scenarios(ReportFactory sut,
                                                [NamedScenario] Scenario scenarioOne,
                                                [NamedScenario] Scenario scenarioTwo,
                                                [NamedScenario] Scenario scenarioThree)
    {
      // Arrange
      var allScenarios = new [] { scenarioOne, scenarioTwo, scenarioThree };

      // Act
      var result = sut.GetReport(allScenarios);

      // Assert
      CollectionAssert.AreEquivalent(allScenarios, result.Scenarios);
    }

    [Test,AutoMoqData]
    public void GetReport_does_not_create_duplicate_features_when_multiple_scenarios_use_the_same_feature(
      ReportFactory sut,
      [NamedScenario(FeatureId = "TestFeature")] Scenario scenarioOne,
      [NamedScenario(FeatureId = "TestFeature")] Scenario scenarioTwo,
      [NamedScenario(FeatureId = "TestFeature")] Scenario scenarioThree)
    {
      // Arrange
      var allScenarios = new [] { scenarioOne, scenarioTwo, scenarioThree };

      // Act
      var result = sut.GetReport(allScenarios);

      // Assert
      Assert.AreEqual(1, result.Features.Count, "Only one feature created");
      Assert.AreEqual("TestFeature", result.Features.First().Id);
    }

    [Test,AutoMoqData]
    public void GetReport_places_all_scenarios_in_the_correct_feature(
      ReportFactory sut,
      [NamedScenario(FeatureId = "TestFeature1")] Scenario scenarioOne,
      [NamedScenario(FeatureId = "TestFeature2")] Scenario scenarioTwo,
      [NamedScenario(FeatureId = "TestFeature1")] Scenario scenarioThree)
    {
      // Arrange
      var allScenarios = new [] { scenarioOne, scenarioTwo, scenarioThree };
      var expectedScenariosInFeature1 = new [] { scenarioOne, scenarioThree };
      var expectedScenariosInFeature2 = new [] { scenarioTwo };

      // Act
      var result = sut.GetReport(allScenarios);

      // Assert
      Assert.AreEqual(2, result.Features.Count, "Two features created");
      var actualScenariosInFeature1 = result.Features.Single(x => x.Id == "TestFeature1").Scenarios;
      var actualScenariosInFeature2 = result.Features.Single(x => x.Id == "TestFeature2").Scenarios;

      CollectionAssert.AreEquivalent(expectedScenariosInFeature1, actualScenariosInFeature1, "Scenarios in feature 1");
      CollectionAssert.AreEquivalent(expectedScenariosInFeature2, actualScenariosInFeature2, "Scenarios in feature 2");
    }

    [Test,AutoMoqData]
    public void GetReport_orders_features_alphabetically_by_name(
      ReportFactory sut,
      [NamedScenario(FeatureId = "C feature", FeatureName = "CC this is the third feature")] Scenario scenarioOne,
      [NamedScenario(FeatureId = "A feature", FeatureName = "AA this is the first feature")] Scenario scenarioTwo,
      [NamedScenario(FeatureId = "B feature", FeatureName = "BB this is the second feature")] Scenario scenarioThree)
    {
      // Arrange
      var allScenarios = new [] { scenarioOne, scenarioTwo, scenarioThree };
      var expectedFeatureOrder = new [] { "A feature", "B feature", "C feature" };

      // Act
      var result = sut.GetReport(allScenarios);

      // Assert
      var orderedFeatures = result.Features.Select(x => x.Id).ToArray();
      CollectionAssert.AreEqual(expectedFeatureOrder, orderedFeatures);
    }

    [Test,AutoMoqData]
    public void GetReport_orders_scenarios_alphabetically_by_name_within_features(
      ReportFactory sut,
      [NamedScenario(FeatureId = "TestFeature", ScenarioId = "S3", ScenarioName = "C third")] Scenario scenarioOne,
      [NamedScenario(FeatureId = "TestFeature", ScenarioId = "S1", ScenarioName = "A first")] Scenario scenarioTwo,
      [NamedScenario(FeatureId = "TestFeature", ScenarioId = "S2", ScenarioName = "B second")] Scenario scenarioThree)
    {
      // Arrange
      var allScenarios = new [] { scenarioOne, scenarioTwo, scenarioThree };
      var expectedScenarioOrder = new [] { "S1", "S2", "S3" };

      // Act
      var result = sut.GetReport(allScenarios);

      // Assert
      var feature = result.Features.Single();
      var actualScenarioOrder = feature.Scenarios.Select(x => x.Id).ToArray();
      CollectionAssert.AreEqual(expectedScenarioOrder, actualScenarioOrder);
    }
  }
}