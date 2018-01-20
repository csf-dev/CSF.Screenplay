using System;
using CSF.Screenplay.WebTestWebsite.Models;

namespace CSF.Screenplay.JsonApis.Tests.Services
{
  public class SlowlyGetDataService : JsonServiceDescription<SampleApiData>
  {
    readonly string name;

    protected override string GetUriString() => $"Data/slow-sample-data/{name}";

    public SlowlyGetDataService(string name, TimeSpan? timeout = null) : base(timeout: timeout)
    {
      if(name == null)
        throw new ArgumentNullException(nameof(name));

      this.name = name;
    }
  }
}
