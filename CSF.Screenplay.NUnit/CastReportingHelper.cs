using System;
using CSF.Screenplay.Actors;
using CSF.Screenplay.Reporting;

namespace CSF.Screenplay.NUnit
{
  /// <summary>
  /// Sets up subscriptions between a <see cref="ICast"/> and an <see cref="IReporter"/>.
  /// </summary>
  public class CastReportingHelper
  {
    readonly ICast cast;
    readonly IReporter reporter;
    readonly ScreenplayContext context;

    protected ICast Cast => cast;
    protected IReporter Reporter => reporter;
    protected ScreenplayContext Context => context;

    /// <summary>
    /// Sets up the subscriptions between the cast and the reporter.
    /// </summary>
    public virtual void SetupSubscriptions()
    {
      cast.ActorAdded += HandleActorAddedToCast;
      cast.ActorCreated += HandleActorCreatedInCast;
    }

    /// <summary>
    /// Handles the creation of a new actor within an <see cref="ICast"/> instance.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="args">Arguments.</param>
    protected virtual void HandleActorCreatedInCast(object sender, ActorEventArgs args)
    {
      SubscribeReporter(args.Actor);
    }

    /// <summary>
    /// Handles the addition of a new actor to an <see cref="ICast"/> instance.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="args">Arguments.</param>
    protected virtual void HandleActorAddedToCast(object sender, ActorEventArgs args)
    {
      // Intentional no-op, method is here for subclasses to override.
    }

    /// <summary>
    /// Gets the <see cref="Reporting.IReporter"/> from the current context and subscribes it to the given actor.
    /// </summary>
    /// <param name="actor">Actor.</param>
    protected virtual void SubscribeReporter(IActor actor)
    {
      reporter.Subscribe(actor);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.NUnit.CastReporter"/> class.
    /// </summary>
    /// <param name="cast">Cast.</param>
    /// <param name="reporter">Reporter.</param>
    public CastReportingHelper(ICast cast, IReporter reporter, ScreenplayContext context)
    {
      if(reporter == null)
        throw new ArgumentNullException(nameof(reporter));
      if(cast == null)
        throw new ArgumentNullException(nameof(cast));
      if(context == null)
        throw new ArgumentNullException(nameof(context));
      

      this.cast = cast;
      this.reporter = reporter;
      this.context = context;
    }
  }
}
