﻿using System;
namespace CSF.Screenplay.Builders
{
  /// <summary>
  /// Type which provides a <c>System.TimeSpan</c>.
  /// </summary>
  public interface IProvidesTimespan
  {
    /// <summary>
    /// Gets the timespan.
    /// </summary>
    /// <returns>The timespan.</returns>
    TimeSpan GetTimespan();
  }
}
