using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using CSF.Screenplay.WebTestWebsite.App_Start;

namespace CSF.Screenplay.WebTestWebsite
{
  public class Global : HttpApplication
  {
    protected void Application_Start()
    {
      AreaRegistration.RegisterAllAreas();
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      ViewEngines.Engines.Clear();
      ViewEngines.Engines.Add(new Zpt.MVC.ZptViewEngine());
    }
  }
}
