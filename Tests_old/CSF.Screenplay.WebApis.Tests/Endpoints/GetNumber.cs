using System;
using System.Net.Http;

namespace CSF.Screenplay.WebApis.Tests.Endpoints
{
  public class GetNumber : RelativeEndpoint
  {
    public override string Name => "the number-retrieval service";

    public override HttpMethod HttpMethod => HttpMethod.Get;

    protected override string GetRelativeUriString() => $"Execution/GetMyNumber";
  }
}
