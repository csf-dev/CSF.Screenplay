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
  }
}