﻿using System;
using System.Collections.Generic;
using CSF.FlexDi;
using CSF.FlexDi.Builders;
using CSF.Screenplay.Scenarios;

namespace CSF.Screenplay.Integration
{
  /// <summary>
  /// Exposes a number of collections of callbacks, which are used to configure the integration between
  /// Screenplay and a testing framework.
  /// </summary>
  public interface IIntegrationConfigBuilder
  {
    /// <summary>
    /// Gets the callbacks which are used to register Screenplay-related services.
    /// </summary>
    /// <value>The service registrations.</value>
    ServiceRegistrations ServiceRegistrations { get; }

    /// <summary>
    /// Gets a collection of callbacks which are executed before the first scenario in a test run.
    /// </summary>
    /// <value>The before first scenario.</value>
    IList<Action<IProvidesTestRunEvents,IResolvesServices>> BeforeFirstScenario { get; }

    /// <summary>
    /// Gets a collection of callbacks which are executed after the last scenario in a test run.
    /// </summary>
    /// <value>The after last scenario.</value>
    IList<Action<IResolvesServices>> AfterLastScenario { get; }

    /// <summary>
    /// Gets a collection of callbacks which are executed before each scenario.
    /// </summary>
    /// <value>The before scenario.</value>
    IList<Action<IScenario>> BeforeScenario { get; }

    /// <summary>
    /// Gets a collection of callbacks which are executed after each scenario.
    /// </summary>
    /// <value>The after scenario.</value>
    IList<Action<IScenario>> AfterScenario { get; }
  }
}
