using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Builders;

namespace CSF.Screenplay.NUnit
{
  class ScreenplayFixtureBuilder
  {
    readonly NUnitTestFixtureBuilder builder;

    internal IEnumerable<TestSuite> BuildTestSuites(ITypeInfo typeInfo)
    {
      var data = new TestFixtureData(ScreenplayContextContainer.GetContext());
      var output = builder.BuildFrom(typeInfo, data);
      return new [] { output };
    }

    internal ScreenplayFixtureBuilder()
    {
      builder = new NUnitTestFixtureBuilder();
    }
  }
}
