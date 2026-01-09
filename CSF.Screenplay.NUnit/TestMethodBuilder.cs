using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Builders;

namespace CSF.Screenplay
{
    /// <summary>
    /// Builder class which is used to create NUnit3 test method instances for a Screenplay-based test.
    /// </summary>
    public static class TestMethodBuilder
    {
        static readonly Dictionary<Type,Func<Guid,object>> supportedInjectableTypes = new Dictionary<Type, Func<Guid,object>>
        {
            { typeof(IStage), id => new StageAdapter(id) },
            { typeof(ICast), id => new CastAdapter(id) },
            { typeof(IPerformance), id => new PerformanceAdapter(id) },
        };

        /// <summary>
        /// Gets a collection of NUnit3 <c>TestMethod</c> instances for the specified method and test.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method handles the resolution of the <xref href="InjectableServicesArticle?text=supported+injectable+services" />, which may be used in tests by
        /// adding them as parameters to the NUnit test method.
        /// </para>
        /// </remarks>
        /// <param name="method">The NUnit method object for a test</param>
        /// <param name="suite">The NUnit test suite object</param>
        /// <returns>A collection of NUnit test method instances</returns>
        /// <exception cref="ArgumentNullException">If any parameter is <see langword="null"/></exception>
        public static IEnumerable<TestMethod> BuildFrom(IMethodInfo method, Test suite)
        {
            if (method is null) throw new ArgumentNullException(nameof(method));
            if (suite is null) throw new ArgumentNullException(nameof(suite));

            var builder = new NUnitTestCaseBuilder();

            var performanceId = Guid.NewGuid();
            var testCaseParameters = GetResolvedParameters(method, performanceId);

            var testMethod = builder.BuildTestMethod(method, suite, testCaseParameters);
            testMethod.Properties.Add(ScreenplayAttribute.CurrentPerformanceIdentityKey, performanceId);
            
            return new [] { testMethod };
        }

        static TestCaseParameters GetResolvedParameters(IMethodInfo method, Guid performanceId)
        {
            var requestedParameters = method.GetParameters();
            if(requestedParameters.Any(param => !supportedInjectableTypes.Keys.Contains(param.ParameterType)))
                throw new InvalidOperationException("The test method must not contain any parameters except those of supported injectable types:\n" +
                                                    string.Join(", ", supportedInjectableTypes.Keys.Select(x => x.Name)) +
                                                    "\nUnfortunately, NUnit's architecture makes it troublesome to support arbitrary injectable parameters.");
            var resolvedParameterValues = requestedParameters.Select(param => supportedInjectableTypes[param.ParameterType].Invoke(performanceId)).ToArray();
            return new TestCaseParameters(resolvedParameterValues);
        }
    }
}