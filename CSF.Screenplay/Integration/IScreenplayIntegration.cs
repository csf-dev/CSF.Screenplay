using System;
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay.Integration
{
  /// <summary>
  /// Describes the API of a screenplay integration type.
  /// </summary>
  public interface IScreenplayIntegration
  {
    /// <summary>
    /// Ensures that all of the screenplay services are registered.
    /// This method is safe to be called many times, however it will only perform the actual registration once.
    /// </summary>
    void EnsureServicesAreRegistered();

    /// <summary>
    /// Executed once, before the first scenario in the test run is executed.  Note that
    /// all services must have already been registered prior to executing this method.
    /// </summary>
    void BeforeExecutingFirstScenario();

    /// <summary>
    /// Executed before each scenario in a test run.
    /// </summary>
    /// <param name="scenario">Scenario.</param>
    void BeforeScenario(ScreenplayScenario scenario);

    /// <summary>
    /// Executed after each scenario in a test run.
    /// </summary>
    /// <param name="scenario">Scenario.</param>
    /// <param name="success">If set to <c>true</c> success.</param>
    void AfterScenario(ScreenplayScenario scenario, bool success);

    /// <summary>
    /// Executed after the last scenario in a test run.
    /// </summary>
    void AfterExecutedLastScenario();

    /// <summary>
    /// Gets the scenario factory.
    /// </summary>
    /// <returns>The scenario factory.</returns>
    IScenarioFactory GetScenarioFactory();
  }
}
