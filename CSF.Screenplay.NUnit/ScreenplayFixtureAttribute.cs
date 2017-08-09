using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Builders;

namespace CSF.Screenplay.NUnit
{
  public class ScreenplayFixtureAttribute : TestFixtureAttribute, IFixtureBuilder
  {
    readonly NUnitTestFixtureBuilder builder;

    public new IEnumerable<TestSuite> BuildFrom (ITypeInfo typeInfo)
    {
      return BuildTestSuites(typeInfo);
    }

    IEnumerable<TestSuite> IFixtureBuilder.BuildFrom(ITypeInfo typeInfo)
    {
      return BuildTestSuites(typeInfo);
    }

    IEnumerable<TestSuite> BuildTestSuites(ITypeInfo typeInfo)
    {
      var data = new TestFixtureData(ScreenplayContextContainer.GetContext(typeInfo.Assembly));
      var output = builder.BuildFrom(typeInfo, data);
      return new [] { output };
    }

    public ScreenplayFixtureAttribute() : this(new object[0]) {}

    public ScreenplayFixtureAttribute(params object[] arguments) : base(arguments)
    {
      builder = new NUnitTestFixtureBuilder();
    }
  }
}
