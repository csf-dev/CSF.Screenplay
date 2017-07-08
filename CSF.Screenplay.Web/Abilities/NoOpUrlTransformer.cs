using System;
using CSF.Screenplay.Web.Models;

namespace CSF.Screenplay.Web.Abilities
{
  public class NoOpUrlTransformer : IUrlTransformer
  {
    public Uri Transform(ApplicationUri uri) => uri.Uri;
  }
}
