using System;
using System.Collections.Generic;
using System.Linq;
using CSF.Screenplay.Performables;
using CSF.Screenplay.Performances;
using CSF.Screenplay.ReportModel;
using CSF.Screenplay.Resources;

namespace CSF.Screenplay.Reporting
{
    /// <summary>
    /// Builder for an instance of <see cref="PerformanceReport"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This is a stateful builder, so instances of this type are not reusable for more than one <see cref="PerformanceReport"/>.
    /// </para>
    /// </remarks>
    public class PerformanceReportBuilder
    {
        readonly PerformanceReport report;
        readonly Stack<PerformableReport> performableStack = new Stack<PerformableReport>();
        readonly IGetsValueFormatter valueFormatterProvider;
        readonly IFormatsReportFragment formatter;

        /// <summary>
        /// Gets a value indicating whether or not this builder has a 'current' performable that it is building.
        /// </summary>
        bool HasCurrentPerformable => performableStack.Count != 0;

        /// <summary>
        /// Gets the 'current' perormable that this builder is building; this will throw
        /// if <see cref="HasCurrentPerformable"/> is <see langword="false" />.
        /// </summary>
        PerformableReport CurrentPerformable => performableStack.Peek();

        /// <summary>
        /// Gets the collection of reportable models to which new performables should be added when they are begun.
        /// </summary>
        /// <seealso cref="BeginPerformable(object, Actor, string)"/>
        List<ReportableModelBase> NewPerformableList
            => HasCurrentPerformable ? CurrentPerformable.Reportables : report.Reportables;

        /// <summary>
        /// Records the outcome of this performance and gets the report.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Use this method when the performance has completed.  Once this method has been used, it is normal
        /// that this builder instance will not be used any further.
        /// </para>
        /// </remarks>
        /// <param name="success">A value indicating the outcome of the performance, this has the same semantics as
        /// the parameter to <see cref="IBeginsAndEndsPerformance.FinishPerformance(bool?)"/></param>
        /// <returns>The completed performance report.</returns>
        public PerformanceReport GetReport(bool? success)
        {
            switch(success)
            {
            case true:
                report.Outcome = PerformanceState.Success.ToString();
                break;
            case false:
                report.Outcome = PerformanceState.Failed.ToString();
                break;
            default:
                report.Outcome = PerformanceState.Completed.ToString();
                break;
            }
            return report;
        }

#region Actors

        /// <summary>
        /// Adds a report to the current performance indicating that an actor has been created/added to the performance.
        /// </summary>
        /// <param name="actor">The actor</param>
        public void ActorCreated(Actor actor)
        {
            NewPerformableList.Add(new ActorCreatedReport
            {
                ActorName = actor.Name,
                Report = string.Format(ReportStrings.ActorCreatedFormat, actor.Name),
            });
        }

        /// <summary>
        /// Adds a report to the current performance indicating that an actor has gained/been granted a new ability.
        /// </summary>
        /// <param name="actor">The actor</param>
        /// <param name="ability">The ability which was granted</param>
        public void ActorGainedAbility(Actor actor, object ability)
        {
            var reportText = ability is ICanReport reporter
                ? reporter.GetReportFragment(actor, formatter).FormattedFragment
                : string.Format(ReportStrings.ActorGainedAbilityFormat, actor.Name, valueFormatterProvider.FormatValue(ability));

            NewPerformableList.Add(new ActorGainedAbilityReport
            {
                ActorName = actor.Name,
                Report = reportText,
            });
        }

        /// <summary>
        /// Adds a report to the current performance indicating that an actor has been placed into the spotlight.
        /// </summary>
        /// <remarks>
        /// <para>
        /// For more information, see <xref href="SpotlightGlossaryItem?text=the+spotlight+glossary+item"/>.
        /// </para>
        /// </remarks>
        /// <param name="actor">The actor</param>
        public void ActorSpotlit(Actor actor)
        {
            NewPerformableList.Add(new ActorSpotlitReport
            {
                ActorName = actor.Name,
                Report = string.Format(ReportStrings.ActorSpotlitFormat, actor.Name),
            });
        }

        /// <summary>
        /// Adds a report to the current performance indicating that the spotlight has been turned off.
        /// </summary>
        /// <remarks>
        /// <para>
        /// For more information, see <xref href="SpotlightGlossaryItem?text=the+spotlight+glossary+item"/>.
        /// </para>
        /// </remarks>
        public void SpotlightTurnedOff()
        {
            NewPerformableList.Add(new SpotlightTurnedOffReport
            {
                Report = ReportStrings.SpotlightTurnedOff,
            });
        }

#endregion

#region Performables

