using System;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Abilities
{
  public interface IUrlTransformer
  {
    Uri Transform(ApplicationUri uri);
  }
}
