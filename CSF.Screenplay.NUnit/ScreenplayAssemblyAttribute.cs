using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace CSF.Screenplay
{
    /// <summary>
    /// An attribute used to mark an assembly which contains Screenplay-based tests.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This attribute is the core of the NUnit3 <xref href="IntegrationGlossaryItem?text=test+framework+integration"/> with Screenplay.
    /// In order to run tests with Screenplay, the assembly must be decorated with this attribute.
    /// </para>
    /// <para>
    /// This attribute identifies a concrete implementation of <see cref="IGetsScreenplay"/> which will be used to build and retrieve
    /// the <see cref="Screenplay"/> instance for running those tests.
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>
    /// Decorate your assembly with this attribute using the syntax <c>[assembly: ScreenplayAssembly]</c>.  You may place this
    /// into any source file, outside of any type declaration.  By convention it would be put into a dedicated source file
    /// within the <c>Properties</c> project directory.
    /// </para>
    /// </example>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public class ScreenplayAssemblyAttribute : TestActionAttribute
    {
        readonly Lazy<Screenplay> lazyScreenplay;

        /// <summary>
        /// Gets the <see cref="Screenplay"/> which is to be used for tests contained in the current assembly.
        /// </summary>
        /// <returns>The Screenplay.</returns>
        /// <exception cref="InvalidOperationException">If the Screenplay factory used with the constructor to this
        /// attribute is invalid or fails to return a non-<see langword="null" /> <see cref="Screenplay"/> instance.</exception>
        public Screenplay GetScreenplay()
        {
            try
            {
                return lazyScreenplay.Value;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected exception was encountered creating the Screenplay for this test assembly; see the inner exception for details.", ex);
            }
        }

        /// <inheritdoc/>
        public override ActionTargets Targets => ActionTargets.Suite;

        /// <inheritdoc/>
        public override void AfterTest(ITest test) => GetScreenplay().CompleteScreenplay();

        /// <inheritdoc/>
        public override void BeforeTest(ITest test) => GetScreenplay().BeginScreenplay();

        /// <summary>
        /// Initializes a new instance of <see cref="ScreenplayAssemblyAttribute"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The <paramref name="factoryType"/> specified in this constructor must meet all of the following criteria:
        /// </para>
        /// <list type="bullet">
        /// <item><description>It must be a non-<see langword="null" /> <see cref="Type"/> which derives from <see cref="IGetsScreenplay"/></description></item>
        /// <item><description>It must have a public parameterless constructor</description></item>
        /// <item><description>It must return a non-<see langword="null" /> instance of <see cref="Screenplay"/> from its <see cref="IGetsScreenplay.GetScreenplay"/> method</description></item>
        /// </list>
        /// </remarks>
        /// <param name="factoryType">The concrete type of a class which implements <see cref="IGetsScreenplay"/>.</param>
        public ScreenplayAssemblyAttribute(Type factoryType)
        {
            lazyScreenplay = new Lazy<Screenplay>(GetScreenplayFactory(factoryType));
        }

        static Func<Screenplay> GetScreenplayFactory(Type factoryType)
        {
            if (factoryType is null)
                throw new ArgumentNullException(nameof(factoryType));
            if (!typeof(IGetsScreenplay).IsAssignableFrom(factoryType))
                throw new ArgumentException($"The factory type {factoryType.FullName} must derive from {nameof(IGetsScreenplay)}.", nameof(factoryType));
            if (factoryType.GetConstructor(Type.EmptyTypes) is null)
                throw new ArgumentException($"The factory type {factoryType.FullName} must have a public parameterless constructor.", nameof(factoryType));
            
            return () =>
            {
                var factory = InstantiateFactory(factoryType);
                var screenplay = factory.GetScreenplay();
                if (screenplay is null)
                    throw new InvalidOperationException($"The factory type {factoryType.FullName} must return a non-null Screenplay instance from its {nameof(IGetsScreenplay.GetScreenplay)} method.");
                
                return screenplay;
            };
        }

        static IGetsScreenplay InstantiateFactory(Type factoryType)
        {
            try
            {
                return (IGetsScreenplay) Activator.CreateInstance(factoryType);
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException($"The factory type {factoryType.FullName} must not raise an exception from its public parameterless constructor.", ex);
            }
        }
    }
}
