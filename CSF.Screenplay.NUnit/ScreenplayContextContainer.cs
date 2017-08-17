using System;

namespace CSF.Screenplay.NUnit
{
  static class ScreenplayContextContainer
  {
    static readonly ScreenplayContext context;

    internal static ScreenplayContext GetContext() => context;

    static ScreenplayContextContainer()
    {
      context = new ScreenplayContext();
    }
  }
}
