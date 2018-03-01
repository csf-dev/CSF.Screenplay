using System;
using System.Net.Http;

namespace CSF.Screenplay.WebApis.Tests.Endpoints
{
  public class GetData  : RelativeEndpoint
  {
    readonly string name;

    public override string Name => $"the data-retrieval service for {name}";

    public override HttpMethod HttpMethod => HttpMethod.Get;

    protected override string GetRelativeUriString() => $"Data/sample-data/{name}";

    public GetData(string name) : base()
    {
      if(name == null)
        throw new ArgumentNullException(nameof(name));

      this.name = name;
    }

    public static GetData For(string name) => new GetData(name);
  }
}
