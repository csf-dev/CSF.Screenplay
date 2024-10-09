namespace CSF.Screenplay.WebApis;

[TestFixture,Parallelizable]
public class SendTheHttpRequestAndGetJsonResponseTests
{
    [Test,AutoMoqData]
    public void GetReportFragmentShouldReturnTheCorrectString(IHasName actor,
                                                              IFormatsReportFragment formatter,
                                                              [Frozen] HttpRequestMessageBuilder<string> builder,
                                                              SendTheHttpRequestAndGetJsonResponse<string> sut,
                                                              ReportFragment report)
    {
        Mock.Get(formatter)
            .Setup(x => x.Format("{Actor} sends a web request and deserializes the JSON response as {ResponseType}", actor, "String"))
            .Returns(report);

        Assert.That(sut.GetReportFragment(actor, formatter), Is.SameAs(report));
    }
}