namespace CSF.Screenplay.Reporting;

[TestFixture,Parallelizable]
public class FormattableFormatterTests
{
    [Test,AutoMoqData]
    public void CanFormatShouldReturnFalseForAnObjectWhichCannotBeFormatted(FormattableFormatter sut, object value)
    {
        Assert.That(sut.CanFormat(value), Is.False);
    }

    [Test,AutoMoqData]
    public void CanFormatShouldReturnTrueForAnObjectWhichCannotBeFormatted(FormattableFormatter sut, IFormattableValue value)
    {
        Assert.That(sut.CanFormat(value), Is.True);
    }

    [Test,AutoMoqData]
    public void FormatShouldReturnACorrectlyFormattedString(FormattableFormatter sut, IFormattableValue value, string format)
    {
        Mock.Get(value).Setup(x => x.FormatForReport()).Returns(format);
        Assert.That(sut.FormatForReport(value), Is.EqualTo(format));
    }
}