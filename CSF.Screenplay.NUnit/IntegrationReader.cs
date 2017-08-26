using System;
using System.Reflection;
using CSF.Screenplay.Integration;
using NUnit.Framework.Interfaces;

namespace CSF.Screenplay.NUnit
{
  /// <summary>
  /// Helper type which provides access to the current Screenplay integration.
  /// </summary>
  public class IntegrationReader
  {
    IScreenplayIntegration cachedIntegration;

    /// <summary>
    /// Gets the integration from a given NUnit test method.
    /// </summary>
    /// <returns>The integration.</returns>
    /// <param name="method">Method.</param>
    public IScreenplayIntegration GetIntegration(IMethodInfo method)
    {
      if(cachedIntegration == null)
      {
        var assembly = method?.MethodInfo?.DeclaringType?.Assembly;
        if(assembly == null)
        {
          throw new ArgumentException($"The method must have an associated {nameof(Assembly)}.",
                                      nameof(method));
        }

        var assemblyAttrib = assembly.GetCustomAttribute<ScreenplayAssemblyAttribute>();
        if(assemblyAttrib == null)
        {
          var message = $"All test methods must be contained within assemblies which are " +
            $"decorated with `{nameof(ScreenplayAssemblyAttribute)}'.";
          throw new InvalidOperationException(message);
        }

        cachedIntegration = assemblyAttrib.Integration;
      }

      return cachedIntegration;
    }

    /// <summary>
    /// Gets the integration from a given NUnit test instance.
    /// </summary>
    /// <returns>The integration.</returns>
    /// <param name="test">Test.</param>
    public IScreenplayIntegration GetIntegration(ITest test)
    {
      if(test.Method == null)
        throw new ArgumentException("The test must specify a method.", nameof(test));

      return GetIntegration(test.Method);
    }
  }
}
