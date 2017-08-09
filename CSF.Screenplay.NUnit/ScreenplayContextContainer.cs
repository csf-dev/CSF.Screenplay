using System;
using System.Collections.Generic;
using System.Reflection;

namespace CSF.Screenplay.NUnit
{
  static class ScreenplayContextContainer
  {
    static readonly IDictionary<Assembly,ScreenplayContext> contexts;
    static object syncRoot;

    internal static ScreenplayContext GetContext(object fixture)
    {
      if(fixture == null)
        throw new ArgumentNullException(nameof(fixture));

      return GetContext(fixture.GetType());
    }

    internal static ScreenplayContext GetContext(Type fixtureType)
    {
      if(fixtureType == null)
        throw new ArgumentNullException(nameof(fixtureType));
      
      return GetContext(fixtureType.Assembly);
    }

    internal static ScreenplayContext GetContext(Assembly fixtureAssembly)
    {
      if(fixtureAssembly == null)
        throw new ArgumentNullException(nameof(fixtureAssembly));

      lock(syncRoot)
      {
        if(!contexts.ContainsKey(fixtureAssembly))
        {
          var context = new ScreenplayContext();
          contexts.Add(fixtureAssembly, context);
        }
      }

      return contexts[fixtureAssembly];
    }

    static ScreenplayContextContainer()
    {
      contexts = new Dictionary<Assembly,ScreenplayContext>();
      syncRoot = new object();
    }
  }
}
