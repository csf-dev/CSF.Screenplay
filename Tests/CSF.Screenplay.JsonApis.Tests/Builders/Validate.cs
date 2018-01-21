using System;
using CSF.Screenplay.JsonApis.Tests.Services;
using CSF.Screenplay.WebTestWebsite.Models;

namespace CSF.Screenplay.JsonApis.Tests.Builders
{
  public static class Validate
  {
    public static JsonServiceDescription TheData(SampleApiData data) => new CheckDataService(data);
  }
}
