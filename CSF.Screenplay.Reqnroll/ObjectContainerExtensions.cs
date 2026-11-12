using System;
#if SPECFLOW
using BoDi;
#else
using Reqnroll.BoDi;
#endif
using Microsoft.Extensions.DependencyInjection;

namespace CSF.Screenplay
{
    /// <summary>
    /// Extension methods for the Reqnroll/SpecFlow "BoDi" DI container.
    /// </summary>
    public static class ObjectContainerExtensions
    {
        /// <summary>
        /// Gets an adapter object which permits limited use of the BoDi <see cref="IObjectContainer"/> as if it were an
        /// <see cref="IServiceCollection"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Note that this is an imperfect conversion and suffers from numerous
        /// <see href="https://en.wikipedia.org/wiki/Liskov_substitution_principle">Liskov Substitution Principle violations</see>.
        /// In other words, the BoDi container used by Reqnroll/SpecFlow does not support the API defined for <see cref="IServiceCollection"/>.
        /// Using a BoDi container as a Service Collection requires workarounds for common scenarios and and comes with several
        /// limitations which have no workaround.
        /// </para>
        /// <para>
        /// For more information, including suggestions to mitigate or avoid this, see
        /// <xref href="BoDiWorkaroundsArticle?text=the+BoDi+workarounds+article"/> and the documentation for the class
        /// <see cref="ServiceCollectionAdapter"/>.
        /// </para>
        /// </remarks>
        /// <param name="bodiContainer">A Reqnroll/SpecFlow BoDi DI container.</param>
        /// <returns>An adapter object which implements some of the functionality of <see cref="IServiceCollection"/></returns>
        public static IServiceCollection ToServiceCollection(this IObjectContainer bodiContainer)
            => new ServiceCollectionAdapter(bodiContainer);

        /// <summary>
        /// Gets an adapter object which permits the use of the BoDi <see cref="IObjectContainer"/> as if it were an
        /// <see cref="IServiceProvider"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Note that this is an imperfect conversion and suffers from a
        /// <see href="https://en.wikipedia.org/wiki/Liskov_substitution_principle">Liskov Substitution Principle violation</see>.
        /// In other words, the BoDi container used by Reqnroll/SpecFlow does not support the API defined for <see cref="IServiceProvider"/>.
        /// Using a BoDi container as a Service Provider requires workarounds for common scenarios and and comes with some
        /// limitations which have no workaround.
        /// </para>
        /// <para>
        /// For more information, including suggestions to mitigate or avoid this, see
        /// <xref href="BoDiWorkaroundsArticle?text=the+BoDi+workarounds+article"/> and the documentation for the class
        /// <see cref="ServiceProviderAdapter"/>.
        /// </para>
        /// </remarks>
        /// <param name="bodiContainer">A Reqnroll/SpecFlow BoDi DI container.</param>
        /// <returns>An adapter object which implements the functionality of <see cref="IServiceProvider"/></returns>
        public static IServiceProvider ToServiceProvider(this IObjectContainer bodiContainer)
            => new ServiceProviderAdapter(bodiContainer);
    }
}