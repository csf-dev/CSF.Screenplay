using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace CSF.Screenplay.NUnit
{
  class BeforeAfterTestHelper
  {
    internal void BeforeScenario(ScreenplayContext context, ITest test)
    {
      BeginScenario(context, test);
      DismissCast(context);
    }

    internal void AfterScenario(ScreenplayContext context, ITest test)
    {
      EndScenario(context);
    }

    void BeginScenario(ScreenplayContext context, ITest test)
    {
      var adapter = new ScenarioAdapter(test);
      context.OnBeginScenario(adapter.ScenarioId, adapter.ScenarioName, adapter.FeatureId, adapter.FeatureName);
    }

    void EndScenario(ScreenplayContext context)
    {
      var result = TestContext.CurrentContext.Result;
      context.OnEndScenario(result.Outcome.Status == TestStatus.Passed);
    }

    void DismissCast(ScreenplayContext context)
    {
      var cast = context.GetCast();
      if(cast == null)
        return;

      cast.Dismiss();
    }
  }
}
