using System.IO;

namespace CSF.Screenplay.Reporting;

[TestFixture, Parallelizable]
public class AssetPathProviderTests
{
    [Test, AutoMoqData]
    public void GetAssetFilePathShouldThrowIfBaseNameIsNull(AssetPathProvider sut)
    {
        Assert.That(() => sut.GetAssetFilePath(null), Throws.ArgumentException);
    }

    [Test, AutoMoqData]
    public void GetAssetFilePathShouldThrowIfBaseNameIsEmpty(AssetPathProvider sut)
    {
        Assert.That(() => sut.GetAssetFilePath(null), Throws.ArgumentException);
    }

    [Test, AutoMoqData]
    public void GetAssetFilePathShouldThrowIfBaseNameIsADirectory(AssetPathProvider sut)
    {
        Assert.That(() => sut.GetAssetFilePath("aDirectory" + Path.DirectorySeparatorChar), Throws.ArgumentException);
    }

    [Test, AutoMoqData]
    public void GetAssetFilePathShouldReturnNullIfReportPathIsNull([Frozen] IGetsReportPath reportPathProvider, AssetPathProvider sut)
    {
        Mock.Get(reportPathProvider).Setup(x => x.GetReportPath()).Returns(() => null!);
        Assert.That(() => sut.GetAssetFilePath("myAsset.png"), Is.Null);
    }

    [Test, AutoMoqData]
    public void GetAssetFilePathShouldReturnANameBasedOnTheReportPathAndPerformanceIdentifier([Frozen] IGetsReportPath reportPathProvider,
                                                                                              [Frozen] IPerformance performance,
                                                                                              AssetPathProvider sut)
    {
        Mock.Get(reportPathProvider).Setup(x => x.GetReportPath()).Returns(Path.Combine("foo", "bar"));
        Mock.Get(performance).SetupGet(x => x.NamingHierarchy).Returns([new ("firstId")]);
        Assert.That(() => sut.GetAssetFilePath("myAsset.png"), Is.EqualTo(Path.Combine("foo", "bar", "firstId_001_myAsset.png")));
    }

    [Test, AutoMoqData]
    public void GetAssetFilePathShouldReturnANameBasedOnTheReportPathAndPerformanceIdIfNoPerformanceIdentifier([Frozen] IGetsReportPath reportPathProvider,
                                                                                                               [Frozen] IPerformance performance,
                                                                                                               AssetPathProvider sut,
                                                                                                               Guid performanceId)
    {
        Mock.Get(reportPathProvider).Setup(x => x.GetReportPath()).Returns(Path.Combine("foo", "bar"));
        Mock.Get(performance).SetupGet(x => x.NamingHierarchy).Returns([]);
        Mock.Get(performance).SetupGet(x => x.PerformanceIdentity).Returns(performanceId);
        Assert.That(() => sut.GetAssetFilePath("myAsset.png"), Is.EqualTo(Path.Combine("foo", "bar", $"{performanceId}_001_myAsset.png")));
    }

    [Test, AutoMoqData]
    public void GetAssetFilePathShouldRemoveInvalidFilenameChars([Frozen] IGetsReportPath reportPathProvider,
                                                                 [Frozen] IPerformance performance,
                                                                 AssetPathProvider sut)
    {
        Mock.Get(reportPathProvider).Setup(x => x.GetReportPath()).Returns(Path.Combine("foo", "bar"));
        Mock.Get(performance).SetupGet(x => x.NamingHierarchy).Returns([new ("first///Id")]);
        Assert.That(() => sut.GetAssetFilePath("myAsset.png"), Is.EqualTo(Path.Combine("foo", "bar", "firstId_001_myAsset.png")));
    }
}