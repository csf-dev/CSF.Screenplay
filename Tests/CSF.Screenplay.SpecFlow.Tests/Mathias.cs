using System;
using CSF.Screenplay.AddingUp;

namespace CSF.Screenplay;

public class Mathias : IPersona
{
    public string Name => "Mathias";

    public Actor GetActor(Guid performanceIdentity)
    {
        var mathias = new Actor(Name, performanceIdentity);
        mathias.IsAbleTo<AddNumbers>();
        return mathias;
    }
}