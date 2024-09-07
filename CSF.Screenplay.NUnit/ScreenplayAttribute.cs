using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CSF.Screenplay.Performances;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Builders;
using static CSF.Screenplay.ScreenplayLocator;

namespace CSF.Screenplay
{
    /// <summary>
    /// Applied to an assembly, test class or individual test method, indicates that the tests in the corresponding scope are Screenplay tests.
    /// </summary>
    /// <remarks>
    /// <para>
    /// When a test method, fixture or assembly is decorated with this attribute then all tests with that scope
    /// (the test method, all test methods in the fixture, or all test methods in the assembly) are to be executed via Screenplay.
    /// This means that each affected test method will be executed as an <see cref="IPerformance"/>.
    /// This means that all parameters to the affected test methods will be provided by resolving them from the current
    /// performance's <see cref="IHasServiceProvider.ServiceProvider"/>.
    /// See <xref href="DependencyInjectionMainArticle?text=the+article+on+dependency+injection+in+Screenplay"/> for more information
    /// about what may be injected into test logic from DI, via the test method parameters.
    /// </para>
    /// <para>
    /// Remember that for this attribute to be effective, the <see cref="Assembly"/> which contains the test methods must be decorated with
    /// <see cref="ScreenplayAssemblyAttribute"/>.  If it is not, then the tests will all fail with exceptions.
    /// </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = false)]
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
        /// Gets the targets for the attribute (when performing before/after test actions).
        /// </summary>
        /// <value>The targets.</value>
        public ActionTargets Targets => ActionTargets.Test;


        /// <inheritdoc/>
        public void BeforeTest(ITest test)
        {
            var performance = GetPerformance(test);
            test.Properties.Add(CurrentPerformanceIdentityKey, performance.PerformanceIdentity);
            performance.BeginPerformance();
        }

        /// <inheritdoc/>
        public void AfterTest(ITest test)
        {
            var performance = GetPerformance(test);
            var outcome = GetOutcome();
            performance.FinishPerformance(outcome);
        }

        /// <inheritdoc/>
        public IEnumerable<TestMethod> BuildFrom(IMethodInfo method, Test suite)
        {
            if (method is null)
                throw new ArgumentNullException(nameof(method));
            if (suite is null)
                throw new ArgumentNullException(nameof(suite));

            var screenplay = GetScreenplay(method);
            var performance = CreatePerformance(screenplay, method, suite);
            var testMethod = GetTestMethod(performance, method, suite);
            return new[] { testMethod };
        }

        static IPerformance CreatePerformance(Screenplay screenplay, IMethodInfo method, Test suite)
        {
            var namingHierarchy = GetReverseOrderNamingHierarchy(method, suite).ToList();
            // Reverse it to get it in the correct order
            namingHierarchy.Reverse();
            return screenplay.CreatePerformance(namingHierarchy);
        }

        static IPerformance GetPerformance(ITest test)
        {
            return (IPerformance)test.Properties.Get(CurrentPerformanceKey)
                ?? throw new ArgumentException($"The specified test must contain a property '{CurrentPerformanceKey}' containing an {nameof(IPerformance)}", nameof(test));
        }

        static TestMethod GetTestMethod(IPerformance performance, IMethodInfo method, Test suite)
        {
            var builder = new NUnitTestCaseBuilder();
            var resolvedTestMethodParameters = (from parameter in method.GetParameters()
                                                select performance.ServiceProvider.GetService(parameter.ParameterType))
                .ToArray();
            var testCaseParameters = new TestCaseParameters(resolvedTestMethodParameters);

            var testMethod = builder.BuildTestMethod(method, suite, testCaseParameters);
            testMethod.Properties.Add(CurrentPerformanceKey, performance);
            return testMethod;
        }

        static IEnumerable<IdentifierAndName> GetReverseOrderNamingHierarchy(IMethodInfo method, ITest suite)
        {
            yield return new IdentifierAndName($"{suite.FullName}.{method.Name}", suite.Properties.Get(DescriptionPropertyName)?.ToString());

            for (var currentSuite = suite.Parent;
                currentSuite != null;
                currentSuite = currentSuite.Parent)
            {
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
