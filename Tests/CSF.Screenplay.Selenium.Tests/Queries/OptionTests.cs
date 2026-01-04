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

        Assert.That(option1.Equals(option2), Is.True);
    }

    [Test]
    public void GetHashCodeShouldReturnSameResultForSameOptions()
    {
        var option1 = new Option("value1", "Option 1");
        var option2 = new Option("value1", "Option 1");

        Assert.That(option1.GetHashCode().Equals(option2.GetHashCode()), Is.True);
    }

    [Test]
    public void EqualsShouldReturnFalseForDifferentOptions()
    {
        object option1 = new Option("value1", "Option 1");
        object option2 = new Option("value2", "Option 2");

        Assert.That(option1.Equals(option2), Is.False);
    }

    [Test]
    public void GetHashCodeShouldReturnDifferentResultForDifferentOptions()
    {
        var option1 = new Option("value1", "Option 1");
        var option2 = new Option("value2", "Option 2");

        Assert.That(option1.GetHashCode().Equals(option2.GetHashCode()), Is.False);
    }
}