using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CSF.Screenplay.Actors;
using Humanizer;

namespace CSF.Screenplay.Reporting
{
    /// <summary>
    /// A formatter which makes use of the Humanizer: <see href="https://github.com/Humanizr/Humanizer"/> library to format a limited number of
    /// value types.
    /// </summary>
    /// <remarks>
    /// <para>
    /// In theory this formatter could do quite a lot more, as Humanizer's capabilities are much wider than the functionality used here.
    /// Since this is a preloaded/default formatter, it is intentionally limited so as to avoid accidentally "humanising" values which were
    /// already human-readable, mangling them in an unwanted way.
    /// </para>
    /// <para>
    /// The types supported by this formatter implementation are:
    /// </para>
    /// <list type="table">
    /// <listheader>
    /// <term>Type</term>
    /// <term>Note</term>
    /// </listheader>
    /// <item>
    /// <description><see cref="DateTime"/></description>
    /// <description>Formatted by Humanizer</description>
    /// </item>
    /// <item>
    /// <description><see cref="Nullable{T}"/> of <see cref="DateTime"/></description>
    /// <description>Formatted by Humanizer if not <see langword="null" />, otherwise <see cref="CanFormat(object)"/> will return <see langword="false" /></description>
    /// </item>
    /// <item>
    /// <description><see cref="TimeSpan"/></description>
    /// <description>Formatted by Humanizer</description>
    /// </item>
    /// <item>
    /// <description><see cref="Nullable{T}"/> of <see cref="TimeSpan"/></description>
    /// <description>Formatted by Humanizer if not <see langword="null" />, otherwise <see cref="CanFormat(object)"/> will return <see langword="false" /></description>
    /// </item>
    /// <item>
    /// <description>Implements <see cref="IEnumerable{T}"/> of <see cref="string"/></description>
    /// <description>Formatted by Humanizer into a comma-separated list</description>
    /// </item>
    /// </list>
    /// <para>
    /// Developers are welcome to create their own formatter implementations which make use of Humanizer (the dependency is already present)
    /// and which more closely target the values they'd like formatted.
    /// This class is intentionally limited to formatting values which are unlikely to be controversial.
    /// </para>
    /// </remarks>
    public class HumanizerFormatter : IValueFormatter
    {
        static readonly MethodInfo tryHumanizeAsEnumOpenGeneric = typeof(HumanizerFormatter).GetMethod(nameof(TryHumanizeAsEnum), BindingFlags.Static | BindingFlags.NonPublic);

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
            var objectType = value.GetType();

            if (supportedTypes.Contains(objectType)) return true;
            if (typeof(IEnumerable<string>).IsAssignableFrom(objectType)) return true;
            if (objectType.IsEnum) return true;
            return false;
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
            if(value is IEnumerable<string> stringCollection)
                return stringCollection.Humanize();
            if (TryHumanizeAsEnumNonGeneric(value, out var humanized))
                return humanized;

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

        static bool TryHumanizeAsEnumNonGeneric(object value, out string huamnized)
        {
            huamnized = null;
            if (value is null) return false;
            var genericMethpd = tryHumanizeAsEnumOpenGeneric.MakeGenericMethod(value.GetType());
            var result = (string)genericMethpd.Invoke(null, new object[] { value });
            huamnized = result;
            return result != null;
        }

        static string TryHumanizeAsEnum<T>(object value) where T : Enum
        {
            try
            {
                var typedValue = (T) value;
                return typedValue.Humanize();
            }
            catch
            {
                return null;
            }
        }
    }
}