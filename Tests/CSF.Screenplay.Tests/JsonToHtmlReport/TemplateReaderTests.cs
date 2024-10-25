namespace CSF.Screenplay.JsonToHtmlReport
{
    [TestFixture,Parallelizable]
    public class TemplateReaderTests
    {
        [Test, AutoMoqData]
        public async Task ReadTemplateShouldReturnAString(TemplateReader sut)
        {
            var result = await sut.ReadTemplate();

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null, "Not null");
                Assert.That(result, Is.Not.Empty, "Not empty");
            });
        }
    }
}