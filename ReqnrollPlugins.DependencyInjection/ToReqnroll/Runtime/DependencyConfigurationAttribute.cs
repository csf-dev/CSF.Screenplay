namespace Reqnroll;

[AttributeUsage(AttributeTargets.Class)]
public class DependencyConfigurationAttribute : Attribute
{
    public const int DefaultPriority = 0;
    public const int LowPriority = -100;
    public const int HighPriority = 100;

    /// <summary>
    /// Defines the priority of the dependency configuration to choose configuration from multiple. Higher value means higher priority.
    /// </summary>
    public int Priority { get; set; } = DefaultPriority;
}