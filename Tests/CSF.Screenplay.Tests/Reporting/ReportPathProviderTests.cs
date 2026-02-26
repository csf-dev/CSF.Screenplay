using System.IO;
using Microsoft.Extensions.Options;
using NUnit.Framework.Internal;

namespace CSF.Screenplay.Reporting;

[TestFixture, Parallelizable]
public class ReportPathProviderTests
{
    [Test, AutoMoqData]
    public void GetReportPathShouldReturnAbsolutePathIfSpecified([Frozen] ITestsPathForWritePermissions permissionsTester,
                                                                 [Frozen, Options] IOptions<ScreenplayOptions> opts,
                                                                 ReportPathProvider sut)
    {
        var path = Path.Combine(Environment.CurrentDirectory, "myreport", "reporting_path");
        Mock.Get(permissionsTester).Setup(x => x.HasWritePermission(path)).Returns(true);
        opts.Value.ReportPath = path;
        Assert.That(() => sut.GetReportPath(), Is.EqualTo(path)); 
    }

    [Test, AutoMoqData]
    public void GetReportPathShouldReturnAbsolutePathBasedOnCurrentDirIfRelativePathSpecified([Frozen] ITestsPathForWritePermissions permissionsTester,
                                                                                              [Frozen, Options] IOptions<ScreenplayOptions> opts,
                                                                                              ReportPathProvider sut)
    {
        var path = Path.Combine("myreport", "reporting_path");
        Mock.Get(permissionsTester).Setup(x => x.HasWritePermission(Path.Combine(Environment.CurrentDirectory, "myreport", "reporting_path"))).Returns(true);
        opts.Value.ReportPath = path;
        Assert.That(() => sut.GetReportPath(), Is.EqualTo(Path.Combine(Environment.CurrentDirectory, "myreport", "reporting_path")));
    }

    [Test, AutoMoqData]
    public void GetReportPathShouldReturnNullIfNoPathSpecified([Frozen] ITestsPathForWritePermissions permissionsTester,
                                                               [Frozen, Options] IOptions<ScreenplayOptions> opts,
                                                               ReportPathProvider sut)
    {
        opts.Value.ReportPath = null;
        Assert.That(() => sut.GetReportPath(), Is.Null);
    }

    [Test, AutoMoqData]
    public void GetReportPathShouldReturnNullIfNoPermissionsAvailable([Frozen] ITestsPathForWritePermissions permissionsTester,
                                                                      [Frozen, Options] IOptions<ScreenplayOptions> opts,
                                                                      ReportPathProvider sut)
    {
        var path = Path.Combine("myreport", "reporting_path");
        Mock.Get(permissionsTester).Setup(x => x.HasWritePermission(Path.Combine(Environment.CurrentDirectory, "myreport", "reporting_path"))).Returns(false);
        opts.Value.ReportPath = path;
        Assert.That(() => sut.GetReportPath(), Is.Null);
    }

    [Test, AutoMoqData]
    public void GetReportFilePathShouldReturnFilePath([Frozen] ITestsPathForWritePermissions permissionsTester,
                                                      [Frozen, Options] IOptions<ScreenplayOptions> opts,
                                                      ReportPathProvider sut)
    {
        var path = Path.Combine(Environment.CurrentDirectory, "myreport", "reporting_path");
        Mock.Get(permissionsTester).Setup(x => x.HasWritePermission(path)).Returns(true);
        opts.Value.ReportPath = path;
        Assert.That(() => sut.GetReportFilePath(), Is.EqualTo(Path.Combine(Environment.CurrentDirectory, "myreport", "reporting_path", "ScreenplayReport.json"))); 
    }
}