using System.Collections;
using System.Reflection;
using AutoFixture;

namespace CSF.Screenplay.Reporting;

[TestFixture,Parallelizable]
public class ValueFormatterRegistryTests
{
    [Test,AutoMoqData]
    public void IndexerGetShouldReturnExpectedValue([DefaultRegistry] ValueFormatterRegistry sut)
    {
        Assert.That(() => sut[0], Is.EqualTo(typeof(SampleFormatter1)));
    }

    [Test,AutoMoqData]
    public void IndexerSetShouldReplaceAnItem([DefaultRegistry] ValueFormatterRegistry sut)
    {
        sut[0] = typeof(SampleFormatter2);
        Assert.That(() => sut[0], Is.EqualTo(typeof(SampleFormatter2)));
    }

    [Test,AutoMoqData]
    public void IndexerSetShouldThrowIfItIsAnInvalidType([DefaultRegistry] ValueFormatterRegistry sut)
    {
        Assert.That(() => sut[0] = typeof(string), Throws.ArgumentException);
    }

    [Test,AutoMoqData]
    public void CountSouldReturnCountFromWrapped([DefaultRegistry] ValueFormatterRegistry sut)
    {
        Assert.That(sut, Has.Count.EqualTo(1));
    }

    [Test,AutoMoqData]
    public void IsReadOnlyShouldReturnIsReadOnlyFromWrapped([DefaultRegistry] ValueFormatterRegistry sut)
    {
        Assert.That(sut.IsReadOnly, Is.False);
    }

    [Test,AutoMoqData]
    public void AddShouldAddAnItem([DefaultRegistry] ValueFormatterRegistry sut)
    {
        sut.Add(typeof(SampleFormatter2));
        Assert.That(sut[1], Is.EqualTo(typeof(SampleFormatter2)));
    }

    [Test,AutoMoqData]
    public void AddShouldThrowIfNull([DefaultRegistry] ValueFormatterRegistry sut)
    {
        Assert.That(() => sut.Add(null), Throws.ArgumentNullException);
    }

    [Test,AutoMoqData]
    public void ClearShouldEmptyTheCollection([DefaultRegistry] ValueFormatterRegistry sut)
    {
        sut.Clear();
        Assert.That(sut, Has.Count.Zero);
    }

    [Test,AutoMoqData]
    public void ContainsShouldReturnTrueIfTheTypeIsContained([DefaultRegistry] ValueFormatterRegistry sut)
    {
        Assert.That(() => sut.Contains(typeof(SampleFormatter1)), Is.True);
    }

    [Test,AutoMoqData]
    public void CopyToShouldCopyToAnArray([DefaultRegistry] ValueFormatterRegistry sut)
    {
        var array = new Type[1];
        sut.CopyTo(array, 0);
        Assert.That(array, Is.EqualTo(new [] { typeof(SampleFormatter1) }));
    }

    [Test,AutoMoqData]
    public void GetEnumeratorShouldNotReturnNull([DefaultRegistry] ValueFormatterRegistry sut)
    {
        Assert.That(() => sut.GetEnumerator(), Is.Not.Null);
    }

    [Test,AutoMoqData]
    public void IndexOfShouldReturnResultForItemThatIsContained([DefaultRegistry] ValueFormatterRegistry sut)
    {
        Assert.That(() => sut.IndexOf(typeof(SampleFormatter1)), Is.Zero);
    }

    [Test,AutoMoqData]
    public void InsertShouldBeAbleToAddToTheBeginningOfTheCollection([DefaultRegistry] ValueFormatterRegistry sut)
    {
        sut.Insert(0, typeof(SampleFormatter2));
        Assert.That(sut, Is.EqualTo(new[] { typeof(SampleFormatter2), typeof(SampleFormatter1) }));
    }

    [Test,AutoMoqData]
    public void RemoveShouldRemoveAnItem([DefaultRegistry] ValueFormatterRegistry sut)
    {
        sut.Remove(typeof(SampleFormatter1));
        Assert.That(sut, Has.Count.Zero);
    }

    [Test,AutoMoqData]
    public void RemoveAtShouldRemoveAnItem([DefaultRegistry] ValueFormatterRegistry sut)
    {
        sut.RemoveAt(0);
        Assert.That(sut, Has.Count.Zero);
    }

    [Test,AutoMoqData]
    public void NonGenericGetEnumeratorShouldNotReturnNull([DefaultRegistry] ValueFormatterRegistry sut)
    {
        Assert.That(() => ((IEnumerable) sut).GetEnumerator(), Is.Not.Null);
    }
}

public class DefaultRegistryAttribute : CustomizeAttribute
{
    public override ICustomization GetCustomization(ParameterInfo parameter) => new DefaultRegistryCustomization();
}

public class DefaultRegistryCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<ValueFormatterRegistry>(x => {
            return x
                .OmitAutoProperties()
                .Do(r =>
                {
                    r.Add(typeof(SampleFormatter1));
                });
        });
    }
}

public class SampleFormatter1 : IValueFormatter
{
    public bool CanFormat(object value) => false;

    public string Format(object value) => string.Empty;
}

public class SampleFormatter2 : IValueFormatter
{
    public bool CanFormat(object value) => false;

    public string Format(object value) => string.Empty;
}