using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace CSF.Screenplay.WebTestWebsite.App_Start
{
  public class MvcConfiguration
  {
    public static void Configure()
    {
      AreaRegistration.RegisterAllAreas();
      RouteConfig.RegisterRoutes(RouteTable.Routes);
    }
  }
}
