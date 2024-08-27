using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performances;
using Microsoft.Extensions.DependencyInjection;

namespace CSF.Screenplay
{
    /// <summary>
    /// Helper class to configure dependency injection for Screenplay.
    /// </summary>
    static class DependencyInjectionSetup
    {
        /// <summary>
        /// Sets up DI for the boilerplate/standard types which are common to all Screenplays.
        /// </summary>
        /// <param name="services">The service collection for which to setup DI.</param>
        /// <param name="screenplay">The Screenplay for which we are configuring DI.</param>
        /// <returns>A service provider</returns>
        internal static IServiceProvider SetupDependencyInjection(IServiceCollection services, Screenplay screenplay)
        {
            services = services ?? new ServiceCollection();
            services.AddTransient<ICreatesPerformance, PerformanceFactory>();
            services.AddSingleton(screenplay);
            services.AddSingleton<PerformanceEventBus>();
            services.AddTransient<IHasPerformanceEvents>(s => s.GetRequiredService<PerformanceEventBus>());
            services.AddTransient<IRelaysPerformanceEvents>(s => s.GetRequiredService<PerformanceEventBus>());
            services.AddScoped(s => s.GetRequiredService<ICreatesPerformance>().CreatePerformance());
            services.AddScoped<ICast>(s => new Cast(s, s.GetRequiredService<IPerformance>().PerformanceIdentity));
            services.AddScoped<IStage, Stage>();
            return services.BuildServiceProvider();
        }
    }
}