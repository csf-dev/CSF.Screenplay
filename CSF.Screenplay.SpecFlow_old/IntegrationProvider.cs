using System;
using System.Linq;
using System.Reflection;
using CSF.Screenplay.Integration;

namespace CSF.Screenplay.SpecFlow
{
  /// <summary>
  /// Helper which encapsulates the logic required to create a new <see cref="IScreenplayIntegration"/>.
  /// </summary>
  public class IntegrationProvider
  {
    /// <summary>
    /// Gets the integration (via lazy-initialisation).
    /// </summary>
    /// <returns>The integration.</returns>
    public Lazy<IScreenplayIntegration> GetIntegration()
    {
      return new Lazy<IScreenplayIntegration>(CreateIntegration);
    }

    IScreenplayIntegration CreateIntegration()
    {
      var integration = GetScreenplayAssemblyAttribute()?.Integration;

      if(integration == null)
      {
        var message = String.Format(Resources.ExceptionFormats.ScreenplayAssemblyAttributeRequired,
                                    nameof(ScreenplayAssemblyAttribute));
        throw new InvalidOperationException(message);
      }

      return integration;
    }

    ScreenplayAssemblyAttribute GetScreenplayAssemblyAttribute()
    {
      var appDomain = AppDomain.CurrentDomain;
      var assemblies = appDomain.GetAssemblies();

      return (from assembly in assemblies
              where !assembly.IsDynamic
              let attrib = assembly.GetCustomAttribute<ScreenplayAssemblyAttribute>()
              where attrib != null
              select attrib)
        .FirstOrDefault();
    }
  }
}
