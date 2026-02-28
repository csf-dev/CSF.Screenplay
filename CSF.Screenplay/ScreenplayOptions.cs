using System;
using System.Collections.Generic;
using System.Globalization;
using CSF.Screenplay.Performances;
using CSF.Screenplay.Reporting;
using Microsoft.Extensions.DependencyInjection;

namespace CSF.Screenplay
{
    /// <summary>
    /// Options model which permits the customization/configuration of Screenplay in DI.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Developer note:  In an ideal world, this type would be registered into DI via the Options pattern:
    /// <see href="https://learn.microsoft.com/en-us/dotnet/core/extensions/options"/>.
    /// Unfortunately, the BoDi DI container used by Reqnroll (which I wish to support with Screenplay) does not support
    /// the appropriate methods/logic to register the neccesary services for Options.
    /// This is why this object uses a somewhat homebrew version of options, without making use of the official libraries.
    /// </para>
    /// <para>
    /// Reqnroll is presently investigating replacing the BoDi container, and if it is someday replaced by MSDI, then
    /// <see href="https://github.com/csf-dev/CSF.Screenplay/issues/292">issue #292 is available to make that switch
    /// to the options pattern</see>/
    /// </para>
    /// </remarks>
    public sealed class ScreenplayOptions
    {
        /// <summary>
        /// Gets a collection of concrete <see cref="Type"/> which implement <see cref="IValueFormatter"/>, which will be used
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
        /// Gets a file system directory path at which a Screenplay report will be written.
        /// </summary>
        /// <remarks>
        /// <para>
        /// As a <see cref="Screenplay"/> executes each <see cref="IPerformance"/>, it accumulates data relating to those performances, via its reporting
        /// mechanism. This information is then written to a JSON-formatted report file, which is saved into a directory specified by this property.
        /// Once the Screenplay has completed the file may be inspected, converted into a different format and otherwise used to learn-about and diagnose the
        /// Screenplay.
        /// </para>
        /// <para>
        /// This value must indicate a directory, and not a file path, as a Screenplay Report may comprise of many files.
        /// If this value is set to a relative path, then it will be relative to the current working directory.
        /// If using Screenplay with a software testing integration, then the current working directory might not be easily determined.
        /// It is strongly recommended that each Screenplay run should create its own directory, so files for the same report are kept together.
        /// As such, it is advised that the directory name should contain some form of time-based value which will differ upon each run.
        /// </para>
        /// <para>
        /// The default value for this property is a relative directory path in the current working directory, using the format <c>ScreenplayReport_[timestamp]</c>.
        /// The <c>[timestamp]</c> portion is replaced by the current UTC date &amp; time in a format which is similar to ISO 8601, except that the <c>:</c> characters
        /// separating the hours, minutes and second are omitted.  This is because they are typically not legal path characters.  A sample of a Screenplay Report path using
        /// this convention is <c>ScreenplayReport_2024-10-04T192345Z</c>.
        /// </para>
        /// <para>
        /// If this property is set to <see langword="null" />, or an empty/whitespace-only string, or if the path is not writable, then the reporting functionality
        /// will be disabled and no report will be written.
        /// </para>
        /// <para>
        /// At runtime, do not read this value directly; instead use an implementation of <see cref="IGetsReportPath"/> service to get the report path.
        /// </para>
        /// </remarks>
        public string ReportPath { get; set; } = $"ScreenplayReport_{DateTime.UtcNow.ToString("yyyy-MM-ddTHHmmssZ", CultureInfo.InvariantCulture)}";

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

        /// <summary>
        /// Gets an ordered collection of actions which should be executed when the <see cref="Screenplay"/> begins, before the first
        /// <see cref="IPerformance"/> starts.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Each of the actions in this configuration parameter are executed when the <see cref="Screenplay.BeginScreenplay"/> method is invoked.
        /// </para>
        /// <para>
        /// By default this collection contains one item.
        /// This will initialise an instance of <see cref="IReporter"/> and subscribe it to the event bus: <see cref="IHasPerformanceEvents"/>,
        /// which will activate Screenplay reporting.
        /// </para>
        /// <para>
        /// You may add further callbacks if you wish, to extend Screenplay; they are executed in the order in which they appear in this collection.
        /// </para>
        /// </remarks>
        public List<Action<IServiceProvider>> OnBeginScreenplayActions { get; } = new List<Action<IServiceProvider>>
            {
                services => {
                    var reporter = services.GetRequiredService<IReporter>();
                    var eventBus = services.GetRequiredService<IHasPerformanceEvents>();
                    reporter.SubscribeTo(eventBus);
                },
            };
        
        /// <summary>
        /// Gets an ordered collection of actions which should be executed when the <see cref="Screenplay"/> finished, after the last
        /// <see cref="IPerformance"/> ends.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Each of the actions in this configuration parameter are executed when the <see cref="Screenplay.CompleteScreenplay"/> method is invoked.
        /// </para>
        /// <para>
        /// By default this collection contains one item, which disposes of the reporting infrastructure.
        /// </para>
        /// <para>
        /// You may add further callbacks if you wish, to extend Screenplay; they are executed in the order in which they appear in this collection.
        /// </para>
        /// <para>
        /// Be very wary of the use of this property.  This is because it is usual for the end of a Screenplay to
        /// be triggered by the unloading of the the assemblies in the current process.  If the logic triggered by this is nontrivial, it's very likely
        /// that it will be terminated early by the ending of the overall process.
        /// </para>
        /// </remarks>
        public List<Action<IServiceProvider>> OnEndScreenplayActions { get; } = new List<Action<IServiceProvider>>
            {
                services => services.GetRequiredService<IReporter>().Dispose(),
            };


    }
}