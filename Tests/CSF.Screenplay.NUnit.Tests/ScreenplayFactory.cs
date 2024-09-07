namespace CSF.Screenplay;

public class ScreenplayFactory : IGetsScreenplay
{
    public Screenplay GetScreenplay() => Screenplay.CreateDefault();
}