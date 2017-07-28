using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CSF.Screenplay.WebTestWebsite.Models;

namespace CSF.Screenplay.WebTestWebsite.Controllers
{
  public class PageThreeController : ControllerBase
  {
    public ActionResult Index()
    {
      var model = new ModelBase();
      PopulateModel(model);
      return View(model);
    }
  }
}
