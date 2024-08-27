using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.JsonApis.Abilities;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay.JsonApis.Actions
{
  /// <summary>
  /// A Screenplay question type used to execute a JSON API and get its response.
  /// </summary>
  public class GetApiResult<T> : Question<T>
  {
    readonly IProvidesInvocationDetails service;

    /// <summary>
    /// Gets the report of the current instance, for the given actor.
    /// </summary>
    /// <returns>The human-readable report text.</returns>
    /// <param name="actor">An actor for whom to write the report.</param>
    protected override string GetReport(INamed actor)
      => $"{actor.Name} gets the result of {service.ToString()}";

    /// <summary>
    /// Performs this operation, as the given actor.
    /// </summary>
    /// <returns>The response or result.</returns>
    /// <param name="actor">The actor performing this task.</param>
    protected override T PerformAs(IPerformer actor)
    {
      if(actor == null)
        throw new ArgumentNullException(nameof(actor));

      var ability = actor.GetAbility<ConsumeJsonWebServices>();
      return ability.GetResult<T>(service);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.JsonApis.Actions.GetApiResult`1"/> class.
    /// </summary>
    /// <param name="service">An object which describes the API to be executed.</param>
    public GetApiResult(IProvidesInvocationDetails service)
    {
      if(service == null)
        throw new ArgumentNullException(nameof(service));

      this.service = service;
    }
  }
}
