using System;
using System.Collections.Generic;

namespace CSF.Screenplay.ReportModel
{
    /// <summary>
    /// Model represents an <see cref="IPerformance"/> within a Screenplay report.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Like many models in this namespace, this type mimicks a first-class part of the Screenplay architecture.
    /// This model type is intended for use with the serialization process to JSON.
    /// Many of the properties of these types will correspond directly with the same-named properties on the original
    /// Screenplay architecture types.
    /// </para>
    /// </remarks>
    public class PerformanceReport
    {
        List<IdentifierAndNameModel> namingHierarchy = new List<IdentifierAndNameModel>();
        List<ReportableModelBase> reportables = new List<ReportableModelBase>();

        /// <summary>
        /// Corresponds with <see cref="IPerformance.NamingHierarchy"/>.
        /// </summary>
        public List<IdentifierAndNameModel> NamingHierarchy
        {
            get => namingHierarchy;
            set => namingHierarchy = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Corresponds with <see cref="IPerformance.PerformanceState"/>, showing the final outcome of the performance.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property contains the string representation of the value of <see cref="Performances.PerformanceState"/>.
        /// It is named 'Outcome' rather than 'PerformanceState' because the concept of state would refer to a current state, wheras a report
        /// is about something which happened in the past.
        /// Additionally, it is impossible for a report to indicate <see cref="Performances.PerformanceState.InProgress"/> or
        /// <see cref="Performances.PerformanceState.NotStarted"/> here.  The state will always correspond to one of the three terminal
        /// states for a performance.
        /// Thus, this property indicates the final outcome of the performance, rather than where it is 'right now'.
        /// </para>
        /// </remarks>
        public string Outcome { get; set; }

        /// <summary>
        /// Gets or set an ordered collection of the reportable events which occurred in the current performance.
        /// </summary>
        public List<ReportableModelBase> Reportables
        {
            get => reportables;
            set => reportables = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}