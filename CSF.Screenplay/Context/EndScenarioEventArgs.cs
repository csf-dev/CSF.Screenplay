using System;
namespace CSF.Screenplay.Context
{
  public class EndScenarioEventArgs : EventArgs
  {
    public bool Success { get; set; }
  }
}
