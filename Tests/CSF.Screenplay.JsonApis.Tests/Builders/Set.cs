using System;
using CSF.Screenplay.JsonApis.Tests.Services;
using CSF.Screenplay.WebTestWebsite.Models;

namespace CSF.Screenplay.JsonApis.Tests.Builders
{
  public static class Set
  {
    public static JsonServiceDescription TheNumberTo(int number)
    => new SetNumberService(new SampleApiData { NewNumber = number });
  }
}
