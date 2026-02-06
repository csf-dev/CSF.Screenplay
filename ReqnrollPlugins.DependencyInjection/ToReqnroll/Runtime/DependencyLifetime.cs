namespace Reqnroll;


public enum DependencyLifetime
{
    TestRun,
    Worker,
    Feature,
    Scenario,
}

public static class DependencyLifetimeExtensions
{
    public static string GetName(this DependencyLifetime lifetime)
    {
        return lifetime switch
        {
            DependencyLifetime.TestRun => nameof(DependencyLifetime.TestRun),
            DependencyLifetime.Worker => nameof(DependencyLifetime.Worker),
            DependencyLifetime.Feature => nameof(DependencyLifetime.Feature),
            DependencyLifetime.Scenario => nameof(DependencyLifetime.Scenario),
            _ => "Unknown"
        };
    }
}