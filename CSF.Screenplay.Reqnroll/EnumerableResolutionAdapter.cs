using System;
using System.Collections;
using System.Collections.Generic;

#if SPECFLOW
using BoDi;
#else
using Reqnroll.BoDi;
#endif

namespace CSF.Screenplay
{
    /// <summary>
    /// Adapter class which - when added to DI - permits the BoDi DI container to resolve arbitrary <see cref="IEnumerable{T}"/> of
    /// service instances.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The BoDi DI container which is included in Reqnroll/SpecFlow does not fully support the functionality of Microsoft's DI standard.
    /// Notably, it cannot natively resolve an <see cref="IEnumerable{T}"/>, where type T is a service type which may have multiple implementations
    /// added to/registered with the container.
    /// BoDi does have conceptually identical functionality, in its <see cref="IObjectContainer.ResolveAll{T}"/> function.
    /// </para>
    /// <para>
    /// The purpose of this type is to provide a mechanism by which BoDi may resolve enumerables of service types.
    /// This class wraps an instance of <see cref="IObjectContainer"/> and - in its <see cref="GetEnumerator"/> method - redirects to the
    /// <see cref="IObjectContainer.ResolveAll{T}"/> method.
    /// </para>
    /// <para>
    /// The limitation of this type (as a workaround) is that this type must be added to the container manually for each <see cref="IEnumerable{T}"/>
    /// type which could be resolved from the container.
    /// </para>
    /// </remarks>
    public class EnumerableResolutionAdapter<T> : IEnumerable<T> where T : class
    {
        readonly IObjectContainer container;

        /// <inheritdoc/>
        public IEnumerator<T> GetEnumerator()
        {
            var allOptions = container.ResolveAll<T>();
            return allOptions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Initializes a new instance of <see cref="EnumerableResolutionAdapter{T}"/>.
        /// </summary>
        /// <param name="container">The BoDi object container.</param>
        /// <exception cref="ArgumentNullException"><paramref name="container"/> is <c>null</c>.</exception>
        public EnumerableResolutionAdapter(IObjectContainer container)
        {
            this.container = container ?? throw new ArgumentNullException(nameof(container));
        }
    }
}