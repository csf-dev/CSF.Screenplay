using System;
using static CSF.Screenplay.NUnit.ScenarioGetter;
using NUnit.Framework;

namespace CSF.Screenplay.Web.Tests
{
  [TestFixture,Screenplay]
  public class NUnitIntegrationTests
  {
    [Test]
    [Description("An NUnit test fixture decorated with `Screenplay' can access the current scenario via a static property")]
    public void ScreenplayScenario_is_visible_from_ScenarioGetter()
    {
      // Assert
      Assert.That(Scenario, Is.Not.Null);
    }
  }
}
