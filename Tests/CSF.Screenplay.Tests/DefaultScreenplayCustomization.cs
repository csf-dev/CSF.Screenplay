using AutoFixture;

namespace CSF.Screenplay;

public class DefaultScreenplayCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<Screenplay>(c => c.FromFactory(() => Screenplay.CreateDefault()));
    }
}