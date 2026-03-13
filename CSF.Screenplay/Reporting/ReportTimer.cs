using System;
using System.Diagnostics;
/// <summary>
/// Default implementation of <see cref="IMeasuresTime"/> which uses a <see cref="Stopwatch"/>.
/// </summary>
public sealed class ReportTimer : IMeasuresTime
{
    readonly Stopwatch stopwatch = new Stopwatch();

    /// <inheritdoc/>
    public DateTimeOffset BeginTiming()
    {
        if(stopwatch.IsRunning) throw new InvalidOperationException($"The {nameof(BeginTiming)} method may not be used more than once.");

        stopwatch.Start();
        return DateTimeOffset.Now;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if(stopwatch.IsRunning)
            stopwatch.Stop();
    }

    /// <inheritdoc/>
    public TimeSpan GetCurrentTime()
    {
        if(!stopwatch.IsRunning) throw new InvalidOperationException($"The {nameof(GetCurrentTime)} method may not be used before {nameof(BeginTiming)}.");
        return stopwatch.Elapsed;
    }
}