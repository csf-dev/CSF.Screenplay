using System;
using System.Net.Http;

namespace CSF.Screenplay.WebApis.Tests.Endpoints
{
  public class SlowlyGetData : RelativeEndpoint
  {
    readonly string name;

    public override string Name => $"the data-retrieval service for {name} which operates slowly";

    public override HttpMethod HttpMethod => HttpMethod.Get;

    protected override string GetRelativeUriString() => $"Data/slow-sample-data/{name}";

    public SlowlyGetData(string name) : base()
    {
      if(name == null)
        throw new ArgumentNullException(nameof(name));

      this.name = name;
    }

    public static SlowlyGetData For(string name) => new SlowlyGetData(name);
  }
}
