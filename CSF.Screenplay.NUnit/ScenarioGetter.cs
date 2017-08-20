using System;
namespace CSF.Screenplay.NUnit
{
  /// <summary>
  /// Helper type to provide access to the current scenario.
  /// </summary>
  public static class ScenarioGetter
  {
    /// <summary>
    /// Gets the current Screenplay scenario.
    /// </summary>
    /// <value>The scenario.</value>
    public static ScreenplayScenario Scenario { get; internal set; }
  }
}
