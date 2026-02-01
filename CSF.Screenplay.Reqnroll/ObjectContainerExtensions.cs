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
        /// Note that this is an imperfect solution.  The BoDi container shipped with Reqnroll/SpecFlow does not support all the functionality
        /// which is expected from <see cref="IServiceCollection"/>. Many methods of the returned object will throw <see cref="NotSupportedException"/>
        /// if attempts are made to use them (a known LSP violation).  Additionally, not all service collection DI behaviour will operate in the same
        /// manner when using this adapter.  In short "your mileage may vary".
        /// </para>
        /// <para>
        /// However, for the most simple of usages, this enables the use of "Add to DI" logic which has been crafted for service collection,
        /// in such a way that services may be added to the BoDi container without additional logic.
        /// </para>
        /// </remarks>
        /// <param name="bodiContainer">A Reqnroll/SpecFlow BoDi DI container.</param>
        /// <returns>An adapter object which implements some of the functionality of <see cref="IServiceCollection"/></returns>
        public static IServiceCollection ToServiceCollection(this IObjectContainer bodiContainer)
            => new ServiceCollectionAdapter(bodiContainer);
    }
}