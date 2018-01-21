using System;
using CSF.Screenplay.JsonApis.Tests.Services;
using CSF.Screenplay.WebTestWebsite.Models;

namespace CSF.Screenplay.JsonApis.Tests.Builders
{
  public static class Get
  {
    public static JsonServiceDescription<int> TheNumber() => new GetNumberService();
    public static JsonServiceDescription<SampleApiData> TheSampleDataFor(string name) => new GetDataService(name);
    public static JsonServiceDescription<SampleApiData> TheSampleDataSlowlyFor(string name) => new SlowlyGetDataService(name);
    public static JsonServiceDescription<SampleApiData> TheSampleDataSlowlyFor(string name, TimeSpan timeout)
      => new SlowlyGetDataService(name, timeout);
  }
}
