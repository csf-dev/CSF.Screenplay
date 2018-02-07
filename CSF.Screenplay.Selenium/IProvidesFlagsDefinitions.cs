using System;
using System.Collections.Generic;
using CSF.WebDriverExtras.Flags;

namespace CSF.Screenplay.Selenium
{
  public interface IProvidesFlagsDefinitions
  {
    IReadOnlyCollection<FlagsDefinition> GetFlagsDefinitions();
  }
}
