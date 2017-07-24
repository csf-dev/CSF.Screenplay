using System;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal.Commands;

namespace CSF.Screenplay.Web.Tests
{
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
  public class ReportableAttribute : Attribute, IWrapSetUpTearDown
  {
    public TestCommand Wrap(TestCommand command)
    {
      return new ReportableCommand(command);
    }
  }
}
