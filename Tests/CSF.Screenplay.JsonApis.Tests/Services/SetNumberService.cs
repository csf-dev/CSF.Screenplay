using System;
namespace CSF.Screenplay.JsonApis.Tests.Services
{
  public class SetNumberService : JsonServiceDescription
  {
    protected override string GetUriString() => $"SetMyNumber";

    public SetNumberService(int number) : base(requestPayload: number) {}

    public static SetNumberService WithTheNumber(int number) => new SetNumberService(number);
  }
}
