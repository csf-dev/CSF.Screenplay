using System;
using CSF.Screenplay.Reporting;
using CSF.Screenplay.Selenium.Resources;

namespace CSF.Screenplay.Selenium.Elements
{
    /// <summary>
    /// A value formatter for <see cref="Lazy{T}"/> of <see cref="SeleniumElement"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Importantly this handles errors which may be raised when resolving the element from the Web Driver.
    /// </para>
    /// </remarks>
    public class LazySeleniumElementFormatter : IValueFormatter
    {
        /// <inheritdoc/>
        public bool CanFormat(object value) => value is Lazy<SeleniumElement> || value is Lazy<IHasSearchContext>;

        /// <inheritdoc/>
        public string FormatForReport(object value)
        {
            if (value is Lazy<SeleniumElement> lazyElement) return GetName(lazyElement);
            if (value is Lazy<IHasSearchContext> lazySearch) return GetName(lazySearch);
            throw new ArgumentException($"The value must be either a lazy {nameof(SeleniumElement)} or a lazy {nameof(IHasSearchContext)}");
        }

        internal static string GetName(Lazy<SeleniumElement> lazyElement)
            => GetName(e => e.Value.Name, lazyElement);

        internal static string GetName(Lazy<IHasSearchContext> lazySearch)
            => GetName(e => e.Value.Name, lazySearch);

        static string GetName<T>(Func<Lazy<T>, string> getter, Lazy<T> lazy) where T : IHasName
        {
            try
            {
                return getter(lazy);
            }
            catch(TargetNotFoundException)
            {
                return MessageResources.NonExistentElement;
            }
            catch
            {
                return MessageResources.UnknownElement;
            }
        }
    }
}