using System;
namespace CSF.Screenplay.Actors
{
  /// <summary>
  /// An implementation of <see cref="Cast"/> which (by default) creates <see cref="ReportingActor"/> instances.
  /// </summary>
  public class ReportingCast : Cast
  {
    /// <summary>
    /// Creates and returns a new object which implements <see cref="IActor"/>.
    /// </summary>
    /// <returns>The actor.</returns>
    /// <param name="name">The actor's name.</param>
    protected override IActor CreateActor(string name)
    {
      return new ReportingActor(name);
    }
  }
}
