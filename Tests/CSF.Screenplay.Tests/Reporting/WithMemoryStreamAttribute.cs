using System.Reflection;
using AutoFixture;

namespace CSF.Screenplay.Reporting;

public class WithMemoryStreamAttribute : CustomizeAttribute
{
    public override ICustomization GetCustomization(ParameterInfo parameter) => new WithMemoryStreamCustomization();
}
