using System;
using System.Collections.Generic;
using System.Globalization;
using CSF.Screenplay.Performances;
using CSF.Screenplay.Reporting;

namespace CSF.Screenplay
{
    /// <summary>
    /// Options model which permits the customization/configuration of Screenplay in DI.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Developer note:  In an ideal world, this type would be registered into DI via the Options pattern:
    /// <see href="https://learn.microsoft.com/en-us/dotnet/core/extensions/options"/>.
    /// Unfortunately, the BoDi DI container used by SpecFlow (which I wish to support with Screenplay) does not support
    /// the appropriate methods/logic to register the neccesary services for Options.
    /// This is why this object uses a somewhat homebrew version of options, without making use of the official libraries.
    /// </para>
    /// </remarks>
    public sealed class ScreenplayOptions
    {
        /// <summary>
        /// Gets a collection of concrete <see cref="System.Type"/> which implement <see cref="IValueFormatter"/>, which will be used
        /// to format values which are to appear in Screenplay reports.
        /// </summary>
        /// <remarks>
        /// <para>
        /// As noted in the documentation for <see cref="IFormatterRegistry"/>, the types in this collection are considered for use in reverse-collection-order.
        /// In other words, they will be tested using <see cref="IValueFormatter.CanFormat(object)"/> from last-to-first in this collection.
        /// Thus, generalized formatters should be placed at the beginning of this collection, where more specialized formatters should be placed toward the end.
        /// </para>
        /// <para>
        /// Make use of <see cref="ICollection{T}.Add(T)"/> to add new formatters to the end of this collection.
        /// It comes pre-loaded with three generalised formatters by default, in the following order.
        /// </para>
        /// <list type="number">
        /// <item><description><see cref="ToStringFormatter"/> - a default/fallback implementation which may format any value at all</description></item>
        /// <item><description><see cref="HumanizerFormatter"/> - a formatter for dates, times &amp; time spans which uses the Humanizer library</description></item>
        /// <item><description><see cref="NameFormatter"/> - which formats values that implement <see cref="IHasName"/> by emitting their name</description></item>
        /// <item><description><see cref="FormattableFormatter"/> - which formats values that implement <see cref="IFormattableValue"/></description></item>
        /// </list>
        /// <para>
        /// There is no need to register/add any types listed in this registry into dependency injection.
        /// The methods which accept a configuration action of <see cref="ScreenplayOptions"/> will iterate through this collection and add every one of the
        /// implementation types found as transient-lifetime services in dependency injection.
        /// </para>
        /// </remarks>
        public IFormatterRegistry ValueFormatters { get; } = new ValueFormatterRegistry
            {
                typeof(ToStringFormatter),
                typeof(HumanizerFormatter),
                typeof(NameFormatter),
                typeof(FormattableFormatter),
            };

        /// <summary>
        /// Gets a file system path at which a Screenplay report file will be written.
        /// </summary>
        /// <remarks>
        /// <para>
        /// As a <see cref="Screenplay"/> executes each <see cref="IPerformance"/>, it accumulates data relating to those performances, via its reporting
        /// mechanism. This information is then written to a JSON-formatted report file, which is saved at the path specified by this property.
        /// Once the Screenplay has completed this file may be inspected, converted into a different format and otherwise used to learn-about and diagnose the
        /// Screenplay.
        /// </para>
        /// <para>
        /// If this value is set to a relative file path, then it will be relative to the current working directory.
        /// If using Screenplay with a software testing integration, then this directory might not be easily determined.
        /// </para>
        /// <para>
        /// The default value for this property is a relative file path in the current working directory, using the filename <c>ScreenplayReport_[timestamp].json</c>
        /// where <c>[timestamp]</c> is replaced by the current UTC date &amp; time in ISO 8601 format.  A sample of a Screenplay Report filename using this default
        /// path is <c>ScreenplayReport_2024-10-04T19:23:45Z.json</c>.
        /// </para>
        /// <para>
        /// If this property is set to <see langword="null" />, or an empty/whitespace-only string, or if the path is not writable, then the reporting functionality
        /// will be disabled and no report will be written.
        /// </para>
        /// </remarks>
        public string ReportPath { get; set; } = $"ScreenplayReport_{DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture)}.json";

        /// <summary>
        /// An optional callback/action which exposes the various <see cref="IHasPerformanceEvents"/> which may be subscribed-to in order to be notified
        /// of the progress of a screenplay.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The implementation of <see cref="IHasPerformanceEvents"/> is an event publisher which emits notifications when key evens occur during the lifetime
        /// of a <see cref="Screenplay"/> and its performances: <see cref="IPerformance"/>.
        /// If you wish, you may subscribe to these events from your own logic in order to develop new functionality or extend Screenplay.
        /// </para>
        /// <para>
        /// There is no need to add an explicit subscription to any events for the reporting infrastructure.
        /// Screenplay will automatically subscribe to this object from the reporting mechanism, unless the value of <see cref="ReportPath"/> means that
        /// reporting is disabled.
        /// </para>
        /// </remarks>
        public Action<IHasPerformanceEvents> PerformanceEventsConfig { get; set; }
    }
}