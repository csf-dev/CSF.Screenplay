using System;
using System.Net.Http;
using CSF.Screenplay.WebTestWebsite.Models;

namespace CSF.Screenplay.WebApis.Tests.Endpoints
{
  public class SetNumber : RelativeEndpoint
  {
    public override string Name => "the number-setting service";

    public override HttpMethod HttpMethod => HttpMethod.Post;

    protected override string GetRelativeUriString() => $"Execution/SetMyNumber";
  }
}
