using System;
using System.Collections.Generic;

namespace CSF.Screenplay.Scenarios
{
  public interface IServiceRegistry : ISingletonServiceResolverFactory
  {
    IReadOnlyCollection<IServiceRegistration> Registrations { get; }
  }
}
