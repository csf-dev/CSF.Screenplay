using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performances;
using CSF.Screenplay.Reporting;
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
        /// <see cref="Screenplay.Create(Action{IServiceCollection}, Action{ScreenplayOptions})"/>.
        /// </para>
        /// <para>
        /// If you choose to provide any configuration via the <paramref name="options"/> parameter, do not 'capture' the <see cref="ScreenplayOptions"/>
        /// object outside the closure.  This object is added to dependency injection as a singleton, and so if it is modified outside of
        /// this configuration action then the integrity of the Screenplay dependency injection may be compromised.
        /// </para>
        /// </remarks>
        /// <param name="services">An <see cref="IServiceCollection"/></param>
        /// <param name="options">An optional configuration action, used to configure Screenplay in DI</param>
        /// <returns>The service collection, so that calls may be chained</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="services"/> is <see langword="null" />.</exception>
        public static IServiceCollection AddScreenplay(this IServiceCollection services, Action<ScreenplayOptions> options = null)
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));
            var optionsModel = new ScreenplayOptions();
            options?.Invoke(optionsModel);

            var eventBus = new PerformanceEventBus();
            optionsModel.PerformanceEventsConfig?.Invoke(eventBus);

            services.AddSingleton<Screenplay>();
            services.AddSingleton(eventBus);
            services.AddSingleton<IHasPerformanceEvents>(s => s.GetRequiredService<PerformanceEventBus>());
            services.AddSingleton<IRelaysPerformanceEvents>(s => s.GetRequiredService<PerformanceEventBus>());
            services.AddSingleton(optionsModel);

            services.AddScoped<ICast>(s => new Cast(s, s.GetRequiredService<IPerformance>().PerformanceIdentity));
            services.AddScoped<IStage, Stage>();
            services.AddScoped(s => s.GetRequiredService<ICreatesPerformance>().CreatePerformance());

            services.AddTransient<ICreatesPerformance, PerformanceFactory>();

            if (ShouldEnableReporting(optionsModel))
                AddReporting(services, optionsModel, eventBus);
            
            return services;
        }

        static bool ShouldEnableReporting(ScreenplayOptions optionsModel)
        {
            if (string.IsNullOrWhiteSpace(optionsModel.ReportPath))
                return false;
            return WritePermissionTester.HasWritePermission(optionsModel.ReportPath);
        }

        static void AddReporting(IServiceCollection services, ScreenplayOptions optionsModel, PerformanceEventBus eventBus)
        {
            services.AddSingleton(s => s.GetRequiredService<ScreenplayOptions>().ValueFormatters);
            services.AddTransient<IGetsReportFormat, ReportFormatCreator>();
            services.AddTransient<IGetsValueFormatter, ValueFormatterProvider>();
            foreach(var type in optionsModel.ValueFormatters)
                services.AddTransient(type);

            var reporter = new JsonScreenplayReporter(optionsModel.ReportPath);
            reporter.SubscribeTo(eventBus);
        }
    }
}