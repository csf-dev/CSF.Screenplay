using System.IO;
using AutoFixture;
using AutoFixture.Kernel;

namespace CSF.Screenplay.Reporting;

public class WithMemoryStreamCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customizations.Add(new TypeRelay(
            typeof(Stream),
            typeof(MemoryStream)));
        fixture.Customize<MemoryStream>(c => c.FromFactory(() => new MemoryStream()).OmitAutoProperties());
    }
}
