using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using CSF.Screenplay.Abilities;
using Newtonsoft.Json;

namespace CSF.Screenplay.JsonApis.Abilities
{
  public class ConsumeJsonWebServices : Ability
  {
    #region fields

    static readonly TimeSpan SystemDefaultTimeout = new TimeSpan(0, 0, 30);

    readonly TimeSpan defaultTimeout;
    readonly SynchronousJsonGateway jsonGateway;

    #endregion

    #region public API

    public virtual void Execute(IProvidesInvocationDetails invocationDetails)
      => jsonGateway.GetResponse(invocationDetails.GetRequestMessage(), GetTimeout(invocationDetails));

    public virtual T GetResult<T>(IProvidesInvocationDetails invocationDetails)
      => jsonGateway.GetResponse<T>(invocationDetails.GetRequestMessage(), GetTimeout(invocationDetails));

    #endregion

    #region private methods

    TimeSpan GetTimeout(IProvidesInvocationDetails invocationDetails)
      => invocationDetails.GetTimeout().GetValueOrDefault(defaultTimeout);

    #endregion

    #region boilerplate Ability overrides

    protected override string GetReport(Actors.INamed actor)
      => $"{actor.Name} can consume JSON web services.";

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);

      if(disposing)
        jsonGateway.Dispose();
    }

    #endregion

    #region constructor

    public ConsumeJsonWebServices(string baseUriString, TimeSpan? defaultTimeout = null)
      : this(new Uri(baseUriString, UriKind.Absolute), defaultTimeout) {}

    public ConsumeJsonWebServices(Uri baseUri = null, TimeSpan? defaultTimeout = null)
    {
      this.defaultTimeout = defaultTimeout.GetValueOrDefault(SystemDefaultTimeout);
      jsonGateway = new SynchronousJsonGateway(baseUri);
    }

    #endregion
  }
}
