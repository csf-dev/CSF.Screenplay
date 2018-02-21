using System;
using CSF.Screenplay.WebTestWebsite.Models;

namespace CSF.Screenplay.JsonApis.Tests.Services
{
  public class SetNumberService : JsonServiceDescription
  {
    protected override string GetRelativeUriString() => $"Execution/SetMyNumber";

    public SetNumberService(SampleApiData data) : base(requestPayload: data) {}
  }
}
