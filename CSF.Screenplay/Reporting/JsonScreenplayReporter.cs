using System;
using System.IO;
using System.Text.Json;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Performances;
using CSF.Screenplay.ReportModel;

namespace CSF.Screenplay.Reporting
{
    /// <summary>
    /// An object which subscribes/listens to the events of <see cref="IHasPerformanceEvents"/> and which
    /// produces a JSON-formatted report from them.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Note that instances of this class are not reusable; this is a stateful type.  Each instance builds and writes a
    /// single report file which corresponds to the execution of a single instance of <see cref="Screenplay"/>.
    /// To write a second report, or to write a report about a different Screenplay, please create a new instance of this type.
    /// </para>
    /// <para>
    /// This type works by accumulating reporting information about <see cref="IPerformance"/> instances using <see cref="ScreenplayReportBuilder"/>.
    /// Each of these performances may themselves accumulate information from the various events which occur during the performance lifetime.
    /// Once a performance is ended (success or otherwise), the completed <see cref="PerformanceReport"/> may be retrieved from the screenplay
    /// report builder.  This is then written &amp; flushed to the JSON file.
    /// </para>
    /// <para>
    /// The writing of JSON must be performed in a thread-safe manner, recall that multiple performances could be ongoing at the same time,
    /// in parallel.
    /// </para>
    /// <para>
    /// Additionally, it is important to write the JSON as-we-progress, rather than waiting until the end of the <see cref="Screenplay"/>, or the
    /// <see cref="IHasPerformanceEvents.ScreenplayEnded"/> event.  That's because in many scenarios, the end of the Screenplay is triggered
    /// by the assembly-unload events for the whole process.  This is particularly true for software testing frameworks.
    /// If we waited to write all of the JSON at the very end of the process then it is very likely that the write will be interrupted.
    /// Logic triggered from unload event handlers is permitted only a tiny amount of system compute resource and is inappropriate for performing
    /// anything substantial.
    /// This is why this type writes each performance to JSON as it completes, essentially streaming the information to the JSON file.
    /// This means that all that is left to be done when the Screenplay completes is to write a few closing symbols and then close the file.
    /// </para>
    /// </remarks>
    public sealed class JsonScreenplayReporter : IDisposable
    {
        readonly ScreenplayReportBuilder builder;
        readonly Utf8JsonWriter jsonWriter;
        readonly object syncRoot = new object();

        /// <summary>
        /// Subscribes to the events emitted by the specified Screenplay event notifier.
        /// </summary>
        /// <remarks>
        /// <para>
        /// As events are received, the JSON object model will be accumulated and written incrementally to file.
        /// </para>
        /// </remarks>
        /// <param name="events">A Screenplay event notifier</param>
        public void SubscribeTo(IHasPerformanceEvents events)
        {
            if (events is null)
                throw new ArgumentNullException(nameof(events));

            events.ScreenplayStarted += OnScreenplayStarted;
            events.ScreenplayEnded += OnScreenplayEnded;
            events.PerformanceBegun += OnPerformanceBegun;
            events.PerformanceFinished += OnPerformanceFinished;
            events.BeginPerformable += OnBeginPerformable;
            events.EndPerformable += OnEndPerformable;
            events.PerformableResult += OnPerformableResult;
            events.PerformableFailed += OnPerformableFailed;
            events.RecordAsset += OnRecordAsset;
            events.ActorCreated += OnActorCreated;
            events.GainedAbility += OnGainedAbility;
            events.ActorSpotlit += OnActorSpotlit;
            events.SpotlightTurnedOff += OnSpotlightTurnedOff;
        }
        /// <summary>
        /// Unsubscribes from the specified Screenplay event notifier.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Use this method only after the event notifier has emitted the <see cref="IHasPerformanceEvents.ScreenplayEnded"/> event.
        /// If this reporter unsubscribes from Screenplay events before the Screenplay has ended then the results are undefined.
        /// This could lead to a corrupt report file.
        /// </para>
        /// </remarks>
        /// <param name="events">A Screenplay event notifier</param>
        public void UnsubscribeFrom(IHasPerformanceEvents events)
        {
            events.ScreenplayStarted -= OnScreenplayStarted;
            events.ScreenplayEnded -= OnScreenplayEnded;
            events.PerformanceBegun -= OnPerformanceBegun;
            events.PerformanceFinished -= OnPerformanceFinished;
            events.BeginPerformable -= OnBeginPerformable;
            events.EndPerformable -= OnEndPerformable;
            events.PerformableResult -= OnPerformableResult;
            events.PerformableFailed -= OnPerformableFailed;
            events.RecordAsset -= OnRecordAsset;
            events.ActorCreated -= OnActorCreated;
            events.GainedAbility -= OnGainedAbility;
            events.ActorSpotlit -= OnActorSpotlit;
            events.SpotlightTurnedOff -= OnSpotlightTurnedOff;
        }

