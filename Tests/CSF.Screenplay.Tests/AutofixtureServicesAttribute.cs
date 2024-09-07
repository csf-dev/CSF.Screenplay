using System.Reflection;
using AutoFixture;

namespace CSF.Screenplay;

/// <summary>
/// AutoFixture customization attribute which creates an <see cref="IServiceProvider"/> that
/// gets services from AutoFixture.
/// </summary>
public class AutofixtureServicesAttribute : CustomizeAttribute
{
    public override ICustomization GetCustomization(ParameterInfo parameter)
        => new AutofixtureServicesCustomization();
}