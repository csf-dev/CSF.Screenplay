using System.Reflection;
using AutoFixture;

namespace CSF.Screenplay.Actors;

public class NamedActorAttribute(string name) : CustomizeAttribute
{
    readonly string name = name ?? throw new ArgumentNullException(nameof(name));

    public override ICustomization GetCustomization(ParameterInfo parameter) => new NamedActorCustomization(name);
}