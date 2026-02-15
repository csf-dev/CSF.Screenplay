
using System;
using CSF.Screenplay.Selenium.Elements;
using OpenQA.Selenium;
using static CSF.Screenplay.PerformanceStarter;
using static CSF.Screenplay.Selenium.PerformableBuilder;

namespace CSF.Screenplay.Selenium.Actions;

[TestFixture, Parallelizable, Category("WebDriver")]
public class OpenUrlTests
{
    static readonly ITarget
        textContent = new ElementId("textContent", "the displayable text");

    static readonly NamedUri testPage = new NamedUri("OpenUrlTests.html", "the test page");

    [Test, Screenplay]
    public async Task OpenTheUrlShouldShouldYieldExpectedContent(IStage stage)
    {
        var webster = stage.Spotlight<Webster>();

        await Given(webster).WasAbleTo(OpenTheUrl(testPage));
        var contents = await Then(webster).Should(ReadFromTheElement(textContent).TheText());

        Assert.That(contents, Is.EqualTo("This is the page content."));
    }

    [Test, Screenplay]
    public async Task OpenTheUrlWithDifferentBasePathShouldYieldDifferentContent(IStage stage)
    {
        var pattie = stage.Spotlight<Pattie>();

        await Given(pattie).WasAbleTo(OpenTheUrl(testPage));
        var contents = await Then(pattie).Should(ReadFromTheElement(textContent).TheText());

        Assert.That(contents, Is.EqualTo("This is content at the deeper path."));
    }

    [Test, AutoMoqData]
    public void PerformAsAsyncShouldThrowIfTheUrlIsNotAbsolute(Actor actor,
                                                                     [MockDriver] BrowseTheWeb ability)
    {
        actor.IsAbleTo(ability);
        var sut = new OpenUrl(new NamedUri("foo/bar/baz.html"));
        Assert.That(async () => await sut.PerformAsAsync(actor), Throws.InstanceOf<InvalidOperationException>());
    }

    [Test, AutoMoqData]
    public void PerformAsAsyncShouldNotThrowIfTheUrlIsAbsolute(Actor actor,
                                                               [MockDriver] BrowseTheWeb ability)
    {
        actor.IsAbleTo(ability);
        var sut = new OpenUrl(new NamedUri("https://example.com/foo/bar/baz.html"));
        Assert.That(async () => await sut.PerformAsAsync(actor), Throws.Nothing);
    }
}