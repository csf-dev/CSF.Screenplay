using System.Collections.Generic;
using CSF.Screenplay.Reporting;

namespace CSF.Screenplay
{
    /// <summary>
    /// Options model which permits the customization/configuration of Screenplay in DI.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Developer note:  In an ideal world, this type would be registered into DI via the Options pattern:
    /// <see href="https://learn.microsoft.com/en-us/dotnet/core/extensions/options"/>.
    /// Unfortunately, the BoDi DI container used by SpecFlow (which I wish to support with Screenplay) does not support
    /// the appropriate methods/logic to register the neccesary services for Options.
    /// This is why this object uses a somewhat homebrew version of options, without making use of the official libraries.
    /// </para>
    /// </remarks>
    public sealed class ScreenplayOptions
    {
        /// <summary>
        /// Gets a collection of concrete <see cref="System.Type"/> which implement <see cref="IValueFormatter"/>, which will be used
        /// to format values which are to appear in Screenplay reports.
        /// </summary>
        /// <remarks>
        /// <para>
        /// As noted in the documentation for <see cref="IFormatterRegistry"/>, the types in this collection are considered for use in reverse-collection-order.
        /// In other words, they will be tested using <see cref="IValueFormatter.CanFormat(object)"/> from last-to-first in this collection.
        /// Thus, generalized formatters should be placed at the beginning of this collection, where more specialized formatters should be placed toward the end.
        /// </para>
        /// <para>
        /// Make use of <see cref="ICollection{T}.Add(T)"/> to add new formatters to the end of this collection.
        /// It comes pre-loaded with three generalised formatters by default, in the following order.
        /// </para>
        /// <list type="number">
        /// <item><description><see cref="ToStringFormatter"/> - a default/fallback implementation which may format any value at all</description></item>
        /// <item><description><see cref="NameFormatter"/> - which formats values that implement <see cref="IHasName"/> by emitting their name</description></item>
        /// <item><description><see cref="FormattableFormatter"/> - which formats values that implement <see cref="IFormattableValue"/></description></item>
        /// </list>
        /// <para>
        /// There is no need to register/add any types listed in this registry into dependency injection.
        /// The methods which accept a configuration action of <see cref="ScreenplayOptions"/> will iterate through this collection and add every one of the
        /// implementation types found as transient-lifetime services in dependency injection.
        /// </para>
        /// </remarks>
        public IFormatterRegistry ValueFormatters { get; } = new ValueFormatterRegistry
            {
                typeof(ToStringFormatter),
                typeof(NameFormatter),
                typeof(FormattableFormatter),
            };
    }
}