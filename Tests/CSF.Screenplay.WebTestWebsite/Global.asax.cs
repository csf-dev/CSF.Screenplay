using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using CSF.Screenplay.WebTestWebsite.App_Start;

namespace CSF.Screenplay.WebTestWebsite
{
  public class Global : HttpApplication
  {
    void ConfigureMvc()
    {
      AreaRegistration.RegisterAllAreas();
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      ViewEngines.Engines.Clear();
      ViewEngines.Engines.Add(new Zpt.MVC.ZptViewEngine());
    }

    void ConfigureWebApi()
    {
      GlobalConfiguration.Configuration.MapHttpAttributeRoutes();
    }

    protected void Application_Start()
    {
      ConfigureWebApi();
      ConfigureMvc();

      GlobalConfiguration.Configuration.EnsureInitialized();
    }
  }
}
