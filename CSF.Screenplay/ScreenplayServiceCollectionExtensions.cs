using System;
using System.Collections.Generic;
using System.IO;
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performances;
using CSF.Screenplay.Reporting;
using CSF.Screenplay.ReportModel;
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
            services.AddSingleton(s => s.GetRequiredService<ScreenplayOptions>().ValueFormatters);
            services.AddSingleton<ScreenplayReportBuilder>();
            services.AddSingleton<IGetsReportPath, ReportPathProvider>();
            services.AddSingleton<IReporter>(s =>
            {
                var reportPath = s.GetRequiredService<IGetsReportPath>().GetReportPath();
                if(reportPath is null) return new NoOpReporter();
                
                var stream = File.Create(reportPath);
                return ActivatorUtilities.CreateInstance<JsonScreenplayReporter>(s, stream);
            });

            services.AddScoped<ICast>(s => new Cast(s, s.GetRequiredService<IPerformance>().PerformanceIdentity));
            services.AddScoped<IStage, Stage>();
            services.AddScoped(s => s.GetRequiredService<ICreatesPerformance>().CreatePerformance());
            services.AddScoped<IGetsAssetFilePath, AssetPathProvider>();

            services.AddTransient<ICreatesPerformance, PerformanceFactory>();
            services.AddTransient<IGetsReportFormat, ReportFormatCreator>();
            services.AddTransient<IGetsValueFormatter, ValueFormatterProvider>();
            services.AddTransient<IFormatsReportFragment, ReportFragmentFormatter>();
            services.AddTransient<PerformanceReportBuilder>();
            services.AddTransient<JsonScreenplayReporter>();
            services.AddTransient<NoOpReporter>();
            services.AddTransient<Func<List<IdentifierAndNameModel>, PerformanceReportBuilder>>(s =>
            {
                return idsAndNames => ActivatorUtilities.CreateInstance<PerformanceReportBuilder>(s, idsAndNames);
            });
            services.AddTransient<GetAssetFilePaths>();
            foreach(var type in optionsModel.ValueFormatters)
                services.AddTransient(type);
            
            return services;
        }
        
    }
}