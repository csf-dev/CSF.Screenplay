using System;
using NUnit.Framework.Internal.Commands;

namespace CSF.Screenplay.Web.Tests
{
  public class ReportableCommand : BeforeTestCommand
  {
    public ReportableCommand(TestCommand innerCommand) : base(innerCommand)
    {
      this.BeforeTest = ctx => {
        WebdriverTestSetup.Reporter.BeginNewScenario(ctx.CurrentTest.FullName);
      };
    }
  }
}
