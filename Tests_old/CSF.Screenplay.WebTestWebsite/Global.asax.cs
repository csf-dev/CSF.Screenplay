using System.Web;
using System.Web.Http;
using CSF.Screenplay.WebTestWebsite.App_Start;

namespace CSF.Screenplay.WebTestWebsite
{
  public class Global : HttpApplication
  {
    protected void Application_Start()
    {
      WebApiConfiguration.Configure();
      MvcConfiguration.Configure();

      GlobalConfiguration.Configuration.EnsureInitialized();
    }
  }
}
