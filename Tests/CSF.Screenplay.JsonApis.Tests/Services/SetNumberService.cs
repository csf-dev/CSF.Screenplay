using System;
using CSF.Screenplay.WebTestWebsite.Models;

namespace CSF.Screenplay.JsonApis.Tests.Services
{
  public class SetNumberService : JsonServiceDescription
  {
    protected override string GetUriString() => $"Execution/SetMyNumber";

    public SetNumberService(SampleApiData data) : base(requestPayload: data) {}
  }
}
