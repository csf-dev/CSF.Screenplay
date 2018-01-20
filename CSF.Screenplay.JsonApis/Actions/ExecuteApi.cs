using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.JsonApis.Abilities;
using CSF.Screenplay.Performables;

namespace CSF.Screenplay.JsonApis.Actions
{
  public class ExecuteApi : Performable
  {
    readonly IProvidesInvocationDetails service;

    protected override string GetReport(INamed actor)
      => $"{actor.Name} executes {service.ToString()}";

    protected override void PerformAs(IPerformer actor)
    {
      if(actor == null)
        throw new ArgumentNullException(nameof(actor));

      var ability = actor.GetAbility<ConsumeJsonWebServices>();
      ability.Execute(service);
    }

    public ExecuteApi(IProvidesInvocationDetails service)
    {
      if(service == null)
        throw new ArgumentNullException(nameof(service));

      this.service = service;
    }
  }
}
