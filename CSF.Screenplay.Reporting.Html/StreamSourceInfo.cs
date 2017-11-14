using System;
using CSF.Zpt.Rendering;

namespace CSF.Screenplay.Reporting
{
  public class StreamSourceInfo : ISourceInfo
  {
    readonly string name;

    public string FullName => name;

    public bool Equals(ISourceInfo other)
    {
      return (other is StreamSourceInfo) && ((StreamSourceInfo) other).FullName == FullName;
    }

    public object GetContainer() => null;

    public string GetRelativeName(string root) => FullName;

    public StreamSourceInfo(string name)
    {
      if(name == null)
        throw new ArgumentNullException(nameof(name));

      this.name = name;
    }
  }
}
