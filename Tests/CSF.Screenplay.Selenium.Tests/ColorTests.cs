using System;

namespace CSF.Screenplay.Selenium;

[TestFixture, Parallelizable]
public class ColorTests
{
    [TestCase("rgb(1, 2, 3)", 1, 2, 3, 1)]
    [TestCase("rgb(10%, 20%, 30%)", 25, 51, 76, 1)]
    [TestCase("rgb(\t1,     2,        3)", 1, 2, 3, 1)]
    [TestCase("rgba(1, 2, 3, 0.5)", 1, 2, 3, 0.5D)]
    [TestCase("rgba(10%, 20%, 30%, 0.5)", 25, 51, 76, 0.5D)]
    [TestCase("#ff00a0", 255, 0, 160, 1)]
    [TestCase("#01Ff03", 1, 255, 3, 1)]
    [TestCase("#00FF33", 0, 255, 51, 1)]
    [TestCase("#0f3", 0, 255, 51, 1)]
    [TestCase("hsl(120, 100%, 25%)", 0, 128, 0, 1)]
    [TestCase("hsl(100, 0%, 50%)", 128, 128, 128, 1)]
    [TestCase("hsl(0, 100%, 50%)", 255, 0, 0, 1)]
    [TestCase("hsl(120, 100%, 50%)", 0, 255, 0, 1)]
    [TestCase("hsl(240, 100%, 50%)", 0, 0, 255, 1)]
    [TestCase("hsl(0, 0%, 100%)", 255, 255, 255, 1)]
    [TestCase("hsla(120, 100%, 25%, 1)", 0, 128, 0, 1)]
    [TestCase("hsla(100, 0%, 50%, 0.5)", 128, 128, 128, 0.5)]
    [TestCase("green", 0, 128, 0, 1)]
    [TestCase("gray", 128, 128, 128, 1)]
    [TestCase("transparent", 0, 0, 0, 0)]
    public void TryParseShouldReturnTrueAndExposeACorrespondingColorForAValidString(string colorString, byte red, byte green, byte blue, double alpha)
    {
        var result = Color.TryParse(colorString, out var color);
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.True, "Parsing success");
            Assert.That(color, Is.EqualTo(new Color(red, green, blue, alpha)), "Expected color matches");
        });
    }

    [TestCase("rgb(1, 2, 3)", 1, 2, 3, 1)]
    [TestCase("rgb(10%, 20%, 30%)", 25, 51, 76, 1)]
    [TestCase("rgb(\t1,     2,        3)", 1, 2, 3, 1)]
    [TestCase("rgba(1, 2, 3, 0.5)", 1, 2, 3, 0.5D)]
    [TestCase("rgba(10%, 20%, 30%, 0.5)", 25, 51, 76, 0.5D)]
    [TestCase("#ff00a0", 255, 0, 160, 1)]
    [TestCase("#01Ff03", 1, 255, 3, 1)]
    [TestCase("#00FF33", 0, 255, 51, 1)]
    [TestCase("#0f3", 0, 255, 51, 1)]
    [TestCase("hsl(120, 100%, 25%)", 0, 128, 0, 1)]
    [TestCase("hsl(100, 0%, 50%)", 128, 128, 128, 1)]
    [TestCase("hsl(0, 100%, 50%)", 255, 0, 0, 1)]
    [TestCase("hsl(120, 100%, 50%)", 0, 255, 0, 1)]
    [TestCase("hsl(240, 100%, 50%)", 0, 0, 255, 1)]
    [TestCase("hsl(0, 0%, 100%)", 255, 255, 255, 1)]
    [TestCase("hsla(120, 100%, 25%, 1)", 0, 128, 0, 1)]
    [TestCase("hsla(100, 0%, 50%, 0.5)", 128, 128, 128, 0.5)]
    [TestCase("green", 0, 128, 0, 1)]
    [TestCase("gray", 128, 128, 128, 1)]
    [TestCase("transparent", 0, 0, 0, 0)]
    public void ParseShouldReturnTheCorrespondingColor(string colorString, byte red, byte green, byte blue, double alpha)
    {
        Assert.That(() => Color.Parse(colorString), Is.EqualTo(new Color(red, green, blue, alpha)));
    }

    [TestCase("elephants")]
    [TestCase("!!!")]
    [TestCase("😒")]
    [TestCase(null)]
    [TestCase("")]
    [TestCase("#00z")]
    [TestCase("rgb(1, 5, _)")]
    public void TryParseShouldReturnFalseIfTheStringIsInvalid(string? invalidColor)
    {
        Assert.That(() => Color.TryParse(invalidColor, out _), Is.False);
    }

    [TestCase("elephants")]
    [TestCase("!!!")]
    [TestCase("😒")]
    [TestCase("")]
    [TestCase("#00z")]
    [TestCase("rgb(1, 5, _)")]
    public void ParseShouldThrowFormatExceptionIfTheStringIsInvalid(string invalidColor)
    {
        Assert.That(() => Color.Parse(invalidColor), Throws.InstanceOf<FormatException>());
    }

    [Test]
    public void ParseShouldThrowAneIfTheStringIsNull()
    {
        Assert.That(() => Color.Parse(null), Throws.ArgumentNullException);
    }

    [TestCase("#f00", "rgb(255, 0, 0)")]
    public void TwoColorsShouldBeEqualIfTheyAreRgbaEquivalent(string first, string second)
    {
        Assert.That(Color.Parse(first), Is.EqualTo(Color.Parse(second)));
    }

    [TestCase(255, 0, 0, "#FF0000")]
    public void AColorShouldBeEqualToAStringIfTheyDescribeEquivalentColors(byte r, byte g, byte b, string other)
    {
        Assert.That(new Color(r, g, b), Is.EqualTo(other));
    }

    [TestCase("#f00", "rgb(255, 0, 1)")]
    public void TwoColorsShouldBeNotEqualIfTheyAreNotRgbaEquivalent(string first, string second)
    {
        Assert.That(Color.Parse(first), Is.Not.EqualTo(Color.Parse(second)));
    }

    [TestCase(255, 0, 0, "#FF0001")]
    public void AColorShouldNotBeEqualToAStringIfTheyDescribeDifferentColors(byte r, byte g, byte b, string other)
    {
        Assert.That(new Color(r, g, b), Is.Not.EqualTo(other));
    }
    
    [TestCase(255, 0, 0, 255, 0, 0)]
    public void AColorShouldBeEqualToASystemColorIfTheyDescribeEquivalentColors(byte r, byte g, byte b, byte r2, byte g2, byte b2)
    {
        Assert.That(new Color(r, g, b), Is.EqualTo(System.Drawing.Color.FromArgb(r2, g2, b2)));
    }

    [TestCase(255, 0, 0, 255, 1, 0)]
    public void TwoColorsShouldBeNotEqualIfTheyAreNotRgbaEquivalent(byte r, byte g, byte b, byte r2, byte g2, byte b2)
    {
        Assert.That(new Color(r, g, b), Is.Not.EqualTo(System.Drawing.Color.FromArgb(r2, g2, b2)));
    }

    [TestCase(0, 255, 0)]
    public void FromSystemDrawingColourShouldReturnCorrectColorInstance(byte r, byte g, byte b)
    {
        Assert.That(Color.FromSystemDrawingColour(System.Drawing.Color.FromArgb(r, g, b)), Is.EqualTo(new Color(r, g, b)));
    }
    
    [TestCase("#f00", "rgb(255, 0, 0)")]
    public void OpEqualityShouldReturnTrueForTwoColorsIfTheyAreRgbaEquivalent(string first, string second)
    {
        Assert.That(() => Color.Parse(first) == Color.Parse(second), Is.True);
    }
    
    [TestCase("#f00", "rgb(0, 255, 0)")]
    public void OpEqualityShouldReturnFalseForTwoColorsIfTheyAreNotRgbaEquivalent(string first, string second)
    {
        Assert.That(() => Color.Parse(first) == Color.Parse(second), Is.False);
    }
        
    [TestCase("#f00", "rgb(0, 255, 0)")]
    public void OpInequalityShouldReturnTrueForTwoColorsIfTheyAreNotRgbaEquivalent(string first, string second)
    {
        Assert.That(() => Color.Parse(first) != Color.Parse(second), Is.True);
    }

    [TestCase("#f00", "rgb(255, 0, 0)")]
    public void OpEqualityShouldReturnTrueForAColorAndAStringIfTheyAreRgbaEquivalent(string first, string second)
    {
        Assert.That(() => Color.Parse(first) == second, Is.True);
    }
    
    [TestCase("#f00", "rgb(0, 255, 0)")]
    public void OpEqualityShouldReturnFalseForAColorAndAStringIfTheyAreNotRgbaEquivalent(string first, string second)
    {
        Assert.That(() => Color.Parse(first) == second, Is.False);
    }
    
    [TestCase("#f00", "rgb(0, 255, 0)")]
    public void OpInequalityShouldReturnTrueForAColorAndAStringIfTheyAreNotRgbaEquivalent(string first, string second)
    {
        Assert.That(() => Color.Parse(first) != second, Is.True);
    }

    [TestCase("#f00", 255, 0, 0)]
    public void OpEqualityShouldReturnTrueForAColorAndASysColorIfTheyAreRgbaEquivalent(string first, byte r, byte g, byte b)
    {
        Assert.That(() => Color.Parse(first) == System.Drawing.Color.FromArgb(r, g, b), Is.True);
    }
    
    [TestCase("#f00", 0, 255, 0)]
    public void OpEqualityShouldReturnFalseForAColorAndASysColorIfTheyAreNotRgbaEquivalent(string first, byte r, byte g, byte b)
    {
        Assert.That(() => Color.Parse(first) == System.Drawing.Color.FromArgb(r, g, b), Is.False);
    }
    
    [TestCase("#f00", 0, 255, 0)]
    public void OpInequalityShouldReturnTrueForAColorAndASysColorIfTheyAreNotRgbaEquivalent(string first, byte r, byte g, byte b)
    {
        Assert.That(() => Color.Parse(first) != System.Drawing.Color.FromArgb(r, g, b), Is.True);
    }

    [TestCase("rgb(30, 128, 55)")]
    public void ParseFollowedByToRgbStringShouldRoundtripTheColor(string colorString)
    {
        Assert.That(() => Color.Parse(colorString).ToRgbString(), Is.EqualTo(colorString));
    }

    [TestCase("rgba(30, 128, 55, 0.5)")]
    [TestCase("rgba(30, 128, 55, 1)")]
    [TestCase("rgba(0, 0, 0, 0)")]
    public void ParseFollowedByToRgbaStringShouldRoundtripTheColor(string colorString)
    {
        Assert.That(() => Color.Parse(colorString).ToRgbaString(), Is.EqualTo(colorString));
    }

    [TestCase("rgba(30, 128, 55, 0.5)")]
    [TestCase("rgba(30, 128, 55, 1)")]
    [TestCase("rgba(0, 0, 0, 0)")]
    public void ParseFollowedByToStringShouldRoundtripTheColor(string colorString)
    {
        Assert.That(() => Color.Parse(colorString).ToString(), Is.EqualTo(colorString));
    }
    [TestCase("#E055D8")]
    public void ParseFollowedByToHexStringShouldRoundtripTheColor(string colorString)
    {
        Assert.That(() => Color.Parse(colorString).ToHexString(), Is.EqualTo(colorString));
    }

    [TestCase("rgb(1, 2, 3)", 1, 2, 3, 1)]
    [TestCase("rgb(10%, 20%, 30%)", 25, 51, 76, 1)]
    [TestCase("rgb(\t1,     2,        3)", 1, 2, 3, 1)]
    [TestCase("rgba(1, 2, 3, 0.5)", 1, 2, 3, 0.5D)]
    [TestCase("rgba(10%, 20%, 30%, 0.5)", 25, 51, 76, 0.5D)]
    [TestCase("#ff00a0", 255, 0, 160, 1)]
    [TestCase("#01Ff03", 1, 255, 3, 1)]
    [TestCase("#00FF33", 0, 255, 51, 1)]
    [TestCase("#0f3", 0, 255, 51, 1)]
    [TestCase("hsl(120, 100%, 25%)", 0, 128, 0, 1)]
    [TestCase("hsl(100, 0%, 50%)", 128, 128, 128, 1)]
    [TestCase("hsl(0, 100%, 50%)", 255, 0, 0, 1)]
    [TestCase("hsl(120, 100%, 50%)", 0, 255, 0, 1)]
    [TestCase("hsl(240, 100%, 50%)", 0, 0, 255, 1)]
    [TestCase("hsl(0, 0%, 100%)", 255, 255, 255, 1)]
    [TestCase("hsla(120, 100%, 25%, 1)", 0, 128, 0, 1)]
    [TestCase("hsla(100, 0%, 50%, 0.5)", 128, 128, 128, 0.5)]
    [TestCase("green", 0, 128, 0, 1)]
    [TestCase("gray", 128, 128, 128, 1)]
    [TestCase("transparent", 0, 0, 0, 0)]
    public void GetHashCodeShouldReturnTheSameResultForEquivalentColors(string colorString, byte red, byte green, byte blue, double alpha)
    {
        Assert.That(() => Color.Parse(colorString).GetHashCode(), Is.EqualTo(new Color(red, green, blue, alpha).GetHashCode()));
    }
}