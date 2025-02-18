using Autofac.Builder;

namespace ReqnrollPlugins.Autofac.ToReqnroll.AutofacPlugin;

public static class AutofacExtensions
{
    public static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle>
        InstancePerReqnrollScope<TLimit, TActivatorData, TRegistrationStyle>(
            this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> builder, DependencyLifetime lifetime)
    {
        if (lifetime == DependencyLifetime.TestRun)
            return builder.SingleInstance();

        return builder.InstancePerMatchingLifetimeScope(lifetime.GetName());
    }

    public static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle>
        ScenarioScope<TLimit, TActivatorData, TRegistrationStyle>(
            this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> builder)
    {
        return builder.InstancePerReqnrollScope(DependencyLifetime.Scenario);
    }

    public static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle>
        FeatureScope<TLimit, TActivatorData, TRegistrationStyle>(
            this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> builder)
    {
        return builder.InstancePerReqnrollScope(DependencyLifetime.Feature);
    }

    public static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle>
        WorkerScope<TLimit, TActivatorData, TRegistrationStyle>(
            this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> builder)
    {
        return builder.InstancePerReqnrollScope(DependencyLifetime.Worker);
    }

    public static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle>
        TestRunScope<TLimit, TActivatorData, TRegistrationStyle>(
            this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> builder)
    {
        return builder.InstancePerReqnrollScope(DependencyLifetime.TestRun);
    }
}