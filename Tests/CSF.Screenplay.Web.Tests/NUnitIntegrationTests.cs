using System;
using CSF.Screenplay.NUnit;
using NUnit.Framework;

namespace CSF.Screenplay.Web.Tests
{
  [ScreenplayFixture]
  public class NUnitIntegrationTests
  {
    readonly ScreenplayContext context;

    [Test]
    [Description("An NUnit test fixture decorated with `ScreenplayFixture' receives an injected context")]
    public void ScreenplayContext_should_be_injected_by_ScreenplayFixture_attribute()
    {
      // Assert
      Assert.That(context, Is.Not.Null);
    }

    public NUnitIntegrationTests(ScreenplayContext context)
    {
      if(context == null)
        throw new ArgumentNullException(nameof(context));
      this.context = context;
    }
  }
}
