using System;
using System.Net.Http;

namespace CSF.Screenplay.JsonApis
{
  public interface IProvidesInvocationDetails
  {
    HttpRequestMessage GetRequestMessage();

    TimeSpan? GetTimeout();
  }
}
