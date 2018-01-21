using System;
using System.Net.Http;
using CSF.Screenplay.WebTestWebsite.Models;

namespace CSF.Screenplay.JsonApis.Tests.Services
{
  public class CheckDataService : JsonServiceDescription
  {
    protected override string GetUriString() => "Execution/CheckData";

    public CheckDataService(SampleApiData data) : base(requestPayload: data) {}
  }
}
