using System;
using CSF.Screenplay.JsonApis.Tests.Services;

namespace CSF.Screenplay.JsonApis.Tests.Builders
{
  public static class Set
  {
    public static JsonServiceDescription TheNumberTo(int number) => new SetNumberService(number);
  }
}
