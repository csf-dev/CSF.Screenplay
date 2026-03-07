using System.IO;
using System.Reflection;
using AutoFixture;
using CSF.Screenplay.Reporting;
using CSF.Screenplay.ReportModel;

namespace CSF.Screenplay;

/// <summary>
/// Creates a <see cref="ScreenplayReport"/> which contains two sample assets, a PNG and a JPEG image file.
/// </summary>
public class SampleAssetsAttribute : CustomizeAttribute
{
    public override ICustomization GetCustomization(ParameterInfo parameter)
        => new SampleAssetsCustomization();
}

public class SampleAssetsCustomization : ICustomization
{
    internal const string Asset1Filename = "SampleAsset1.png", Asset2Filename = "SampleAsset2.jpeg";

    public void Customize(IFixture fixture)
    {
        fixture.Customize<ScreenplayReport>(c => c.FromFactory(() =>
        {
            return new ScreenplayReport()
            {
                Performances = [
                    new () {
                        Reportables = [
                            new PerformableReport() {
                                Assets = [
                                    new () {
                                        ContentType = MimeTypes.GetMimeType(Asset1Filename),
                                        FileName = Asset1Filename,
                                        FilePath = Path.Combine(Environment.CurrentDirectory, Asset1Filename)
                                    },
                                    new () {
                                        ContentType = MimeTypes.GetMimeType(Asset2Filename),
                                        FileName = Asset2Filename,
                                        FilePath = Path.Combine(Environment.CurrentDirectory, Asset2Filename)
                                    },
                                ]
                            }
                        ]
                    }
                ]
            };
        }).OmitAutoProperties());
    }
}