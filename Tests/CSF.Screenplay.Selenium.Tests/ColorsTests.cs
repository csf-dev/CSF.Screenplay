namespace CSF.Screenplay.Selenium;

[TestFixture, Parallelizable]
public class ColorsTests
{
    [TestCase("GREEN", 0, 128, 0)]
    [TestCase("green", 0, 128, 0)]
    [TestCase("gReEn", 0, 128, 0)]
    public void GetNamedColorShouldReturnAColorIfTheNameIsValid(string colorName, byte r, byte g, byte b)
    {
        Assert.That(() => Colors.GetNamedColor(colorName), Is.EqualTo(new Color(r, g, b)));
    }

    [Test]
    public void GetNamedColorShouldThrowAneIfTheNameIsNull()
    {
        Assert.That(() => Colors.GetNamedColor(null), Throws.ArgumentNullException);
    }

    [Test]
    public void GetNamedColorShouldThrowArgExIfTheNameIsInvalid()
    {
        Assert.That(() => Colors.GetNamedColor("ElephantsAreNotAColor"), Throws.ArgumentException);
    }
}