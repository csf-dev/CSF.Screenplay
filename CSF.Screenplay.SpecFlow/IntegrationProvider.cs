using System;
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
      var screenplayConfig = GetScreenplayConfiguration();
      if(screenplayConfig == null)
        throw new InvalidOperationException("The SpecFlow/Screenplay configuration must be provided."); 

      var integrationConfigType = screenplayConfig.GetIntegrationConfigType();
      return new IntegrationFactory().Create(integrationConfigType);
    }

    IScreenplayConfiguration GetScreenplayConfiguration()
    {
      var reader = new Configuration.ConfigurationReader();
      return reader.ReadSection<SpecFlowScreenplayConfiguration>();
    }
  }
}
