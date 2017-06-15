using System;
using System.Collections.Generic;
using CSF.Screenplay.Reporting;

namespace CSF.Screenplay.Abilities
{
  /// <summary>
  /// An ability which can be held by an <see cref="Actors.ICanReceiveAbilities"/>.
  /// </summary>
  public interface IAbility : IDisposable, IReportable
  {
  }
}