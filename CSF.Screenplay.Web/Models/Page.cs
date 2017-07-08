using System;

namespace CSF.Screenplay.Web.Models
{
  public abstract class Page
  {
    public abstract string GetName();

    public abstract ApplicationUri GetUri();
  }
}
