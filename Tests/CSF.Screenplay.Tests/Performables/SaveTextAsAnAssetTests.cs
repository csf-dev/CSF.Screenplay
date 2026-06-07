using System.IO;
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Reporting;
using NUnit.Framework.Legacy;
using static CSF.Screenplay.PerforamableBuilder;

namespace CSF.Screenplay.Performables;

[TestFixture, Parallelizable]
public class SaveTextAsAnAssetTests
{
    [Test, AutoMoqData]
    public async Task PerformAsAsyncShouldSaveTheText(Actor actor, IGetsAssetFilePath pathProvider)
    {
        var destinationPath = Path.Combine(TestContext.CurrentContext.WorkDirectory, "SaveTextAsAnAssetTests-asset.txt");
        if(File.Exists(destinationPath))
            File.Delete(destinationPath);
        
        var builder = SaveTheText("Foo bar baz")
            .AsAnAssetWithTheFilename("SaveTextAsAnAssetTests-asset.txt")
            .WithTheSummary("Sample asset");
        var sut = ((IGetsPerformable) builder).GetPerformable();
        actor.IsAbleTo(new GetAssetFilePaths(pathProvider));
        Mock.Get(pathProvider)
            .Setup(x => x.GetAssetFilePath(It.IsAny<string>()))
            .Returns((string b) => Path.Combine(TestContext.CurrentContext.WorkDirectory, b));

        await sut.PerformAsAsync(actor);

        FileAssert.Exists(destinationPath);
    }

    [Test, AutoMoqData]
    public void GetReportFragmentShouldGetAReportFragment(Actor actor, IGetsAssetFilePath pathProvider, IFormatsReportFragment formatter)
    {
        var builder = SaveTheText("Foo bar baz")
            .AsAnAssetWithTheFilename("SaveTextAsAnAssetTests-asset.txt")
            .WithTheSummary("Sample asset");
        var sut = (ICanReport) ((IGetsPerformable) builder).GetPerformable();
        actor.IsAbleTo(new GetAssetFilePaths(pathProvider));
        Mock.Get(pathProvider)
            .Setup(x => x.GetAssetFilePath(It.IsAny<string>()))
            .Returns((string b) => Path.Combine(TestContext.CurrentContext.WorkDirectory, b));

        sut.GetReportFragment(actor, formatter);

        Mock.Get(formatter)
            .Verify(x => x.Format("{Actor} saves some text as an asset file named {Name}",
                    actor,
                    "SaveTextAsAnAssetTests-asset.txt"));
    }
}