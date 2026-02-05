using AutoFixture;
using Microsoft.Extensions.DependencyInjection;

namespace CSF.Screenplay;

public class DefaultScreenplayCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<Screenplay>(c => c.FromFactory(() => Screenplay.Create(s => s.Configure<ScreenplayOptions>(o => o.ReportPath = null))));
    }
}