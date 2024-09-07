using CSF.Screenplay.Performances;

namespace CSF.Screenplay;

[TestFixture,Parallelizable,Description("This is a test fixture with a description")]
public class TestWithDescription
{
    [Test,Screenplay,Description("This is the first test")]
    public void FirstTest(IPerformance performance)
    {
        IdentifierAndName[] expectedNamingHierarchy = [
            new(typeof(TestWithDescription).FullName, "This is a test fixture with a description"),
            new($"{typeof(TestWithDescription).FullName}.{nameof(FirstTest)}", "This is the first test"),
        ];

        AssertThatPerformanceHasCorrectState(performance, expectedNamingHierarchy);
    }

    [Test,Screenplay,Description("This is the second test")]
    public void SecondTest(IPerformance performance)
    {
        IdentifierAndName[] expectedNamingHierarchy = [
            new(typeof(TestWithDescription).FullName, "This is a test fixture with a description"),
            new($"{typeof(TestWithDescription).FullName}.{nameof(SecondTest)}", "This is the second test"),
        ];

        AssertThatPerformanceHasCorrectState(performance, expectedNamingHierarchy);
    }

    static void AssertThatPerformanceHasCorrectState(IPerformance performance, IdentifierAndName[] expectedNamingHierarchy)
    {
        Assert.Multiple(() =>
        {
            Assert.That(performance, Is.Not.Null, "Performance must not be null");
            Assert.That(performance.NamingHierarchy, Is.EqualTo(expectedNamingHierarchy), "Performance naming hierarchy is correct");
        });
    }
}