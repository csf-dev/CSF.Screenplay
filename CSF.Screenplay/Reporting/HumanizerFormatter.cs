using System;
using System.Linq;
using Humanizer;

namespace CSF.Screenplay.Reporting
{
    /// <summary>
    /// A formatter which makes use of the Humanizer: <see href="https://github.com/Humanizr/Humanizer"/> library to format dates, times &amp; time spans.
    /// </summary>
    /// <remarks>
    /// <para>
    /// In theory this formatter could do quite a lot more, as Humanizer's capabilities are much wider than just dates, times &amp; time spans.
    /// Since this is a preloaded/default formatter, I don't want to risk accidentally "humanising" values which are already human-readable,
    /// and mangling them in an unwanted way.
    /// </para>
    /// <para>
    /// Developers are welcome to create their own formatter implementations which make use of Humanizer (the dependency is already present)
    /// and which more closely target the values they'd like formatted.
    /// </para>
    /// </remarks>
    public class HumanizerFormatter : IValueFormatter
    {
        static readonly Type[] supportedTypes = {
            typeof(DateTime),
            typeof(DateTime?),
            typeof(TimeSpan),
            typeof(TimeSpan?),
        };

        /// <inheritdoc/>
        public bool CanFormat(object value)
        {
            if (value is null) return false;
            return supportedTypes.Contains(value.GetType());
        }

        /// <inheritdoc/>
        public string Format(object value)
        {
            if (value is DateTime dateTime)
                return dateTime.Humanize();
            if (value is TimeSpan timeSpan)
                return timeSpan.Humanize();
            if(TryCast<DateTime>(value, out var castDateTime))
                return castDateTime.Humanize();
            if(TryCast<TimeSpan>(value, out var castTimeSpan))
                return castTimeSpan.Humanize();

            throw new ArgumentException($"Unsupported parameter value: {value}", nameof(value));
        }

        static bool TryCast<T>(object value, out T typedValue)
        {
            try
            {
                typedValue = (T) value;
                return true;
            }
            catch
            {
                typedValue = default;
                return false;
            }
        }
    }
}