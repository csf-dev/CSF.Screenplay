using CSF.Screenplay.Reporting;

namespace CSF.Screenplay.Abilities;

[TestFixture, Parallelizable]
public class GetAssetFilePathsTests
{
    [Test, AutoMoqData]
    public void GetAssetFilePathShouldReturnValueFromService([Frozen] IGetsAssetFilePath pathProvider,
                                                             GetAssetFilePaths sut,
                                                             string basePath,
                                                             string expected)
    {
        Mock.Get(pathProvider).Setup(x => x.GetAssetFilePath(basePath)).Returns(expected);
        Assert.That(() => sut.GetAssetFilePath(basePath), Is.EqualTo(expected));
    }

    [Test, AutoMoqData]
    public void GetReportFragmentShouldReturnValueFromFormatterUsingFormat(GetAssetFilePaths sut,
                                                                           Actor actor,
                                                                           IFormatsReportFragment formatter,
                                                                           ReportFragment expected)
    {
        Mock.Get(formatter).Setup(x => x.Format("{Actor} is able to get file system paths for assets", actor)).Returns(expected);
        Assert.That(() => sut.GetReportFragment(actor, formatter), Is.EqualTo(expected));
    }
}