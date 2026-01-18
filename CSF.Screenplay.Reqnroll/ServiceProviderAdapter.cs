using System;
using BoDi;

namespace CSF.Screenplay
{
    /// <summary>
    /// Adapter class which allows a SpecFlow/BoDi <c>IObjectContainer</c> to be used as an <see cref="IServiceProvider"/>.
    /// </summary>
    public class ServiceProviderAdapter : IServiceProvider
    {
        readonly IObjectContainer wrapped;

        /// <inheritdoc/>
        public object GetService(Type serviceType) => wrapped.Resolve(serviceType);

        /// <summary>
        /// Initialises an instance of <see cref="ServiceProviderAdapter"/>.
        /// </summary>
        /// <param name="wrapped">The BoDi object container</param>
        /// <exception cref="ArgumentNullException">If <paramref name="wrapped"/> is <see langword="null" />.</exception>
        public ServiceProviderAdapter(IObjectContainer wrapped)
        {
            this.wrapped = wrapped ?? throw new ArgumentNullException(nameof(wrapped));
        }
    }
}