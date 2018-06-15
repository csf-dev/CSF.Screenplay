﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace CSF.Screenplay.Reporting.Models
{
  /// <summary>
  /// Represents a single feature within a report (a logical grouping of scenarios - in NUnit terminology a TestFixture).
  /// </summary>
  public class Feature : IFeature
  {
    IdAndName name;

    /// <summary>
    /// Gets or sets naming information for this feature.
    /// </summary>
    /// <value>The feature name.</value>
    public IdAndName Name
    {
      get => name;
      set => name = value ?? new IdAndName();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.Screenplay.Reporting.Models.Feature"/> class.
    /// </summary>
    public Feature()
    {
      name = new IdAndName();
    }
  }
}