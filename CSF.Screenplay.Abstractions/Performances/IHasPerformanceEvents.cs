using System;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay.Performances
{
    /// <summary>
    /// An object which has events which are significant to the progress of a Screenplay.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This object is used as an event publisher, such that events which originate from many separate objects
    /// may be aggregated to a single point of contact.  This way, consumers of those events have only a single object
    /// to which they need subscribe.
    /// There should only be a single instance of an object which implements this interface, for the lifetime
    /// of a Screenplay.
    /// </para>
    /// <para>
    /// This type is closely related to <see cref="IRelaysPerformanceEvents"/>.  This is the event publisher and
    /// <c>IRelaysPerformanceEvents</c> is event sink which collects them. Despite this, their APIs are not symmetrical,
    /// as many of the events published by this type are derived by subscribing to an <see cref="Actor"/> instance from
    /// the event sink.
    /// </para>
    /// </remarks>
    /// <seealso cref="IRelaysPerformanceEvents"/>
    public interface IHasPerformanceEvents
    {
        #region Screenplay

        /// <summary>
        /// Occurs when a Screenplay starts.
        /// </summary>
        event EventHandler ScreenplayStarted;

        /// <summary>
        /// Occurs when a Screenplay has ended.
        /// </summary>
        event EventHandler ScreenplayEnded;

        #endregion

        #region Performances

        /// <summary>
        /// Occurs when a <see cref="IPerformance"/> begins executing.
        /// </summary>
        event EventHandler<PerformanceEventArgs> PerformanceBegun;

        /// <summary>
        /// Occurs when a <see cref="IPerformance"/> has finished executing.
        /// </summary>
        event EventHandler<PerformanceFinishedEventArgs> PerformanceFinished;

        #endregion

        #region Performables

        /// <summary>
        /// Occurs when an <see cref="Actor"/> begins the execution of a performable object.
        /// </summary>
        event EventHandler<PerformableEventArgs> BeginPerformable;

        /// <summary>
        /// Occurs when an <see cref="Actor"/> ends the execution of a performable object.
        /// </summary>
        event EventHandler<PerformableEventArgs> EndPerformable;

        /// <summary>
        /// Occurs when an <see cref="Actor"/> receives a result from a perfperformable objectrmance.
        /// </summary>
        event EventHandler<PerformableResultEventArgs> PerformableResult;

        /// <summary>
        /// Occurs when a performable object fails with an exception.
        /// </summary>
        event EventHandler<PerformableFailureEventArgs> PerformableFailed;

        /// <summary>
        /// Occurs when an actor records the presence of a new file asset.
        /// </summary>
        event EventHandler<PerformableAssetEventArgs> RecordAsset;

        #endregion

        #region Actors

        /// <summary>
        /// Occurs when an <see cref="Actor"/> gains a new ability.
        /// </summary>
        event EventHandler<GainAbilityEventArgs> GainedAbility;

        /// <summary>
        /// Occurs when a new <see cref="Actor"/> is created and added to the <see cref="IPerformance"/>.
        /// </summary>
        event EventHandler<ActorEventArgs> ActorCreated;

        /// <summary>
        /// Occurs when an <see cref="Actor"/> is placed into the Spotlight of an <see cref="IStage"/>.
        /// </summary>
        event EventHandler<ActorEventArgs> ActorSpotlit;

        /// <summary>
        /// Occurs when the Spotlight of an <see cref="IStage"/> is 'turned off'; the <see cref="Actor"/> who is currently spotlit is removed without being replaced.
        /// </summary>
        event EventHandler<PerformanceScopeEventArgs> SpotlightTurnedOff;

        #endregion
    }
}