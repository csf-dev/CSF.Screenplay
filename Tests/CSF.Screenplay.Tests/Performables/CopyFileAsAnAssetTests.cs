using System.IO;
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Reporting;
using NUnit.Framework.Legacy;
using static CSF.Screenplay.PerforamableBuilder;

namespace CSF.Screenplay.Performables;

[TestFixture, Parallelizable]
public class CopyFileAsAnAssetTests
{
    [Test, AutoMoqData]
    public async Task PerformAsAsyncShouldCopyAFile(Actor actor, IGetsAssetFilePath pathProvider)
    {
        var destinationPath = Path.Combine(TestContext.CurrentContext.WorkDirectory, "CopyFileAsAnAssetTests-asset.txt");
        if(File.Exists(destinationPath))
            File.Delete(destinationPath);
        
        var sut = CopyTheFile(Path.Combine(TestContext.CurrentContext.TestDirectory, "Performables", "SampleAsset.CopyFileAsAnAsset.txt"))
            .AsAnAssetWithTheFilename("CopyFileAsAnAssetTests-asset.txt")
            .WithTheSummary("Sample asset");
        actor.IsAbleTo(new GetAssetFilePaths(pathProvider));
        Mock.Get(pathProvider)
            .Setup(x => x.GetAssetFilePath(It.IsAny<string>()))
            .Returns((string b) => Path.Combine(TestContext.CurrentContext.WorkDirectory, b));

        await sut.PerformAsAsync(actor);

        FileAssert.Exists(destinationPath);
    }

    [Test, AutoMoqData]
    public async Task GetReportFragmentShouldGetAReportFragment(Actor actor, IGetsAssetFilePath pathProvider, IFormatsReportFragment formatter)
    {
        var sut = CopyTheFile(Path.Combine(TestContext.CurrentContext.TestDirectory, "Performables", "SampleAsset.CopyFileAsAnAsset.txt"))
            .AsAnAssetWithTheFilename("CopyFileAsAnAssetTests-asset.txt")
            .WithTheSummary("Sample asset");
        actor.IsAbleTo(new GetAssetFilePaths(pathProvider));
        Mock.Get(pathProvider)
            .Setup(x => x.GetAssetFilePath(It.IsAny<string>()))
            .Returns((string b) => Path.Combine(TestContext.CurrentContext.WorkDirectory, b));

        sut.GetReportFragment(actor, formatter);

        Mock.Get(formatter)
            .Verify(x => x.Format("{Actor} copies {SourcePath} as an asset file named {Name}",
                    actor,
                    Path.Combine(TestContext.CurrentContext.TestDirectory, "Performables", "SampleAsset.CopyFileAsAnAsset.txt"),
                    "CopyFileAsAnAssetTests-asset.txt"));
    }
}