using System;
using CSF.Screenplay.JsonApis.Actions;

namespace CSF.Screenplay.JsonApis.Builders
{
  /// <summary>
  /// Static builder type which assists in the creation of JSON API performables.
  /// </summary>
  public static class Execute
  {
    /// <summary>
    /// Creates and returns an <see cref="ExecuteApi"/> instance from the given service description.
    /// </summary>
    /// <returns>The JSON API action.</returns>
    /// <param name="invocationDetails">Details which describe the service to be executed.</param>
    public static ExecuteApi AJsonApi(JsonServiceDescription invocationDetails)
      => new ExecuteApi(invocationDetails);

    /// <summary>
    /// Creates and returns an <see cref="T:GetApiResult{TResult}"/> instance from the given service description.
    /// </summary>
    /// <returns>The JSON API question.</returns>
    /// <param name="invocationDetails">Details which describe the service to be executed.</param>
    /// <typeparam name="TResult">The expected return/response type of the API service.</typeparam>
    public static GetApiResult<TResult> AJsonApiAndGetTheResult<TResult>(JsonServiceDescription<TResult> invocationDetails)
      => new GetApiResult<TResult>(invocationDetails);
  }
}
