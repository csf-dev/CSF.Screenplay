using System;
using System.Collections.Generic;
using System.IO;
using CSF.Screenplay.Abilities;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performances;
using CSF.Screenplay.Reporting;
using CSF.Screenplay.ReportModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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

            services
                .AddOptions<ScreenplayOptions>()
                .Configure((ScreenplayOptions o, PerformanceEventBus eventBus) => o.PerformanceEventsConfig?.Invoke(eventBus));
            
            services
                .AddSingleton<Screenplay>()
                .AddSingleton<PerformanceEventBus>()
                .AddSingleton<IHasPerformanceEvents>(s => s.GetRequiredService<PerformanceEventBus>())
                .AddSingleton<IRelaysPerformanceEvents>(s => s.GetRequiredService<PerformanceEventBus>())
                .AddSingleton(s => s.GetRequiredService<IOptions<ScreenplayOptions>>().Value.ValueFormatters)
                .AddSingleton<ScreenplayReportBuilder>()
                .AddSingleton<IGetsReportPath, ReportPathProvider>()
                .AddSingleton<IReporter>(s =>
                {
                    var reportPath = s.GetRequiredService<IGetsReportPath>().GetReportFilePath();
                    if(reportPath is null) return new NoOpReporter();
                    
                    Directory.CreateDirectory(Path.GetDirectoryName(reportPath));
                    var stream = File.Create(reportPath);
                    return ActivatorUtilities.CreateInstance<JsonScreenplayReporter>(s, stream);
                });

            services
                .AddScoped<ICast>(s => new Cast(s, s.GetRequiredService<IPerformance>().PerformanceIdentity))
                .AddScoped<IStage, Stage>()
                .AddScoped<PerformanceProvider>()
                .AddScoped(s => s.GetRequiredService<PerformanceProvider>().GetCurrentPerformance())
                .AddScoped<IGetsAssetFilePath, AssetPathProvider>();

            services
                .AddTransient<IGetsReportFormat, ReportFormatCreator>()
                .AddTransient<IGetsValueFormatter, ValueFormatterProvider>()
                .AddTransient<IFormatsReportFragment, ReportFragmentFormatter>()
                .AddTransient<PerformanceReportBuilder>()
                .AddTransient<JsonScreenplayReporter>()
                .AddTransient<NoOpReporter>()
                .AddTransient<ITestsPathForWritePermissions, WritePermissionTester>()
                .AddTransient<Func<List<IdentifierAndNameModel>, PerformanceReportBuilder>>(s =>
                {
                    return idsAndNames => ActivatorUtilities.CreateInstance<PerformanceReportBuilder>(s, idsAndNames);
                })
                .AddTransient<GetAssetFilePaths>()
                .AddTransient<ToStringFormatter>()
                .AddTransient<HumanizerFormatter>()
                .AddTransient<NameFormatter>()
                .AddTransient<FormattableFormatter>();
            
            return services;
        }
        
    }
}