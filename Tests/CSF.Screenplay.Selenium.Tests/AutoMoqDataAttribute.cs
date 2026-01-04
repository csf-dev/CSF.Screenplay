using AutoFixture;
using AutoFixture.AutoMoq;

namespace CSF.Screenplay;

/// <summary>
/// Sets up a test to use Autofixture &amp; Moq together, via <see cref="AutoMoqCustomization"/>.
/// </summary>
public class AutoMoqDataAttribute : AutoDataAttribute
{
    public AutoMoqDataAttribute() : base(() => new Fixture().Customize(new AutoMoqCustomization())) {}
}