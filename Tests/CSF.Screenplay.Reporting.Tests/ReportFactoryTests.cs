using System.Linq;
using CSF.Screenplay.Reporting.Builders;
using CSF.Screenplay.Reporting.Models;
using CSF.Screenplay.Reporting.Tests.Autofixture;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace CSF.Screenplay.Reporting.Tests
{
  [TestFixture]
  public class ReportFactoryTests
  {
    [Test,AutoMoqData]
    public void GetReport_creates_report_using_all_scenarios(ReportFactory sut,
                                                             Scenario scenarioOne,
                                                             Scenario scenarioTwo,
                                                             Scenario scenarioThree)
    {
      // Arrange
      var allScenarios = new [] { scenarioOne, scenarioTwo, scenarioThree };

      // Act
      var result = sut.GetReport(allScenarios);

      // Assert
      Assert.That(result.Scenarios, Is.EquivalentTo(allScenarios));
    }

    [Test,AutoMoqData]
    public void GetReport_calls_getScenario_from_all_builders(ReportFactory sut,
                                                              IBuildsScenario scenarioOne,
                                                              IBuildsScenario scenarioTwo,
                                                              IBuildsScenario scenarioThree,
                                                              Scenario scenario)
    {
      // Arrange
      Mock.Get(scenarioOne).Setup(x => x.GetScenario()).Returns(scenario);
      Mock.Get(scenarioTwo).Setup(x => x.GetScenario()).Returns(scenario);
      Mock.Get(scenarioThree).Setup(x => x.GetScenario()).Returns(scenario);

      var allBuilders = new [] { scenarioOne, scenarioTwo, scenarioThree };
      var sameScenarioThreeTimes = new [] { scenario, scenario, scenario };

      // Act
      var result = sut.GetReport(allBuilders);

      // Assert
      Mock.Get(scenarioOne).Verify(x => x.GetScenario(), Times.Once);
      Mock.Get(scenarioTwo).Verify(x => x.GetScenario(), Times.Once);
      Mock.Get(scenarioThree).Verify(x => x.GetScenario(), Times.Once);

      Assert.That(result.Scenarios, Is.EquivalentTo(sameScenarioThreeTimes));
    }
  }
}