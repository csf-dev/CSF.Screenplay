using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CSF.Screenplay.Performances;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace CSF.Screenplay
{
    /// <summary>
    /// A small static service locator of sorts, dedicated to getting an appropriate instance of <see cref="Screenplay"/> for
    /// a specified test object.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This type uses reflection to find the <see cref="ScreenplayAssemblyAttribute"/> which decorates the assembly in which the
    /// specified object (a test, a test method or the assembly itself) resides.
    /// It additionally caches the results in-memory to avoid repetitive reflection, only to retrieve the same results.
    /// </para>
    /// </remarks>
    public static class ScreenplayLocator
    {
        /// <summary>
        /// The name of an NUnit3 Property for the suite or test description.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is a built-in NUnit property.  It is set automatically by the framework if/when a test is decorated with appropriate attributes.
        /// </para>
        /// </remarks>
        internal const string DescriptionPropertyName = "Description";

        static readonly ConcurrentDictionary<Assembly, Screenplay> screenplayCache = new ConcurrentDictionary<Assembly, Screenplay>();
        static readonly ConcurrentDictionary<Guid,ScopeAndPerformance> performanceCache = new ConcurrentDictionary<Guid, ScopeAndPerformance>();

        /// <summary>
        /// Gets a <see cref="Screenplay"/> instance from the specified <see cref="Assembly"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method makes use of the <see cref="ScreenplayAssemblyAttribute"/> which decorates the assembly to get a
        /// Screenplay object instance for that assembly.
        /// If the specified assembly is not decorated with the Screenplay assembly attribute then this method will raise
        /// an exception.
        /// </para>
        /// </remarks>
        /// <param name="assembly">The test assembly for which to get a Screenplay object.</param>
        /// <returns>The Screenplay object for the specified assembly.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="assembly"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentException">If the <paramref name="assembly"/> is not decorated with <see cref="ScreenplayAssemblyAttribute"/>.</exception>
        public static Screenplay GetScreenplay(Assembly assembly)
        {
            if (assembly is null) throw new ArgumentNullException(nameof(assembly));
            return screenplayCache.GetOrAdd(assembly, GetScreenplayFromAssembly);
        }

        /// <summary>
        /// Gets a <see cref="Screenplay"/> instance from the specified test method.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method makes use of the <see cref="ScreenplayAssemblyAttribute"/> which decorates the assembly in which the
        /// specified method was declared, to get a Screenplay object instance applicable to the test method.
        /// If the method's assembly is not decorated with the Screenplay assembly attribute then this method will raise
        /// an exception.
        /// </para>
        /// </remarks>
        /// <param name="method">The test method for which to get a Screenplay object.</param>
        /// <returns>The Screenplay object for the specified test method.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="method"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentException">If the <paramref name="method"/>'s assembly is <see langword="null" /> or
        /// is not decorated with <see cref="ScreenplayAssemblyAttribute"/>.</exception>
        public static Screenplay GetScreenplay(IMethodInfo method)
        {
            if (method is null) throw new ArgumentNullException(nameof(method));

            var assembly = method.MethodInfo?.DeclaringType?.Assembly;
            if (assembly is null)
                throw new ArgumentException($"The test method must have an associated {nameof(Assembly)}.", nameof(method));
            return GetScreenplay(assembly);
        }

        /// <summary>
        /// Gets a <see cref="Screenplay"/> instance from the specified test.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method makes use of the <see cref="ScreenplayAssemblyAttribute"/> which decorates the assembly in which the
        /// specified test's method was declared, to get a Screenplay object instance applicable to the test method.
        /// If the test's method's assembly is not decorated with the Screenplay assembly attribute then this method will raise
        /// an exception.
        /// </para>
        /// </remarks>
        /// <param name="test">The test for which to get a Screenplay object.</param>
        /// <returns>The Screenplay object for the specified test.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="test"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentException">If the <paramref name="test"/>'s method's assembly is <see langword="null" /> or
        /// is not decorated with <see cref="ScreenplayAssemblyAttribute"/>.</exception>
        public static Screenplay GetScreenplay(ITest test)
        {
            if(test is null) throw new ArgumentNullException(nameof(test));
            if(test is TestAssembly testAssembly)
                return GetScreenplay(testAssembly.Assembly);
            return GetScreenplay(test.Method);
        }

        static Screenplay GetScreenplayFromAssembly(Assembly assembly)
        {
            var assemblyAttrib = assembly.GetCustomAttribute<ScreenplayAssemblyAttribute>()
                ?? throw new ArgumentException($"The assembly {assembly.FullName} must be decorated with {nameof(ScreenplayAssemblyAttribute)}.", nameof(assembly));
            return assemblyAttrib.GetScreenplay();
        }

        /// <summary>
        /// Gets a DI scope and <see cref="IPerformance"/> for the specified test.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method will return a cached <see cref="ScopeAndPerformance"/> if one exists for the specified test.
        /// If one does not yet exist then a new scope will be created, with an associated performance, and added to the cache.
        /// </para>
        /// </remarks>
        /// <param name="test">An NUnit3 test object.</param>
        /// <returns>A DI scope and performance.</returns>
        public static ScopeAndPerformance GetScopedPerformance(ITest test)
        {
            return performanceCache.GetOrAdd(GetPerformanceIdentity(test), _ => CreateScopedPerformance(test));
        }

        /// <summary>
        /// Gets a DI scope and <see cref="IPerformance"/> matching the specified performance identity.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Unlike the other overload of this method, this overload will not create a scoped performance if one does not yet exist.
        /// If this method is used with a performance identity which is not yet cached, then an exception will be raised.
        /// </para>
        /// </remarks>
        /// <param name="identity">A GUID performance identity, corresponding to <see cref="IHasPerformanceIdentity.PerformanceIdentity"/>.</param>
        /// <returns>A DI scope and performance.</returns>
        /// <exception cref="ArgumentException">If no scope &amp; performance exists in the cache, matching the specified identity</exception>
        public static ScopeAndPerformance GetScopedPerformance(Guid identity)
        {
            if (!performanceCache.TryGetValue(identity, out var result))
                throw new ArgumentException($"There must be a cached performance with the identity {identity}", nameof(identity));
            return result;
        }

        static ScopeAndPerformance CreateScopedPerformance(ITest test)
        {
            var screenplay = GetScreenplay(test);
            return screenplay.CreateScopedPerformance(GetNamingHierarchy(test), GetPerformanceIdentity(test));
        }

        static Guid GetPerformanceIdentity(ITest test)
        {
            if (test is null) throw new ArgumentNullException(nameof(test));
            if(!test.Properties.ContainsKey(ScreenplayAttribute.CurrentPerformanceIdentityKey))
                throw new ArgumentException($"The test must contain a property by the name of '{ScreenplayAttribute.CurrentPerformanceIdentityKey}', " +
                                            "containing a Guid performance identity",
                                            nameof(test));
            return (Guid) test.Properties.Get(ScreenplayAttribute.CurrentPerformanceIdentityKey);
        }
        
        static IList<IdentifierAndName> GetNamingHierarchy(ITest test)
        {
            var namingHierarchy = GetReverseOrderNamingHierarchy(test).ToList();
            namingHierarchy.Reverse();
            return namingHierarchy;
        }

        static IEnumerable<IdentifierAndName> GetReverseOrderNamingHierarchy(ITest suite)
        {
            for (var currentSuite = suite;
                 currentSuite != null;
                 currentSuite = currentSuite.Parent)
            {
                if (!currentSuite.IsSuite || (currentSuite.Method is null && currentSuite.Fixture is null))
                    continue;
                yield return new IdentifierAndName(currentSuite.FullName, currentSuite.Properties.Get(DescriptionPropertyName)?.ToString());
            }
        }
    }
}
