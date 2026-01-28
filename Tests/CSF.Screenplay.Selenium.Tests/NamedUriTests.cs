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
}