using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using CSF.Screenplay.WebTestWebsite.Models;

namespace CSF.Screenplay.WebTestWebsite.Controllers.Controllers
{
  public class HomeController : ControllerBase
  {
    public ActionResult Index()
    {
      var model = new ModelBase();
      PopulateModel(model);
      return View(model);
    }
  }
}
