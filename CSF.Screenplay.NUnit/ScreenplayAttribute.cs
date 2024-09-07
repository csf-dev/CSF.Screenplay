using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CSF.Screenplay.Performances;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Builders;
using static CSF.Screenplay.ScreenplayLocator;

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
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ScreenplayAttribute : Attribute, ITestAction, ITestBuilder
    {
        /// <summary>
        /// The name of an NUnit3 Property for the suite or test description.
        /// </summary>
        internal const string DescriptionPropertyName = "Description";

        /// <summary>
        /// A key for an NUnit3 Property which will hold the current <see cref="IPerformance"/>.
        /// </summary>
        internal const string CurrentPerformanceKey = "Current Screenplay Performance";

        /// <summary>
        /// A key for an NUnit3 Property which will hold the <see cref="IHasPerformanceIdentity.PerformanceIdentity"/> of the current <see cref="IPerformance"/>.
        /// </summary>
        internal const string CurrentPerformanceIdentityKey = "Current Screenplay Performance identity";

        /// <summary>
        /// A key for an NUnit3 Property which will hold the current Dependency Injection scope.
        /// </summary>
        internal const string CurrentDiScopeKey = "Current Screenplay DI scope";

        /// <summary>
        /// Gets the targets for the attribute (when performing before/after test actions).
        /// </summary>
        /// <value>The targets.</value>
        public ActionTargets Targets => ActionTargets.Test;


        /// <inheritdoc/>
        public void BeforeTest(ITest test)
        {
            var performance = GetPerformance(test);
            BackfillPerformanceNamingHierarchy(performance, test);
            performance.BeginPerformance();
        }

        /// <inheritdoc/>
        public void AfterTest(ITest test)
        {
            var performance = GetPerformance(test);
            performance.FinishPerformance(GetOutcome());

            var diScope = test.Properties.Get(CurrentDiScopeKey) as IServiceScope;
            diScope?.Dispose();
        }

        /// <inheritdoc/>
        public IEnumerable<TestMethod> BuildFrom(IMethodInfo method, Test suite)
        {
            if (method is null)
                throw new ArgumentNullException(nameof(method));
            if (suite is null)
                throw new ArgumentNullException(nameof(suite));

            var screenplay = GetScreenplay(method);
            var scopeAndPerformance = screenplay.CreateScopedPerformance();
            var testMethod = GetTestMethod(scopeAndPerformance, method, suite);
            return new[] { testMethod };
        }

        static IPerformance GetPerformance(ITest test)
        {
            return (IPerformance)test.Properties.Get(CurrentPerformanceKey)
                ?? throw new ArgumentException($"The specified test must contain a property '{CurrentPerformanceKey}' containing an {nameof(IPerformance)}", nameof(test));
        }

        static TestMethod GetTestMethod(ScopeAndPerformance scopeAndPerformance, IMethodInfo method, Test suite)
        {
            var builder = new NUnitTestCaseBuilder();
            var resolvedTestMethodParameters = (from parameter in method.GetParameters()
                                                select scopeAndPerformance.Performance.ServiceProvider.GetService(parameter.ParameterType))
                .ToArray();
            var testCaseParameters = new TestCaseParameters(resolvedTestMethodParameters);

            var testMethod = builder.BuildTestMethod(method, suite, testCaseParameters);
            testMethod.Properties.Add(CurrentPerformanceKey, scopeAndPerformance.Performance);
            testMethod.Properties.Add(CurrentPerformanceIdentityKey, scopeAndPerformance.Performance.PerformanceIdentity);
            testMethod.Properties.Add(CurrentDiScopeKey, scopeAndPerformance.Scope);
            return testMethod;
        }

        static void BackfillPerformanceNamingHierarchy(IPerformance performance, ITest test)
        {
            var namingHierarchy = GetReverseOrderNamingHierarchy(test).ToList();

            // Reverse it to get it in the correct order
            namingHierarchy.Reverse();
            performance.NamingHierarchy.Clear();
            performance.NamingHierarchy.AddRange(namingHierarchy);
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
