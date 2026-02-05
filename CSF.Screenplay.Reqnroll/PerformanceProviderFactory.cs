using System;
using System.Collections.Concurrent;
using CSF.Screenplay.Performances;
using Microsoft.Extensions.DependencyInjection;
#if SPECFLOW
using TechTalk.SpecFlow;
#else
using Reqnroll;
#endif

namespace CSF.Screenplay
{
    /// <summary>
    /// Factory type for instances of <see cref="PerformanceProvider"/>.
    /// </summary>
    public class PerformanceProviderFactory
    {
        readonly ConcurrentDictionary<FeatureInfo, Guid> featureContextIds = new ConcurrentDictionary<FeatureInfo, Guid>();
        readonly ConcurrentDictionary<ScenarioAndFeatureInfoKey, Guid> scenarioContextIds = new ConcurrentDictionary<ScenarioAndFeatureInfoKey, Guid>();

        /// <summary>
        /// Gets an instance of <see cref="PerformanceProvider"/> for the specified service provider.
        /// </summary>
        /// <param name="services">A service provider</param>
        /// <returns>A <see cref="PerformanceProvider"/>.</returns>
        public PerformanceProvider GetPerformanceContainer(IServiceProvider services)
        {
            var performance = new Performance(services, new [] { GetFeatureIdAndName(services), GetScenarioIdAndName(services) });
            var performanceContainer = new PerformanceProvider();
            performanceContainer.SetCurrentPerformance(performance);
            return performanceContainer;
        }

        IdentifierAndName GetFeatureIdAndName(IServiceProvider services)
        {
            var featureInfo = services.GetRequiredService<FeatureInfo>();
            return new IdentifierAndName(GetFeatureId(featureInfo).ToString(),
                                         featureInfo.Title,
                                         true);
        }

        Guid GetFeatureId(FeatureInfo featureContext) => featureContextIds.GetOrAdd(featureContext, _ => Guid.NewGuid());

        IdentifierAndName GetScenarioIdAndName(IServiceProvider services)
        {
            var featureInfo = services.GetRequiredService<FeatureInfo>();
            var scenarioInfo = services.GetRequiredService<ScenarioInfo>();
            return new IdentifierAndName(GetScenarioId(featureInfo, scenarioInfo).ToString(),
                                         scenarioInfo.Title,
                                         true);
        }

        Guid GetScenarioId(FeatureInfo featureInfo, ScenarioInfo scenarioInfo)
            => scenarioContextIds.GetOrAdd(new ScenarioAndFeatureInfoKey(scenarioInfo, featureInfo), _ => Guid.NewGuid());

    }
}