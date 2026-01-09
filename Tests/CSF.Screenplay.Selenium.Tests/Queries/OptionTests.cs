using NUnit.Framework.Internal;

namespace CSF.Screenplay.Selenium.Queries;

[TestFixture, Parallelizable]
public class OptionTests
{
    [Test]
    public void EqualsShouldReturnTrueForSameOptions()
    {
        object option1 = new Option("value1", "Option 1");
        object option2 = new Option("value1", "Option 1");

        Assert.That(option1, Is.EqualTo(option2));
    }

    [Test]
    public void GetHashCodeShouldReturnSameResultForSameOptions()
    {
        var option1 = new Option("value1", "Option 1");
        var option2 = new Option("value1", "Option 1");

        Assert.That(option1.GetHashCode(), Is.EqualTo(option2.GetHashCode()));
    }

    [Test]
    public void EqualsShouldReturnFalseForDifferentOptions()
    {
        object option1 = new Option("value1", "Option 1");
        object option2 = new Option("value2", "Option 2");

        Assert.That(option1, Is.Not.EqualTo(option2));
    }

    [Test]
    public void GetHashCodeShouldReturnDifferentResultForDifferentOptions()
    {
        var option1 = new Option("value1", "Option 1");
        var option2 = new Option("value2", "Option 2");

        Assert.That(option1.GetHashCode(), Is.Not.EqualTo(option2.GetHashCode()));
    }
}