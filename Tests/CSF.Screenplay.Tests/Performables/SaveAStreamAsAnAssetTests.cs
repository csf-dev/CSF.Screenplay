using System.IO;
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Reporting;
using NUnit.Framework.Legacy;
using static CSF.Screenplay.PerforamableBuilder;

namespace CSF.Screenplay.Performables;

[TestFixture, Parallelizable]
public class SaveAStreamAsAnAssetTests
{
    [Test, AutoMoqData]
    public async Task PerformAsAsyncShouldSaveTheStream(Actor actor, IGetsAssetFilePath pathProvider)
    {
        var destinationPath = Path.Combine(TestContext.CurrentContext.WorkDirectory, "SaveAStreamAsAnAssetTests-asset.txt");
        if(File.Exists(destinationPath))
            File.Delete(destinationPath);
        
        using var stream = GetStream();
        var sut = SaveTheStream(stream)
            .AsAnAssetWithTheFilename("SaveAStreamAsAnAssetTests-asset.txt")
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
        using var stream = GetStream();
        var sut = SaveTheStream(stream)
            .AsAnAssetWithTheFilename("SaveAStreamAsAnAssetTests-asset.txt")
            .WithTheSummary("Sample asset");
        actor.IsAbleTo(new GetAssetFilePaths(pathProvider));
        Mock.Get(pathProvider)
            .Setup(x => x.GetAssetFilePath(It.IsAny<string>()))
            .Returns((string b) => Path.Combine(TestContext.CurrentContext.WorkDirectory, b));

        sut.GetReportFragment(actor, formatter);

        Mock.Get(formatter)
            .Verify(x => x.Format("{Actor} saves a stream as an asset file named {Name}",
                    actor,
                    "SaveAStreamAsAnAssetTests-asset.txt"));
    }

    static Stream GetStream()
    {
        var stream = new MemoryStream();
        var writer = new StringWriter();
        writer.WriteLine("This is a sample stream");
        writer.Flush();
        stream.Position = 0;
        return stream;
    }
}