using System;
using CSF.Screenplay.Selenium;

namespace CSF.Screenplay.Selenium;

[TestFixture, Parallelizable]
public class NamedUriTests
{
    [Test]
    public void RebaseToShouldReturnARebasedUriIfItIsNotAbsolute()
    {
        var sut = new NamedUri("test.html", "name");
        var rebased = sut.RebaseTo("https://example.com");
        Assert.That(rebased.Uri.ToString(), Is.EqualTo("https://example.com/test.html"));
    }

    [Test]
    public void RebaseToShouldNotAlterTheUriIfItIsAbsolute()
    {
        var sut = new NamedUri("https://foobar.example.com/test.html", "name");
        var rebased = sut.RebaseTo("https://example.com");
        Assert.That(rebased.Uri.ToString(), Is.EqualTo("https://foobar.example.com/test.html"));
    }

    [Test]
    public void RebaseToShouldReturnAUriWithTheSameName()
    {
        var sut = new NamedUri("test.html", "name");
        var rebased = sut.RebaseTo("https://example.com");
        Assert.That(rebased.Name, Is.EqualTo("name"));
    }

    [Test]
    public void RebaseToShouldThrowIfUriIsNull()
    {
        var sut = new NamedUri("test.html", "name");
        Assert.That(() => sut.RebaseTo(null), Throws.ArgumentNullException);
    }

    [Test]
    public void RebaseToShouldThrowIfUriIsNullString()
    {
        var sut = new NamedUri("test.html", "name");
        Assert.That(() => sut.RebaseTo((string?) null), Throws.ArgumentNullException);
    }

    [Test]
    public void ImplicitCastFromUriShouldCreateANamedUri()
    {
        NamedUri uri = new Uri("https://example.com/foo.html");
        Assert.That(uri.Uri.ToString(), Is.EqualTo("https://example.com/foo.html"));
    }

    [Test]
    public void ImplicitCastFromStringShouldCreateANamedUri()
    {
        NamedUri uri = "https://example.com/foo.html";
        Assert.That(uri.Uri.ToString(), Is.EqualTo("https://example.com/foo.html"));
    }
}