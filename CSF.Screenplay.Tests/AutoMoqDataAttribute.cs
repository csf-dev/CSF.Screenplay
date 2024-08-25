using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.NUnit3;

namespace CSF.Screenplay;

/// <summary>
/// Sets up a test to use Autofixture &amp; Moq together, via <see cref="AutoMoqCustomization"/>.
/// </summary>
public class AutoMoqDataAttribute : AutoDataAttribute
{
    public AutoMoqDataAttribute() : base(() => new Fixture().Customize(new AutoMoqCustomization())) {}
}