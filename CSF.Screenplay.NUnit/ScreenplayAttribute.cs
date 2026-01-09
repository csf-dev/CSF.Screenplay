using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace CSF.Screenplay
{
    /// <summary>
    /// Applied to a test method, indicates that decorated test is a Screenplay test.
    /// </summary>
    /// <remarks>
    /// <para>
    /// When a test method is decorated with this attribute then the test corresponding to that method will be executed via Screenplay.
    /// This means that the affected test method will be executed as an <see cref="IPerformance"/>.
    /// It also means that all parameters for the method will be provided by resolving them from the current
    /// performance's <see cref="IHasServiceProvider.ServiceProvider"/>.
    /// See <xref href="DependencyInjectionMainArticle?text=the+article+on+dependency+injection+in+Screenplay"/> for more information
    /// about what may be injected into test logic from DI, via the test method parameters.
    /// </para>
    /// <para>
    /// Remember that for this attribute to be effective, the <see cref="Assembly"/> which contains the test method must be decorated with
    /// <see cref="ScreenplayAssemblyAttribute"/>.  If it is not, then the test will fail with an exception.
    /// </para>
    /// </remarks>
    /// <seealso cref="ScreenplayAssemblyAttribute"/>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ScreenplayAttribute : Attribute, ITestAction, ITestBuilder
    {
        /// <summary>
        /// A key for an NUnit3 Property which will hold the <see cref="IHasPerformanceIdentity.PerformanceIdentity"/>
        /// of the current <see cref="IPerformance"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property value may be used with <see cref="ScreenplayLocator.GetScopedPerformance(Guid)"/>
        /// </para>
        /// </remarks>
        internal const string CurrentPerformanceIdentityKey = "Current Screenplay Performance identity";

        /// <summary>
        /// Gets the targets for the attribute (when performing before/after test actions).
        /// </summary>
        /// <value>The targets.</value>
        public ActionTargets Targets => ActionTargets.Test;


        /// <inheritdoc/>
        public void BeforeTest(ITest test)
        {
            var performance = ScreenplayLocator.GetScopedPerformance(test);
            performance.Performance.BeginPerformance();
        }

        /// <inheritdoc/>
        public void AfterTest(ITest test)
        {
            var scopeAndPerformance = ScreenplayLocator.GetScopedPerformance(test);
            scopeAndPerformance.Performance.FinishPerformance(GetOutcome());
            var diScope = scopeAndPerformance.Scope;
            diScope?.Dispose();
        }

        /// <inheritdoc/>
        public IEnumerable<TestMethod> BuildFrom(IMethodInfo method, Test suite) => TestMethodBuilder.BuildFrom(method, suite);

        static bool? GetOutcome()
        {
            var result = TestContext.CurrentContext.Result.Outcome.Status;
            switch (result)
            {
                case TestStatus.Passed: return true;
                case TestStatus.Failed: return false;
                default: return null;
            }
        }
    }
}
