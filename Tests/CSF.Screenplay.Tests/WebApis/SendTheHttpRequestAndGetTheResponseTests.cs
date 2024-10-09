namespace CSF.Screenplay.WebApis;

[TestFixture,Parallelizable]
public class SendTheHttpRequestAndGetTheResponseTests
{
    [Test,AutoMoqData]
    public void GetReportFragmentShouldReturnTheCorrectString(IHasName actor,
                                                              IFormatsReportFragment formatter,
                                                              [Frozen] HttpRequestMessageBuilder<string> builder,
                                                              SendTheHttpRequestAndGetTheResponse<string> sut,
                                                              ReportFragment report)
    {
        Mock.Get(formatter)
            .Setup(x => x.Format("{Actor} sends an HTTP {Method} request to {Builder}, expecting the response to be {ResponseType}", actor, builder.Method, builder, "String"))
            .Returns(report);

        Assert.That(sut.GetReportFragment(actor, formatter), Is.SameAs(report));
    }
}