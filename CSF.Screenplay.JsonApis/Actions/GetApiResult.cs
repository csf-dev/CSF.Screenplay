using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.JsonApis.Abilities;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay.JsonApis.Actions
{
  public class GetApiResult<T> : Question<T>
  {
    readonly IProvidesInvocationDetails service;

    protected override string GetReport(INamed actor)
      => $"{actor.Name} gets the result of {service.ToString()}";

    protected override T PerformAs(IPerformer actor)
    {
      if(actor == null)
        throw new ArgumentNullException(nameof(actor));

      var ability = actor.GetAbility<ConsumeJsonWebServices>();
      return ability.GetResult<T>(service);
    }

    public GetApiResult(IProvidesInvocationDetails service)
    {
      if(service == null)
        throw new ArgumentNullException(nameof(service));

      this.service = service;
    }
  }
}
