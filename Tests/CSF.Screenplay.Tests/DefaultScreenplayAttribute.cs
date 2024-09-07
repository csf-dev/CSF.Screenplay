using System.Reflection;
using AutoFixture;

namespace CSF.Screenplay;

public class DefaultScreenplayAttribute : CustomizeAttribute
{
    public override ICustomization GetCustomization(ParameterInfo parameter)
        => new DefaultScreenplayCustomization();
}
