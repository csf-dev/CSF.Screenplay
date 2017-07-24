using System;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal.Commands;

namespace CSF.Screenplay.Web.Tests
{
  public class ReportableCommand : BeforeAndAfterTestCommand
  {
    public ReportableCommand(TestCommand innerCommand) : base(innerCommand)
    {
      this.BeforeTest = ctx => {
        WebdriverTestSetup.Reporter.BeginNewScenario(ctx.CurrentTest.FullName);
      };

      this.AfterTest = ctx => {
        WebdriverTestSetup.Reporter.CompleteScenario(ctx.CurrentResult.ResultState.Status == TestStatus.Passed);
      };
    }
  }
}
