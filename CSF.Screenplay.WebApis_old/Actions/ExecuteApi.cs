using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.JsonApis.Abilities;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay.JsonApis.Actions
{
  /// <summary>
  /// A Screenplay action type used to execute a JSON API which either has no response, or where the response is not
  /// needed and may be discarded.
  /// </summary>
  public class ExecuteApi : Performable
  {
    readonly IProvidesInvocationDetails service;

    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    protected override string GetReport(INamed actor)
      => $"{actor.Name} executes {service.ToString()}";

    /// <summary>
    /// Performs this operation, as the given actor.
    /// </summary>
    /// <param name="actor">The actor performing this task.</param>
    protected override void PerformAs(IPerformer actor)
    {
      if(actor == null)
        throw new ArgumentNullException(nameof(actor));

      var ability = actor.GetAbility<ConsumeJsonWebServices>();
      ability.Execute(service);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.JsonApis.Actions.ExecuteApi"/> class.
    /// </summary>
    /// <param name="service">An object which describes the API to be executed.</param>
    public ExecuteApi(IProvidesInvocationDetails service)
    {
      if(service == null)
        throw new ArgumentNullException(nameof(service));

      this.service = service;
    }
  }
}
