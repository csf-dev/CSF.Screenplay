using System;
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay.Integration
{
  public interface IServiceRegistrationProvider
  {
    ServiceRegistry GetServiceRegistry();
  }
}
