using System;

/// <summary>
/// An object which acts as a stopwatch, intended for used in providing timing data for reports.
/// </summary>
public interface IMeasuresTime : IDisposable
{
    /// <summary>
    /// Begins the timer, recording/tracking time.
    /// </summary>
    /// <returns>The current date &amp; time, at the point when timing began.</returns>
    /// <exception cref="InvalidOperationException">If this method is used more than once upon the same object instance.</exception>
    DateTimeOffset BeginTiming();

    /// <summary>
    /// Gets the amount of time (wall clock time) which has elapsed since <see cref="BeginTiming"/> was executed.
    /// </summary>
    /// <returns>A timespan which is the time elapsed since timing began.</returns>
    /// <exception cref="InvalidOperationException">If this method is used before <see cref="BeginTiming"/> has been executed.</exception>
    TimeSpan GetCurrentTime();
}
