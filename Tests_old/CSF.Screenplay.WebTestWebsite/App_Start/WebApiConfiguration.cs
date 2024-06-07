using System;
using System.Web.Http;

namespace CSF.Screenplay.WebTestWebsite.App_Start
{
  public class WebApiConfiguration
  {
    public static void Configure()
    {
      GlobalConfiguration.Configuration.MapHttpAttributeRoutes();
    }
  }
}
