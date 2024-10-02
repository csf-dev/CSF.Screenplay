using CSF.Screenplay.Reporting;

namespace CSF.Screenplay
{
  /// <summary>
  /// A type which can provide a human-readable report fragment when it is used in a <see cref="IPerformance"/>.
  /// </summary>
  /// <remarks>
  /// <para>
  /// It is recommended for all types for Actions, Questions, Tasks (broadly "Performables") as well as
  /// Abilities to implement this interface.
  /// Implementing this interface permits the type to emit a human-readable value for when the type is
  /// used in a Performance.
  /// </para>
  /// <para>
  /// For Performables implementing this interface, the report fragment indicates that the performable has been
  /// executed in the Performance.
  /// For abilities which implement this interface, the report fragment is used when an Actor gains/is granted
  /// the ability.
  /// </para>
  /// </remarks>
  /// <seealso cref="IPerformable"/>
  /// <seealso cref="IPerformableWithResult"/>
  /// <seealso cref="IPerformableWithResult{TResult}"/>
  public interface ICanReport
  {
        /// <summary>
        /// Gets a fragment of a Screenplay report, specific to the execution (performables) or gaining
        /// (abilities) of the current instance, for the specified actor.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Implementers should return a string which indicates that the named actor is performing (present tense)
        /// the performable, for types which also implement a performable interface. For types which represent
        /// abilities, the implementer should return a string which indicates that the named actor is able to do
        /// something. In particular for abilities, to make them easily recognisable in reports, it helps to stick
        /// to the convention <c>{Actor name} is able to {Ability summary}</c>.
        /// </para>
        /// <para>
        /// For performables which return a value (Questions, or Tasks which behave like Questions), there is no
        /// need to include the returned value within the report fragment.  The framework will include the return
        /// value in the report and will format it via a different mechanism.
        /// </para>
        /// <para>
        /// Good report fragments are concise. Be aware that report fragments for Tasks (which are composed from other
        /// performables) do not need to go into detail about what they do. Users reading Screenplay reports are able
        /// to drill-down into Tasks to see what they are composed from, so if the user is curious as to what the
        /// task does, it is easy to discover.
        /// It is also strongly recommended to avoid periods (full stops) at the end of a report fragment.
        /// Whilst report fragments tend to be complete sentences, punctuation like this is distracting and reports
        /// are seldom presented as paragraphs of prose.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// For a performable which clicks a button (where the button itself has been constructor-injected into
        /// the performable instance), then a suitable return value might be a formatted string such as
        /// <c>{Actor name} clicks {Button}</c>, where the two placeholders indicated by braces: <c>{}</c> are
        /// substituted with the <paramref name="actor"/>'s <see cref="IHasName.Name"/> and a string representation of
        /// the button.
        /// </para>
        /// <para>
        /// For a performable which reads the temperature from a thermometer, a suitable return value might be a
        /// string in the format <c>{Actor name} reads the temperature</c>.
        /// </para>
        /// <para>
        /// For an ability which allows the actor to wash dishes then a suitable return value might be a string in the
        /// format <c>{Actor name} is able to wash the dishes</c>.
        /// </para>
        /// </example>
        /// <returns>A human-readable report fragment.</returns>
        /// <param name="actor">An actor for whom to write the report fragment</param>
        /// <param name="formatter">A report-formatting service</param>
        ReportFragment GetReportFragment(IHasName actor, IFormatsReportFragment formatter);
  }
}
