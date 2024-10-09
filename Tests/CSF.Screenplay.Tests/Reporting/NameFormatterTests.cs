namespace CSF.Screenplay.Reporting;

[TestFixture,Parallelizable]
public class NameFormatterTests
{
    [Test,AutoMoqData]
    public void CanFormatShouldReturnFalseForAnObjectWhichCannotBeFormatted(NameFormatter sut, object value)
    {
        Assert.That(sut.CanFormat(value), Is.False);
    }

    [Test,AutoMoqData]
    public void CanFormatShouldReturnTrueForAnObjectWhichCannotBeFormatted(NameFormatter sut, IHasName value)
    {
        Assert.That(sut.CanFormat(value), Is.True);
    }

    [Test,AutoMoqData]
    public void FormatShouldReturnACorrectlyFormattedString(NameFormatter sut, IHasName value, string name)
    {
        Mock.Get(value).SetupGet(x => x.Name).Returns(name);
        Assert.That(sut.Format(value), Is.EqualTo(name));
    }
}