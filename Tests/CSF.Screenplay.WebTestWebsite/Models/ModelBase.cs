using System;
namespace CSF.Screenplay.WebTestWebsite.Models
{
  public class ModelBase
  {
    Uri baseUri;

    public Uri BaseUri
    {
      get { return baseUri; }
      set {
        if(value == null)
          throw new ArgumentNullException(nameof(value));

        baseUri = value;
      }
    }

    public int LoadingPause { get; set; }

    public string BaseUrl => BaseUri.AbsoluteUri;

    public ModelBase()
    {
      baseUri = new Uri("http://localhost:8080/");
    }
  }
}
