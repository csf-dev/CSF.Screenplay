
Screenplay for SpecFlow : README
--------------------------------

The first step in setting up a Screenplay integration is to create a "Screenplay integration
config" class in your test project.  This class must also be referenced by a
ScreenplayAssembly attribute, which simply tells Screenplay where to find your config.

This integration config class is used to set up the optional components of Screenplay.  These
optional components might be:

* Reporting
* Additional abilities
* Dependency services

Here is a template/empty integration config class (including the required attribute) which you
may use as the basis for your own:

// -- START OF TEMPLATE --

[assembly: CSF.Screenplay.SpecFlow.ScreenplayAssembly(typeof(YourNamespace.ScreenplayIntegrationConfig))]
namespace YourNamespace
{
  public class ScreenplayIntegrationConfig : CSF.Screenplay.Integration.IIntegrationConfig
  {
    public void Configure(CSF.Screenplay.Integration.IIntegrationConfigBuilder builder)
    {

    }
  }
}

// -- END OF TEMPLATE --

For more information about what may be placed in the Configure method, please refer to:

  https://github.com/csf-dev/CSF.Screenplay/wiki/ConfiguringIntegrations