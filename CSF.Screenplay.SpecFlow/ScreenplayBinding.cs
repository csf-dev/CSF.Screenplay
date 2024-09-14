using System;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow;

namespace CSF.Screenplay
{
    /// <summary>
    /// SpecFlow binding which uses hooks to coordinate the relevant <see cref="Screenplay"/> &amp; <see cref="IPerformance"/> event invokers.
    /// </summary>
    [Binding]
    public class ScreenplayBinding
    {
        readonly IServiceProvider serviceProvider;
        
        /// <summary>
        /// Executed before each scenario.
        /// </summary>
        [BeforeScenario]
        public void BeforeScenario()
        {
            var performance = serviceProvider.GetRequiredService<IPerformance>();
            performance.BeginPerformance();
        }

        /// <summary>
        /// Executed after each scenario.
        /// </summary>
        [AfterScenario]
        public void AfterScenario()
        {
            var performance = serviceProvider.GetRequiredService<IPerformance>();
            var scenarioContext = serviceProvider.GetRequiredService<ScenarioContext>();
            var success = scenarioContext.TestError is null;
            performance.FinishPerformance(success);
        }

        /// <summary>
        /// Executed before a test run.
        /// </summary>
        [BeforeTestRun]
        public static void BeforeTestRun() => ScreenplayPlugin.Screenplay.BeginScreenplay();

        /// <summary>
        /// Executed after a test run.
        /// </summary>
        [AfterTestRun]
        public static void AfterTestRun() => ScreenplayPlugin.Screenplay.CompleteScreenplay();

        /// <summary>
        /// Initialises a new instance of <see cref="ScreenplayBinding"/>.
        /// </summary>
        /// <param name="serviceProvider">The service provider</param>
        /// <exception cref="ArgumentNullException">If the <paramref name="serviceProvider"/> is <see langword="null" />.</exception>
        public ScreenplayBinding(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }
    }
}