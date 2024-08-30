using System;

namespace CSF.Screenplay.Performables
{
    /// <summary>
    /// A type which may provide a <see cref="TimeSpan"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Many performables make use of time; this interface provides a common abstraction for
    /// objects that provide time spans.
    /// </para>
    /// </remarks>
    /// <seealso cref="TimeSpanBuilder{TOtherBuilder}"/>
    /// <seealso cref="TimeSpanBuilder"/>
    public interface IProvidesTimeSpan
    {
        /// <summary>
        /// Gets the <see cref="TimeSpan"/> which is exposed by the current instance.
        /// </summary>
        /// <returns>The time span</returns>
        TimeSpan GetTimeSpan();
    }
}
