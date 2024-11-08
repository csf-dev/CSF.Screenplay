using System;
using System.Collections.Concurrent;
using System.Reflection;
using NUnit.Framework.Interfaces;

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
        static readonly ConcurrentDictionary<Assembly, Screenplay> screenplayCache = new ConcurrentDictionary<Assembly, Screenplay>();

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
            return GetScreenplay(test.Method);
        }

        static Screenplay GetScreenplayFromAssembly(Assembly assembly)
        {
            var assemblyAttrib = assembly.GetCustomAttribute<ScreenplayAssemblyAttribute>()
                ?? throw new ArgumentException($"The assembly {assembly.FullName} must be decorated with {nameof(ScreenplayAssemblyAttribute)}.", nameof(assembly));
            return assemblyAttrib.GetScreenplay();
        }
    }
}
