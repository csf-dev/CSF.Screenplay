using System;
using CSF.Screenplay.NUnit;
using NUnit.Framework;

namespace CSF.Screenplay.Web.Tests
{
  [ScreenplayTest]
  public class NUnitIntegrationTests
  {
    [Test]
    [Description("An NUnit test fixture decorated with `ScreenplayFixture' receives an injected screenplay scenario")]
    public void ScreenplayContext_should_be_injected_by_ScreenplayFixture_attribute(ScreenplayScenario screenplay)
    {
      // Assert
      Assert.That(screenplay, Is.Not.Null);
    }
  }
}
