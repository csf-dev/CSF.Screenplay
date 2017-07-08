using System;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Abilities
{
  public interface IUriTransformer
  {
    Uri Transform(IUriProvider uri);
  }
}