        /// <inheritdoc/>
        public void Dispose() => jsonWriter.Dispose();

        #region event handlers

        void OnSpotlightTurnedOff(object sender, PerformanceScopeEventArgs e)
            => GetPerformanceBuilder(e).SpotlightTurnedOff();

        void OnActorSpotlit(object sender, ActorEventArgs e)
            => GetPerformanceBuilder(e).ActorSpotlit(e.Actor);

        void OnGainedAbility(object sender, GainAbilityEventArgs e)
            => GetPerformanceBuilder(e).ActorGainedAbility(e.Actor, e.Ability);

        void OnActorCreated(object sender, ActorEventArgs e)
            => GetPerformanceBuilder(e).ActorCreated(e.Actor);

        void OnRecordAsset(object sender, PerformableAssetEventArgs e)
            => GetPerformanceBuilder(e).RecordAssetForCurrentPerformable(e.FilePath, e.FileSummary);

        void OnPerformableFailed(object sender, PerformableFailureEventArgs e)
            => GetPerformanceBuilder(e).RecordFailureForCurrentPerformable(e.Exception);

        void OnPerformableResult(object sender, PerformableResultEventArgs e)
            => GetPerformanceBuilder(e).RecordResultForCurrentPerformable(e.Result);

        void OnEndPerformable(object sender, PerformableEventArgs e)
            => GetPerformanceBuilder(e).EndPerformable(e.Performable, e.Actor);

        void OnBeginPerformable(object sender, PerformableEventArgs e)
            => GetPerformanceBuilder(e).BeginPerformable(e.Performable, e.Actor, GetPhaseString(e.Phase));

        void OnPerformanceBegun(object sender, PerformanceEventArgs e)
            => builder.BeginPerformance(e.PerformanceIdentity, e.NamingHierarchy);

        void OnScreenplayStarted(object sender, EventArgs e)
        {
            jsonWriter.WriteStartObject();
            var metadata = new ReportMetadata();
            jsonWriter.WritePropertyName("Metadata");
            JsonSerializer.Serialize(jsonWriter, metadata);
            jsonWriter.WriteStartArray("Performances");
            jsonWriter.Flush();
        }

        private void OnScreenplayEnded(object sender, EventArgs e)
        {
            jsonWriter.WriteEndArray();
            jsonWriter.WriteEndObject();
            jsonWriter.Flush();
        }

        void OnPerformanceFinished(object sender, PerformanceFinishedEventArgs e)
        {
            var report = builder.EndPerformanceAndGetReport(e.PerformanceIdentity, e.Success);
            lock(syncRoot)
            {
                JsonSerializer.Serialize(jsonWriter, report);
                jsonWriter.Flush();
            }
        }

#endregion

        PerformanceReportBuilder GetPerformanceBuilder(PerformanceScopeEventArgs e)
            => builder.GetPerformanceBuilder(e.PerformanceIdentity);

        static string GetPhaseString(PerformancePhase phase)
            => phase == PerformancePhase.Unspecified ? string.Empty : phase.ToString();


        /// <summary>
        /// Initializes a new instance of <see cref="JsonScreenplayReporter"/> for a specified file path.
        /// </summary>
        /// <param name="writeStream">The stream to which the JSON report shall be written.</param>
        /// <param name="builder">The Screenplay report builder</param>
        /// <exception cref="ArgumentNullException">If <paramref name="writeStream"/> is <see langword="null" />.</exception>
        public JsonScreenplayReporter(Stream writeStream, ScreenplayReportBuilder builder)
        {
            if (writeStream is null)
                throw new ArgumentNullException(nameof(writeStream));

            jsonWriter = new Utf8JsonWriter(writeStream);
            this.builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }
    }
}