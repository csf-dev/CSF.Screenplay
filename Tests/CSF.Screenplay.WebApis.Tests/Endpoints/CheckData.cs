using System;
using System.Net.Http;

namespace CSF.Screenplay.WebApis.Tests.Endpoints
{
  public class CheckData : RelativeEndpoint
  {
    public override string Name => "the data-checking service";

    public override HttpMethod HttpMethod => HttpMethod.Post;

    protected override string GetRelativeUriString() => "Execution/CheckData";
  }
}
