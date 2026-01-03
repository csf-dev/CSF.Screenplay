namespace CSF.Screenplay.WebApis;

[TestFixture,Parallelizable]
public class SendTheHttpRequestTests
{
    [Test,AutoMoqData]
    public void GetReportFragmentShouldReturnTheCorrectString(Actor actor,
                                                              IFormatsReportFragment formatter,
                                                              [Frozen] HttpRequestMessageBuilder builder,
                                                              SendTheHttpRequest sut,
                                                              ReportFragment report)
    {
        Mock.Get(formatter)
            .Setup(x => x.Format("{Actor} sends an HTTP {Method} request to {Builder}", actor, builder.Method, builder))
            .Returns(report);

        Assert.That(sut.GetReportFragment(actor, formatter), Is.SameAs(report));
    }
}