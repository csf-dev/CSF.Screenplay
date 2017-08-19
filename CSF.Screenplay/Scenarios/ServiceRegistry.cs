using System;
using System.Collections.Generic;
using System.Linq;

namespace CSF.Screenplay.Scenarios
{
  public class ServiceRegistry
  {
    readonly IReadOnlyCollection<IServiceRegistration> registrations;

    public ServiceResolver GetResolver()
    {
      return new ServiceResolver(registrations);
    }

    public ServiceRegistry(IReadOnlyCollection<IServiceRegistration> registrations)
    {
      if(registrations == null)
        throw new ArgumentNullException(nameof(registrations));

      this.registrations = registrations;
    }
  }
}
