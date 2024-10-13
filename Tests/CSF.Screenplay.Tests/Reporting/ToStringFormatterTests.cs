namespace CSF.Screenplay.Reporting;

[TestFixture,Parallelizable]
public class ToStringFormatterTests
{
    [Test,AutoMoqData]
    public void CanFormatShouldReturnTrueForAPlainObject(ToStringFormatter sut, object value)
    {
        Assert.That(sut.CanFormat(value), Is.True);
    }

    [Test,AutoMoqData]
    public void CanFormatShouldReturnTrueForNull(ToStringFormatter sut)
    {
        Assert.That(sut.CanFormat(null), Is.True);
    }

    [Test,AutoMoqData]
    public void FormatShouldReturnACorrectlyFormattedStringForAnObject(ToStringFormatter sut, object value)
    {
        Assert.That(sut.FormatForReport(value), Is.EqualTo(typeof(object).FullName));
    }

    [Test,AutoMoqData]
    public void FormatShouldReturnACorrectlyFormattedStringForNull(ToStringFormatter sut)
    {
        Assert.That(sut.FormatForReport(null), Is.EqualTo("<null>"));
    }
}