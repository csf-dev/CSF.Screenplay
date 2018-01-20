using System;
using System.Net.Http;
using CSF.Screenplay.WebTestWebsite.Models;

namespace CSF.Screenplay.JsonApis.Tests.Services
{
  public class GetDataService : JsonServiceDescription<SampleApiData>
  {
    readonly string name;

    protected override HttpMethod GetHttpMethod() => HttpMethod.Get;

    protected override string GetUriString() => $"sample-data/{name}";

    public GetDataService(string name) : base()
    {
      if(name == null)
        throw new ArgumentNullException(nameof(name));

      this.name = name;
    }
  }
}
