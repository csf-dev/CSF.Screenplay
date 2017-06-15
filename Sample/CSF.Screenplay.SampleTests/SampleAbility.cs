using System;
using CSF.Screenplay.Abilities;

namespace CSF.Screenplay.SampleTests
{
  public class SampleAbility : Ability
  {
    public int GetSpecialNumber()
    {
      // Chosen by fair dice roll, guaranteed to be special!
      return 4;
    }
  }
}
