using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CSF.Screenplay.WebTestWebsite.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      return new HttpStatusCodeResult(HttpStatusCode.OK);
    }
  }
}
