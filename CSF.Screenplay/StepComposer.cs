using System;
using CSF.Screenplay.Actors;

namespace CSF.Screenplay
{
  /// <summary>
  /// A convenience type (for readability of tests) which helps 'phrase' the test method in a meaningful manner.
  /// </summary>
  /// <remarks>
  /// <para>
  /// It is recommended to use the functionality of this type via a <c>using static CSF.Screenplay.StepComposer;</c>
  /// declaration at the top of your test steps source file. Typical usage looks like the following:
  /// </para>
  /// <code>
  /// Given(joe).WasAbleTo(takeOutTheTrash);
  /// </code>
  /// <para>
  /// The <c>Given</c> method does nothing more than down-cast the <see cref="Actor"/> instance to an
  /// <see cref="IGivenActor"/>, but whilst it performs little functional benefit, it makes the test more readable by
  /// making it clear that this is a "given" (precondition) testing step.
  /// </para>
  /// </remarks>
  public static class StepComposer
  {
    /// <summary>
    /// Returns the actor instance, as an <see cref="IGivenActor"/>, in order to perform precondition actions.
    /// </summary>
    /// <param name="actor">The actor.</param>
    public static IGivenActor Given(IActor actor) => actor;

    /// <summary>
    /// Returns the actor instance, as an <see cref="IWhenActor"/>, in order to perform actions which exercise the
    /// application.
    /// </summary>
    /// <param name="actor">The actor.</param>
    public static IWhenActor When(IActor actor) => actor;

    /// <summary>
    /// Returns the actor instance, as an <see cref="IThenActor"/>, in order to perform actions which asserts that
    /// the desired outcome has come about.
    /// </summary>
    /// <param name="actor">The actor.</param>
    public static IThenActor Then(IActor actor) => actor;
  }
}
