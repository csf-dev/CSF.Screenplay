using System;
using CSF.Screenplay.JsonApis.Actions;

namespace CSF.Screenplay.JsonApis.Builders
{
  public static class Execute
  {
    public static ExecuteApi AJsonApi(JsonServiceDescription invocationDetails)
      => new ExecuteApi(invocationDetails);

    public static GetApiResult<TResult> AJsonApiAndGetTheResult<TResult>(JsonServiceDescription<TResult> invocationDetails)
      => new GetApiResult<TResult>(invocationDetails);
  }
}
