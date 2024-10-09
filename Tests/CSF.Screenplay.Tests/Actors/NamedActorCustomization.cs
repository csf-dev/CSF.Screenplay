using AutoFixture;

namespace CSF.Screenplay.Actors;

public class NamedActorCustomization(string name) : ICustomization
{
    readonly string name = name ?? throw new ArgumentNullException(nameof(name));

    public void Customize(IFixture fixture)
    {
        fixture.Customize<Actor>(c => c.FromFactory((Guid guid) => new Actor(name, guid)));
    }
}
