using System;
using System.Collections.Generic;
using System.Reflection;

namespace CSF.Screenplay.NUnit
{
  static class ScreenplayContextContainer
  {
    static readonly IDictionary<Assembly,ScreenplayContext> contexts;
    static object syncRoot;

    internal static ScreenplayContext GetContext(Type fixtureType)
    {
      if(fixtureType == null)
        throw new ArgumentNullException(nameof(fixtureType));
      
      return GetContext(fixtureType.Assembly);
    }

    internal static ScreenplayContext GetContext(Assembly assembly)
    {
      if(assembly == null)
        throw new ArgumentNullException(nameof(assembly));

      lock(syncRoot)
      {
        if(!contexts.ContainsKey(assembly))
        {
          var context = new ScreenplayContext();
          contexts.Add(assembly, context);
        }
      }

      return contexts[assembly];
    }

    static ScreenplayContextContainer()
    {
      contexts = new Dictionary<Assembly,ScreenplayContext>();
      syncRoot = new object();
    }
  }
}
