using System;
using System.Collections.Generic;

namespace CSF.Screenplay.ReportModel
{
    /// <summary>
    /// An implementation of <see cref="ReportableModelBase"/> which represents the execution of a <xref href="PerformableGlossaryItem?text=performable+item"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Reports about performables may themselves contain further reportables.
    /// This may create a hierarchical structure of reports which contain further reports, and so on.
    /// This represents the use of high-level performables which consume/compose lower-level performables.
    /// </para>
    /// </remarks>
    public class PerformableReport : ReportableModelBase
    {
        List<PerformableAsset> assets = new List<PerformableAsset>();
        List<ReportableModelBase> reportables = new List<ReportableModelBase>();

        /// <summary>
        /// Gets or sets the <see cref="Type.FullName"/> of the <xref href="PerformableGlossaryItem?text=performable+item"/> to which this report model relates.
        /// </summary>
        public string PerformableType { get; set; }

        /// <summary>
        /// Corresponds to the <see cref="Actors.PerformancePhase"/> to which the current reportable is part.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property contains the string representation of that performance phase.
        /// It is quite rare for this property to be unset (IE: <see langword="null" />), typically it is only unset for the
        /// initial creation/setup of Actors at the beginning of a performance.
        /// </para>
        /// </remarks>
        public string PerformancePhase { get; set; }

        /// <summary>
        /// Gets or sets a string representation of the result which was emitted by the corresponding performable.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property is only relevant if the original performable which lead to this report derived from either
        /// <see cref="IPerformableWithResult"/> or <see cref="IPerformableWithResult{TResult}"/>.
        /// For performables which do not emit a result, this value will always be null.
        /// </para>
        /// </remarks>
        /// <seealso cref="HasResult"/>
        public string Result { get; set; }
        
        /// <summary>
        /// Gets or sets a value that indicates whether or not the performable (which generated this report) emitted a result.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property will be <see langword="true" /> if the original performable which lead to this report derived from either
        /// <see cref="IPerformableWithResult"/> or <see cref="IPerformableWithResult{TResult}"/> (and did not fail).
        /// For performables which do not emit a result, this value will always be <see langword="false" />.
        /// </para>
        /// </remarks>
        /// <seealso cref="Result"/>
        public bool HasResult { get; set; }
        
        /// <summary>
        /// Gets or sets a value which is the string representation of any <see cref="System.Exception"/> which occurred, causing
        /// the performable to fail.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If this property is non-<see langword="null" /> then the performable has failed with an exception, and this property will
        /// contain the result of <see cref="Exception.ToString()"/> for that error.
        /// </para>
        /// <para>If this property is <see langword="null" /> then the performable did not raise an exception and completed successfully.</para>
        /// </remarks>
        public string Exception { get; set; }

        /// <summary>
        /// Gets a value which indicates whether or not the <see cref="Exception"/> is one which was originally thrown from a consumed performable.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If <see cref="Exception"/> is <see langword="null" /> then the value of this property is meaningless and undefined.
        /// If the exception is not null then - if this property is set to <see langword="true" /> then it means that exception which
        /// is recorded for this performable was originally thrown from a performable which was consumed by the current one.
        /// In other words, it indicates whether or not the reason for the current performable's error was because a consumed/child
        /// performable encountered an error.
        /// </para>
        /// <para>
        /// If, on the other hand, <see cref="Exception"/> is not <see langword="null" /> and this property value is <see langword="false" />
        /// then it indicates that the current performable is the original source of the error.
        /// </para>
        /// </remarks>
        public bool ExceptionIsFromConsumedPerformable { get; set; }

        /// <summary>
        /// Gets or sets a collection of the assets which were recorded by the current performable.
        /// </summary>
        public List<PerformableAsset> Assets
        {
            get => assets;
            set => assets = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets a collection of the reportable items which have been composed by the current performable.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This collection is commonly populated for <xref href="TaskGlossaryItem?text=task+performables"/>, because the nature of tasks is to
        /// consume and compose other performables.
        /// This collection can create a hierarchical structure of performables composing other performables.
        /// That structure could be deeply nested.
        /// </para>
        /// </remarks>
        public List<ReportableModelBase> Reportables
        {
            get => reportables;
            set => reportables = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}