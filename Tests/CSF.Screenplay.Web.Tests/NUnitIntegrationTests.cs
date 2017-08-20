using System;
using NUnit.Framework;

namespace CSF.Screenplay.Web.Tests
{
  [TestFixture]
  [Description("The NUnit/Screenplay integration")]
  public class NUnitIntegrationTests
  {
    [Test,Screenplay]
    [Description("An NUnit test decorated with `Screenplay' receives the current scenario as an injected parameter")]
    public void ScreenplayScenario_is_injected_from_parameter(ScreenplayScenario scenario)
    {
      // Assert
      Assert.That(scenario, Is.Not.Null);
    }
  }
}
