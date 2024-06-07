using System;
using System.Collections.Generic;
using System.Linq;
using CSF.FlexDi;
using CSF.FlexDi.Builders;

namespace CSF.Screenplay.Scenarios
{
  /// <summary>
  /// Extension methods for a container.
  /// </summary>
  public static class ContainerExtensions
  {
    /// <summary>
    /// Adds a collection of registrations to the container.
    /// </summary>
    /// <param name="container">Container.</param>
    /// <param name="registrations">Registrations.</param>
    public static void AddRegistrations(this IReceivesRegistrations container,
                                        IEnumerable<Action<IRegistrationHelper>> registrations)
    {
      if(container == null)
        throw new ArgumentNullException(nameof(container));
      var registrationsToAdd = registrations?? Enumerable.Empty<Action<IRegistrationHelper>>();

      container.AddRegistrations(helper => {
        foreach(var registrationAction in registrationsToAdd)
          registrationAction(helper);
      });
    }
  }
}
