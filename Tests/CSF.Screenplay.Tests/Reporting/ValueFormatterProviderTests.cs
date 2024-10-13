namespace CSF.Screenplay.Reporting;

[TestFixture,Parallelizable]
public class ValueFormatterProviderTests
{
    [Test,AutoMoqData]
    public void GetValueFormatterShouldReturnFirstFormatterWhichCanFormat([Frozen] Formatter1 formatter1,
                                                                          [Frozen] Formatter2 formatter2,
                                                                          [Frozen] Formatter3 formatter3,
                                                                          [AutofixtureServices, Frozen] IServiceProvider services,
                                                                          [Frozen] IFormatterRegistry registry,
                                                                          ValueFormatterProvider sut,
                                                                          object value)
    {
        Mock.Get(registry).SetupGet(x => x.Count).Returns(3);
        Mock.Get(registry).SetupGet(x => x[0]).Returns(typeof(Formatter3));
        Mock.Get(registry).SetupGet(x => x[1]).Returns(typeof(Formatter2));
        Mock.Get(registry).SetupGet(x => x[2]).Returns(typeof(Formatter1));

        var formatter = sut.GetValueFormatter(value);

        Assert.That(formatter, Is.SameAs(formatter2));
    }

    [Test,AutoMoqData]
    public void GetValueFormatterShouldThrowIfNoFormattersCanFormat([Frozen] Formatter1 formatter1,
                                                                    [AutofixtureServices, Frozen] IServiceProvider services,
                                                                    [Frozen] IFormatterRegistry registry,
                                                                    ValueFormatterProvider sut,
                                                                    object value)
    {
        Mock.Get(registry).SetupGet(x => x.Count).Returns(1);
        Mock.Get(registry).SetupGet(x => x[0]).Returns(typeof(Formatter1));

        Assert.That(() => sut.GetValueFormatter(value), Throws.InvalidOperationException);
    }

    public class Formatter1 : IValueFormatter
    {
        public bool CanFormat(object value) => false;

        public string FormatForReport(object value)
        {
            throw new NotImplementedException();
        }
    }

    public class Formatter2 : IValueFormatter
    {
        public bool CanFormat(object value) => true;

        public string FormatForReport(object value)
        {
            throw new NotImplementedException();
        }
    }

    public class Formatter3 : IValueFormatter
    {
        public bool CanFormat(object value) => true;

        public string FormatForReport(object value)
        {
            throw new NotImplementedException();
        }
    }
}