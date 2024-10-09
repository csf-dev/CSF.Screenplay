namespace CSF.Screenplay.Reporting;

[TestFixture,Parallelizable,SetCulture("en-GB")]
public class HumanizerFormatterTests
{
    [Test,AutoMoqData]
    public void CanFormatShouldReturnTrueForADateTime(HumanizerFormatter sut, DateTime value)
    {
        Assert.That(sut.CanFormat(value), Is.True);
    }

    [Test,AutoMoqData]
    public void CanFormatShouldReturnTrueForANonNullNullableDateTime(HumanizerFormatter sut, DateTime value)
    {
        DateTime? nullableValue = value;
        Assert.That(sut.CanFormat(nullableValue), Is.True);
    }

    [Test,AutoMoqData]
    public void CanFormatShouldReturnTrueForATimeSpan(HumanizerFormatter sut, TimeSpan value)
    {
        Assert.That(sut.CanFormat(value), Is.True);
    }
    
    [Test,AutoMoqData]
    public void CanFormatShouldReturnTrueForANonNullNullableTimeSpan(HumanizerFormatter sut, TimeSpan value)
    {
        TimeSpan? nullableValue = value;
        Assert.That(sut.CanFormat(nullableValue), Is.True);
    }
    
    [Test,AutoMoqData]
    public void CanFormatShouldReturnTrueForAnEnumType(HumanizerFormatter sut)
    {
        Assert.That(sut.CanFormat(MyEnumType.ThisIsTheFirst), Is.True);
    }
    
    [Test,AutoMoqData]
    public void CanFormatShouldReturnTrueForAStringCollection(HumanizerFormatter sut)
    {
        Assert.That(sut.CanFormat(new [] { "Foo", "Bar", "Baz" }), Is.True);
    }

    [Test,AutoMoqData]
    public void CanFormatShouldReturnFalseForANullReference(HumanizerFormatter sut)
    {
        Assert.That(sut.CanFormat(null), Is.False);
    }

    [Test,AutoMoqData]
    public void CanFormatShouldReturnFalseForAPlainObject(HumanizerFormatter sut, object value)
    {
        Assert.That(sut.CanFormat(value), Is.False);
    }
    [Test,AutoMoqData]
    public void FormatShouldReturnAStringForADateTime(HumanizerFormatter sut, DateTime value)
    {
        Assert.That(sut.Format(value), Is.Not.Null);
    }

    [Test,AutoMoqData]
    public void FormatShouldReturnAStringForANonNullNullableDateTime(HumanizerFormatter sut, DateTime value)
    {
        DateTime? nullableValue = value;
        Assert.That(sut.Format(nullableValue), Is.Not.Null);
    }

    [Test,AutoMoqData]
    public void FormatShouldReturnAStringForATimeSpan(HumanizerFormatter sut, TimeSpan value)
    {
        Assert.That(sut.Format(value), Is.Not.Null);
    }
    
    [Test,AutoMoqData]
    public void FormatShouldReturnAStringForANonNullNullableTimeSpan(HumanizerFormatter sut, TimeSpan value)
    {
        TimeSpan? nullableValue = value;
        Assert.That(sut.Format(nullableValue), Is.Not.Null);
    }
    
    [Test,AutoMoqData]
    public void FormatShouldReturnAStringForAnEnumValue(HumanizerFormatter sut)
    {
        Assert.That(sut.Format(MyEnumType.ThisIsTheFirst), Is.EqualTo("This is the first"));
    }
    
    [Test,AutoMoqData]
    public void FormatShouldReturnAStringForAnEnumValueRespectingTheDescription(HumanizerFormatter sut)
    {
        Assert.That(sut.Format(MyEnumType.ThisIsTheThird), Is.EqualTo("This member has a custom description"));
    }
        
    [Test,AutoMoqData]
    public void FormatShouldReturnAStringForAStringCollection(HumanizerFormatter sut)
    {
        Assert.That(sut.Format(new [] { "Foo", "Bar", "Baz" }), Is.EqualTo("Foo, Bar, and Baz"));
    }

    [Test,AutoMoqData]
    public void FormatShouldThrowForAnUnsupportedValue(HumanizerFormatter sut, object value)
    {
        Assert.That(() => sut.Format(value), Throws.ArgumentException);
    }

    public enum MyEnumType
    {
        ThisIsTheFirst,
        ThisIsTheSecond,
        [System.ComponentModel.Description("This member has a custom description")]
        ThisIsTheThird
    }
}