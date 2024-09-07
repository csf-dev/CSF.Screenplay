using CSF.Screenplay.Performances;

namespace CSF.Screenplay;

[TestFixture,Parallelizable]
public class TestWithoutDescription
{
    [Test,Screenplay]
    public void FirstTest(IPerformance performance)
    {
        IdentifierAndName[] expectedNamingHierarchy = [
            new(typeof(TestWithoutDescription).FullName),
            new($"{typeof(TestWithoutDescription).FullName}.{nameof(FirstTest)}"),
        ];

        AssertThatPerformanceHasCorrectState(performance, expectedNamingHierarchy);
    }

    [Test,Screenplay]
    public void SecondTest(IPerformance performance)
    {
        IdentifierAndName[] expectedNamingHierarchy = [
            new(typeof(TestWithoutDescription).FullName),
            new($"{typeof(TestWithoutDescription).FullName}.{nameof(SecondTest)}"),
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