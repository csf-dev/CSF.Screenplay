using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.Options;

#if SPECFLOW
using BoDi;
#else
using Reqnroll.BoDi;
#endif

namespace CSF.Screenplay
{
    /// <summary>
    /// Part of a workaround for .NET options pattern with BoDi.  Contains instances of <see cref="IConfigureOptions{ScreenplayOptions}"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The BoDi DI container which is included in Reqnroll/SpecFlow does not fully support the .NET Options pattern.
    /// Specifically, it cannot resolve an <c>IEnumerable&lt;T&gt;</c> of "things", where that enumerable represents all the registrations
    /// for the service type <c>T</c>.  However, because we know the concrete type of the options upfront, is is possible to generate
    /// this workaround and register it explicitly into BoDi.  This means that when the Options pattern logic attempts to resolve the enumerable
    /// collection, it receives an instance of this class instead.  This class uses alternative functionality of BoDi to generate that required
    /// collection and provide it in the manner that the Options logic requires.
    /// </para>
    /// </remarks>
    public class EnumerableOptionsContainer : IEnumerable<IConfigureOptions<ScreenplayOptions>>
    {
        readonly IObjectContainer container;

        /// <inheritdoc/>
        public IEnumerator<IConfigureOptions<ScreenplayOptions>> GetEnumerator()
        {
            var allOptions = container.ResolveAll<IConfigureOptions<ScreenplayOptions>>();
            return allOptions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Initializes a new instance of <see cref="EnumerableOptionsContainer"/>.
        /// </summary>
        /// <param name="container">The BoDi object container.</param>
        /// <exception cref="ArgumentNullException"><paramref name="container"/> is <c>null</c>.</exception>
        public EnumerableOptionsContainer(IObjectContainer container)
        {
            this.container = container ?? throw new ArgumentNullException(nameof(container));
        }
    }

    /// <summary>
    /// Part of a workaround for .NET options pattern with BoDi.  Contains instances of <see cref="IPostConfigureOptions{ScreenplayOptions}"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The BoDi DI container which is included in Reqnroll/SpecFlow does not fully support the .NET Options pattern.
    /// Specifically, it cannot resolve an <c>IEnumerable&lt;T&gt;</c> of "things", where that enumerable represents all the registrations
    /// for the service type <c>T</c>.  However, because we know the concrete type of the options upfront, is is possible to generate
    /// this workaround and register it explicitly into BoDi.  This means that when the Options pattern logic attempts to resolve the enumerable
    /// collection, it receives an instance of this class instead.  This class uses alternative functionality of BoDi to generate that required
    /// collection and provide it in the manner that the Options logic requires.
    /// </para>
    /// </remarks>
    public class EnumerablePostOptionsContainer : IEnumerable<IPostConfigureOptions<ScreenplayOptions>>
    {
        readonly IObjectContainer container;

        /// <inheritdoc/>
        public IEnumerator<IPostConfigureOptions<ScreenplayOptions>> GetEnumerator()
        {
            var allOptions = container.ResolveAll<IPostConfigureOptions<ScreenplayOptions>>();
            return allOptions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Initializes a new instance of <see cref="EnumerablePostOptionsContainer"/>.
        /// </summary>
        /// <param name="container">The BoDi object container.</param>
        /// <exception cref="ArgumentNullException"><paramref name="container"/> is <c>null</c>.</exception>
        public EnumerablePostOptionsContainer(IObjectContainer container)
        {
            this.container = container ?? throw new ArgumentNullException(nameof(container));
        }
    }

    /// <summary>
    /// Part of a workaround for .NET options pattern with BoDi.  Contains instances of <see cref="IValidateOptions{ScreenplayOptions}"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The BoDi DI container which is included in Reqnroll/SpecFlow does not fully support the .NET Options pattern.
    /// Specifically, it cannot resolve an <c>IEnumerable&lt;T&gt;</c> of "things", where that enumerable represents all the registrations
    /// for the service type <c>T</c>.  However, because we know the concrete type of the options upfront, is is possible to generate
    /// this workaround and register it explicitly into BoDi.  This means that when the Options pattern logic attempts to resolve the enumerable
    /// collection, it receives an instance of this class instead.  This class uses alternative functionality of BoDi to generate that required
    /// collection and provide it in the manner that the Options logic requires.
    /// </para>
    /// </remarks>
    public class EmptyOptionsValidatorCollection : IEnumerable<IValidateOptions<ScreenplayOptions>>
    {
        readonly IObjectContainer container;

        /// <inheritdoc/>
        public IEnumerator<IValidateOptions<ScreenplayOptions>> GetEnumerator()
        {
            var allOptions = container.ResolveAll<IValidateOptions<ScreenplayOptions>>();
            return allOptions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Initializes a new instance of <see cref="EmptyOptionsValidatorCollection"/>.
        /// </summary>
        /// <param name="container">The BoDi object container.</param>
        /// <exception cref="ArgumentNullException"><paramref name="container"/> is <c>null</c>.</exception>
        public EmptyOptionsValidatorCollection(IObjectContainer container)
        {
            this.container = container ?? throw new ArgumentNullException(nameof(container));
        }
    }
}