using System;
using CSF.Extensions.WebDriver;

namespace CSF.Screenplay;

/// <summary>
/// Webster is a Persona who can browse the web.
/// </summary>
/// <remarks>
/// <para>
/// The <xref href="PersonaGlossaryItem?text=Persona+glossary+item"/> has more information about Personas and how to use them.
/// </para>
/// </remarks>
/// <param name="webDriverFactory">An <see cref="IGetsWebDriver"/> web driver factory</param>
public class Webster(IGetsWebDriver webDriverFactory) : IPersona
{
    /// <inheritdoc/>
    public string Name => nameof(Webster);

    /// <inheritdoc/>
    public Actor GetActor(Guid performanceIdentity)
    {
        var webster = new Actor(Name, performanceIdentity);
        webster.IsAbleTo(BrowseTheWeb());
        webster.IsAbleTo(UseABaseUri());
        return webster;
    }

    BrowseTheWeb BrowseTheWeb() => new(webDriverFactory);

    static UseABaseUri UseABaseUri() => new(TestRunSetup.SampleAppUri);
}