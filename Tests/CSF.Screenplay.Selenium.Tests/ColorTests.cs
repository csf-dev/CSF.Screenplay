using Microsoft.AspNetCore.Mvc;

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
    public void TryParseShouldReturnAMatchingColorObject(string colorString, byte red, byte green, byte blue, double alpha)
    {
        var result = Color.TryParse(colorString, out var color);
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.True, "Parsing success");
            Assert.That(color, Is.EqualTo(new Color(red, green, blue, alpha)), "Expected color matches");
        });
    }
}