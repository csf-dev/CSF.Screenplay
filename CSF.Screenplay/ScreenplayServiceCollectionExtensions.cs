using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performances;
using Microsoft.Extensions.DependencyInjection;

namespace CSF.Screenplay
{
    /// <summary>
    /// Extension methods for <see cref="IServiceCollection"/> relating to Screenplay.
    /// </summary>
    public static class ScreenplayServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the Screenplay framework to the specified service collection.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Use this method to add Screenplay to an existing service collection; if you just want an instance of <see cref="Screenplay"/>
        /// and do not care for integrating it with a service collection of your own then consider the convenience method
        /// <see cref="Screenplay.Create(Action{IServiceCollection})"/>.
        /// </para>
        /// </remarks>
        /// <param name="services">An <see cref="IServiceCollection"/></param>
        /// <returns>The service collection, so that calls may be chained</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="services"/> is <see langword="null" />.</exception>
        public static IServiceCollection AddScreenplay(this IServiceCollection services)
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            services.AddSingleton<Screenplay>();
            services.AddSingleton<PerformanceEventBus>();

            services.AddScoped<ICast>(s => new Cast(s, s.GetRequiredService<IPerformance>().PerformanceIdentity));
            services.AddScoped<IStage, Stage>();
            services.AddScoped(s => s.GetRequiredService<ICreatesPerformance>().CreatePerformance());

            services.AddTransient<IHasPerformanceEvents>(s => s.GetRequiredService<PerformanceEventBus>());
            services.AddTransient<IRelaysPerformanceEvents>(s => s.GetRequiredService<PerformanceEventBus>());
            services.AddTransient<ICreatesPerformance, PerformanceFactory>();

            return services;
        }
    }
}