        /// <summary>
        /// Begins a new report within the current performance, that a new performable has been begun.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This might be a new performable at the root of the performance, or it might be nested within
        /// another performable which is already in progress.
        /// </para>
        /// <para>
        /// When this method is executed, the <paramref name="performable"/> becomes the 'current' performable for this
        /// builder.  The current performable is tracked as a <see cref="Stack{T}"/>, as a performable may contain a
        /// hierarchy of further performables, each of which becomes the current performable in its turn. This method
        /// pushes a new performable onto the stack. Once all of the consumed performables have finished, the current
        /// performable will return to this one.
        /// Thus, as performables begin and end, the current performable stack will be pushed and popped many times.
        /// </para>
        /// </remarks>
        /// <param name="performable">The performable obeject which has begun</param>
        /// <param name="actor">The actor executing the performable</param>
        /// <param name="performancePhase">The performance phase in which the performable occurs</param>
        /// <seealso cref="EndPerformable(object, Actor)"/>
        /// <seealso cref="RecordFailureForCurrentPerformable(Exception)"/>
        public void BeginPerformable(object performable, Actor actor, string performancePhase)
        {
            var performableReport = new PerformableReport
            {
                PerformableType = performable.GetType().FullName,
                ActorName = actor.Name,
                PerformancePhase = performancePhase,
            };

            NewPerformableList.Add(performableReport);
            performableStack.Push(performableReport);
        }

        /// <summary>
        /// Enriches the current performable with information about a file asset.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method may be called many times for the same performable, or it might not be used at all.
        /// </para>
        /// </remarks>
        /// <param name="assetPath">The file path to the asset</param>
        /// <param name="assetSummary">The human readable summary of the asset</param>
        public void RecordAssetForCurrentPerformable(string assetPath, string assetSummary)
            => CurrentPerformable.Assets.Add(new PerformableAsset { FilePath = assetPath, FileSummary = assetSummary });

        /// <summary>
        /// Enriches the current performable with information about its result.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method is only applicable to performables which may return a result.
        /// </para>
        /// </remarks>
        /// <param name="result">The result of the performable</param>
        public void RecordResultForCurrentPerformable(object result)
        {
            CurrentPerformable.HasResult = true;
            CurrentPerformable.Result = valueFormatterProvider.FormatValue(result);
        }

        /// <summary>
        /// Indicates that the current performable has finished normally and that it should no longer be current.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method will 'pop' the current performable from the current performable stack.
        /// The current performable will become either the performable which directly consumed/composed the one which is ending,
        /// or perhaps this will lead to there being no current performable (if the ending performable was at the root of the
        /// performance).
        /// </para>
        /// </remarks>
        /// <param name="performable">The performable which is ending</param>
        /// <param name="actor">The actor which was executing the performable</param>
        /// <seealso cref="BeginPerformable(object, Actor, string)"/>
        public void EndPerformable(object performable, Actor actor)
        {
            CurrentPerformable.Report = performable is ICanReport reporter
                ? reporter.GetReportFragment(actor, formatter).FormattedFragment
                : string.Format(ReportStrings.FallbackReportFormat, actor.Name, performable.GetType().FullName);
            performableStack.Pop();
        }

        /// <summary>
        /// Records the current performable as a failure, due to an uncaught exception.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method will 'pop' the current performable from the current performable stack.
        /// The current performable will become either the performable which directly consumed/composed the one which is ending,
        /// or perhaps this will lead to there being no current performable (if the ending performable was at the root of the
        /// performance).
        /// </para>
        /// </remarks>
        /// <param name="exception">The exception which lead to the failure</param>
        public void RecordFailureForCurrentPerformable(Exception exception)
        {
            CurrentPerformable.Exception = exception.ToString();
            CurrentPerformable.ExceptionIsFromConsumedPerformable = exception is PerformableException;
            performableStack.Pop();
        }

#endregion

        /// <summary>
        /// Initialises a new instance of <see cref="PerformanceReportBuilder"/>.
        /// </summary>
        /// <param name="namingHierarchy">The naming hierarchy of the performance; see <see cref="IPerformance.NamingHierarchy"/></param>
        /// <param name="valueFormatterProvider">A value formatter factory</param>
        /// <param name="formatter">A report-fragment formatter</param>
        /// <exception cref="ArgumentNullException">If any parameter is <see langword="null" />.</exception>
        public PerformanceReportBuilder(List<IdentifierAndNameModel> namingHierarchy,
                                        IGetsValueFormatter valueFormatterProvider,
                                        IFormatsReportFragment formatter)
        {
            if (namingHierarchy is null)
                throw new ArgumentNullException(nameof(namingHierarchy));

            this.valueFormatterProvider = valueFormatterProvider ?? throw new ArgumentNullException(nameof(valueFormatterProvider));
            this.formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));

            report = new PerformanceReport
            {
                NamingHierarchy = namingHierarchy.ToList(),
            };
        }
    }
}