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
    static object syncRoot;
    static IScreenplayIntegration integration;

    /// <summary>
    /// Gets the integration from a given NUnit test method.
    /// </summary>
    /// <returns>The integration.</returns>
    /// <param name="method">Method.</param>
    public IScreenplayIntegration GetIntegration(IMethodInfo method)
    {
      lock(syncRoot)
      {
        if(integration == null)
        {
          var assembly = method?.MethodInfo?.DeclaringType?.Assembly;
          if(assembly == null)
          {
            throw new ArgumentException(Resources.ExceptionFormats.MethodMustHaveAnAssembly,
                                        nameof(method));
          }

          var assemblyAttrib = assembly.GetCustomAttribute<ScreenplayAssemblyAttribute>();
          if(assemblyAttrib == null)
          {
            var message = String.Format(Resources.ExceptionFormats.AssemblyMustBeDecoratedWithScreenplay,
                                        nameof(ScreenplayAttribute),
                                        nameof(ScreenplayAssemblyAttribute));
            throw new InvalidOperationException(message);
          }

          integration = assemblyAttrib.Integration;
        }
      }

      return integration;
    }

    /// <summary>
    /// Gets the integration from a given NUnit test instance.
    /// </summary>
    /// <returns>The integration.</returns>
    /// <param name="test">Test.</param>
    public IScreenplayIntegration GetIntegration(ITest test)
    {
      if(test.Method == null)
        throw new ArgumentException(Resources.ExceptionFormats.TestMustHaveAMethod, nameof(test));

      return GetIntegration(test.Method);
    }

    /// <summary>
    /// Initializes the <see cref="T:CSF.Screenplay.NUnit.IntegrationReader"/> class.
    /// </summary>
    static IntegrationReader()
    {
      syncRoot = new object();
    }
  }
}
