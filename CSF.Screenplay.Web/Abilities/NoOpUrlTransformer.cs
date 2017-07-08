using System;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Abilities
{
  public class NoOpUrlTransformer : IUriTransformer
  {
    public Uri Transform(IUriProvider uri) => uri.Uri;
  }
}
