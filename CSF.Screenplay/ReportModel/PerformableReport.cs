using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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
        string phase;

        /// <summary>
        /// Gets or sets type information for the <xref href="PerformableGlossaryItem?text=performable+item"/> to which this report model relates.
        /// </summary>
        /// <remarks>
        /// <para>
        /// In many cases this is equal to the <see cref="Type.FullName"/> of the performable class.
        /// However, it does not have to be. This is a human-readable value which is intended to convey useful information to a reader (who
        /// understands what a .NET type is) about the performable type.
        /// </para>
        /// <para>
        /// Consider a performable type which is an adapter class, that wraps a more specific performable.  The full name of the performable type itself
        /// might not be very useful, as it would only indicate a general-use adapter type.  This becomes even less useful when the adapter type is generic.
        /// Performable types which are general-use adapters, which would 'hide' a more specific (and useful) performable type may implement
        /// <see cref="IHasCustomTypeName"/>. Performable types which derive from the custom type name interface populate this value with the value retrieved from
        /// <see cref="IHasCustomTypeName.GetHumanReadableTypeName"/> instead of their type's full name..
        /// </para>
        /// </remarks>
        public string Type { get; set; }

        /// <summary>
        /// Corresponds to the <see cref="Actors.PerformancePhase"/> to which the current reportable is part.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property contains the string representation of that performance phase.
        /// This property should always have a non-<see langword="null" /> value at the top-level of a performance.
        /// At deeper levels, it is acceptable for this property to be null, since all consumed performables are presumed to inherit the performance
        /// phase of their consumer.
        /// </para>
        /// <para>
        /// This property normalises null, empty or whitespace-only values to <see langword="null"/>, so that this property may be skipped when it is empty.
        /// </para>
        /// </remarks>
        public string Phase
        {
            get => phase;
            set => phase = string.IsNullOrWhiteSpace(value) ? null : value;
        }

        /// <summary>
        /// Gets or sets the relative time at which this performable ended/finished.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property is expressed as an amount of time since the Screenplay began.  The beginning of the Screenplay is recorded in the
        /// report metadata, at <see cref="ReportMetadata.Timestamp"/>.
        /// </para>
        /// <para>
        /// Recall that it is quite normal for performances and thus reportable actions to occur in parallel.
        /// Do not be alarmed if it appears that unrelated performances are interleaved with regard to their timings.
        /// </para>
        /// </remarks>
        /// <seealso cref="ReportMetadata.Timestamp"/>
        /// <seealso cref="PerformanceReport.Started"/>
        /// <seealso cref="ReportableModelBase.Started"/>
        public TimeSpan Ended { get; set; }

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
        /// Gets a value which indicates whether or not the <see cref="Exception"/> is one which was originally thrown from a consumed performable, 'bubbling' up the call stack.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If <see cref="Exception"/> is <see langword="null" /> then the value of this property is meaningless and undefined.
        /// If the exception is not null then - if this property is set to <see langword="true" /> then it means that exception which
        /// is recorded for this performable was originally thrown from a performable which was consumed by the current one.
        /// </para>
        /// <para>
        /// When this is <see langword="true"/>, it means that the error did not originate within the current performable.  The current performable
        /// has failed only because a consumed/child performable encountered an error, and that error is 'bubbling' up the call stack, failing each consuming
        /// performable, until it reaches a <c>catch</c> block or the performance ends.
        /// </para>
        /// <para>
        /// If, on the other hand, <see cref="Exception"/> is not <see langword="null" /> and this property value is <see langword="false" />
        /// then it indicates that the current performable is the original source of the error.
        /// </para>
        /// </remarks>
        public bool ExceptionIsBubbling { get; set; }

        /// <summary>
        /// Gets or sets a collection of the assets which were recorded by the current performable.
        /// </summary>
        [JsonIgnore]
        public List<PerformableAsset> Assets
        {
            get => assets;
            set => assets = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets a representation of <see cref="Assets"/> which is designed for JSON serialization.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Notably, this property getter will return <see langword="null"/> if <see cref="Assets"/> is empty.
        /// This is desirable for serialization, because it allows us to ignore/skip this property when it's empty.
        /// For general use as a developer, use <see cref="Assets"/> instead.
        /// </para>
        /// </remarks>
        [JsonPropertyName(nameof(Assets))]
        public List<PerformableAsset> SerializableAssets
        {
            get => Assets.Count > 0 ? Assets : null;
            set => Assets = value ?? new List<PerformableAsset>();
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
        [JsonIgnore]
        public List<ReportableModelBase> Reportables
        {
            get => reportables;
            set => reportables = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets a representation of <see cref="Reportables"/> which is designed for JSON serialization.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Notably, this property getter will return <see langword="null"/> if <see cref="Reportables"/> is empty.
        /// This is desirable for serialization, because it allows us to ignore/skip this property when it's empty.
        /// For general use as a developer, use <see cref="Reportables"/> instead.
        /// </para>
        /// </remarks>
        [JsonPropertyName(nameof(Reportables))]
        public List<ReportableModelBase> SerializableReportables
        {
            get => Reportables.Count > 0 ? Reportables : null;
            set => Reportables = value ?? new List<ReportableModelBase>();
        }
    }
}