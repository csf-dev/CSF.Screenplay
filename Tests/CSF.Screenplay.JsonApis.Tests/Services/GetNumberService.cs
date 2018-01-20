using System;
using System.Net.Http;

namespace CSF.Screenplay.JsonApis.Tests.Services
{
  public class GetNumberService : JsonServiceDescription<int>
  {
    protected override HttpMethod GetHttpMethod() => HttpMethod.Get;

    protected override string GetUriString() => $"GetMyNumber";
  }
}
