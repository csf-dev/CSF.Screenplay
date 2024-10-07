using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using CSF.Screenplay.Performances;
using CSF.Screenplay.ReportModel;

namespace CSF.Screenplay.Reporting
{
    /// <summary>
    /// Builder type accumulates <see cref="PerformanceReportBuilder"/> as performances occur within the Screenplay.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This type is used by <see cref="JsonScreenplayReporter"/> so that it may direct the appropriate reports (relating to performances)
    /// to the appropriate performance.
    /// Be mindful that performances could be occurring in parallel, so it would be incorrect to maintain a concept of "the current performance".
    /// This class provides a thread-safe mechanism by which to access the builder for each performance, as they occur.
    /// </para>
    /// <para>
    /// Use <see cref="BeginPerformance(Guid, IReadOnlyList{IdentifierAndName})"/> in order to add a new performance to this builder.
    /// During that performance's lifespan use <see cref="GetPerformanceBuilder(Guid)"/> in order to direct logic to its builder.
    /// Once the performance is over, use <see cref="EndPerformanceAndGetReport(Guid, bool?)"/> to remove that performance from this instance
    /// (it doesn't need tracking once it's finished) and to get the completed report for that performance.
    /// </para>
    /// </remarks>
    /// <seealso cref="PerformanceReportBuilder"/>
    public class ScreenplayReportBuilder
    {
        readonly ConcurrentDictionary<Guid, PerformanceReportBuilder> performanceReports = new ConcurrentDictionary<Guid, PerformanceReportBuilder>();
        readonly Func<List<IdentifierAndNameModel>, PerformanceReportBuilder> performanceBuilderFactory;

        /// <summary>
        /// Begins building a report about a new performance.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method adds the new performance to the state of the current instance, so that it tracked and may be built by accumulating
        /// further reporting data.
        /// Once the performance is being tracked, use <see cref="GetPerformanceBuilder(Guid)"/> to return a reference to that builder.
        /// Use <see cref="EndPerformanceAndGetReport(Guid, bool?)"/> once the performance is finished (successfully or otherwise), which will
        /// remove it from the current Screenplay builder.
        /// </para>
        /// </remarks>
        /// <param name="performanceIdentifier">The performance identifier</param>
        /// <param name="namingHierarchy">A naming hierarchy for that performance</param>
        /// <seealso cref="IPerformance"/>
        /// <seealso cref="IPerformance.NamingHierarchy"/>
        /// <seealso cref="IHasPerformanceIdentity"/>
        public void BeginPerformance(Guid performanceIdentifier, IReadOnlyList<IdentifierAndName> namingHierarchy)
        {
            var mappedNamingHierarchy = namingHierarchy.Select(x => new IdentifierAndNameModel { Identifier = x.Identifier, Name = x.Name }).ToList();
            performanceReports.TryAdd(performanceIdentifier, performanceBuilderFactory(mappedNamingHierarchy));
        }

        /// <summary>
        /// Gets the performance builder corresponding to the specified identifier.
        /// </summary>
        /// <param name="performanceIdentifier">The performance identifier</param>
        /// <returns>A performance report builder which corresponds to the requested performance</returns>
        /// <exception cref="ArgumentException">If the current Screenplay builder does not have a tracked performance with the specified <paramref name="performanceIdentifier"/>.</exception>
        /// <seealso cref="IPerformance"/>
        /// <seealso cref="IHasPerformanceIdentity"/>
        /// <seealso cref="PerformanceReportBuilder"/>
        public PerformanceReportBuilder GetPerformanceBuilder(Guid performanceIdentifier)
        {
            if (!performanceReports.TryGetValue(performanceIdentifier, out var builder))
                throw new ArgumentException($"This Screenplay report builder does not contain a performance builder with identifier {performanceIdentifier}", nameof(performanceIdentifier));
            return builder;
        }

        /// <summary>
        /// Ends the specified performance, removing its builder from the current instance, and returns the completed/built report.
        /// </summary>
        /// <param name="performanceIdentifier">The performance identifier</param>
        /// <param name="success">A value indicating whether or not the performance was a success.  This has the same semantics as the parameter
        /// to <see cref="IBeginsAndEndsPerformance.FinishPerformance(bool?)"/></param>
        /// <returns>The performance report which was built by the performance report builder</returns>
        /// <exception cref="ArgumentException">If the current Screenplay builder does not have a tracked performance with the specified <paramref name="performanceIdentifier"/>.</exception>
        /// <seealso cref="IPerformance"/>
        /// <seealso cref="IHasPerformanceIdentity"/>
        /// <seealso cref="IBeginsAndEndsPerformance"/>
        /// <seealso cref="PerformanceReport"/>
        public PerformanceReport EndPerformanceAndGetReport(Guid performanceIdentifier, bool? success)
        {
            if(!performanceReports.TryRemove(performanceIdentifier, out var builder))
                throw new ArgumentException($"This Screenplay report builder does not contain a performance builder with identifier {performanceIdentifier}", nameof(performanceIdentifier));
            
            return builder.GetReport(success);
        }

        /// <summary>
        /// Initialises a new instance of <see cref="ScreenplayReportBuilder"/>.
        /// </summary>
        /// <param name="performanceBuilderFactory">A factory function for performance report builders</param>
        /// <exception cref="ArgumentNullException">If <paramref name="performanceBuilderFactory"/> is <see langword="null" />.</exception>
        public ScreenplayReportBuilder(Func<List<IdentifierAndNameModel>,PerformanceReportBuilder> performanceBuilderFactory)
        {
            this.performanceBuilderFactory = performanceBuilderFactory ?? throw new ArgumentNullException(nameof(performanceBuilderFactory));
        }
    }
}