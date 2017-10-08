using System;
namespace CSF.Screenplay.Scenarios
{
  /// <summary>
  /// A type which can begin and end a screenplay scenario.
  /// </summary>
  public interface ICanBeginAndEndScenario : IScreenplayScenario
  {
    /// <summary>
    /// Marks the scenario as having begun (which may trigger event listeners).
    /// </summary>
    void Begin();

    /// <summary>
    /// Marks the scenario as having ended (which may trigger event listeners).
    /// </summary>
    void End(bool? success);
  }
}
