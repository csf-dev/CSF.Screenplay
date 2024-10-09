using System;
using Microsoft.Extensions.DependencyInjection;

namespace CSF.Screenplay.Reporting
{
    /// <summary>
    /// Implementation of <see cref="IGetsValueFormatter"/> which uses dependency injection services.
    /// </summary>
    public class ValueFormatterProvider : IGetsValueFormatter
    {
        readonly IServiceProvider services;
        readonly IFormatterRegistry registry;

        /// <inheritdoc/>
        public IValueFormatter GetValueFormatter(object value)
        {
            for(var i = registry.Count - 1; i >= 0; i--)
            {
                var formatter = (IValueFormatter) services.GetRequiredService(registry[i]);
                if (formatter.CanFormat(value)) return formatter;
            }

            throw new InvalidOperationException($"No suitable implementation of {nameof(IValueFormatter)} could be found for a value of type {value?.GetType().FullName ?? "<null>"}");
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ValueFormatterProvider"/>.
        /// </summary>
        /// <param name="services">A service provider</param>
        /// <param name="registry">A registry of the available formatter types</param>
        /// <exception cref="ArgumentNullException">If any parameter is <see langword="null" />.</exception>
        public ValueFormatterProvider(IServiceProvider services, IFormatterRegistry registry)
        {
            this.services = services ?? throw new ArgumentNullException(nameof(services));
            this.registry = registry ?? throw new ArgumentNullException(nameof(registry));
        }
    }
}