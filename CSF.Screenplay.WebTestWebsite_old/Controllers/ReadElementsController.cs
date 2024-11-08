using System;
using System.Threading;
using System.Web.Mvc;
using CSF.Screenplay.WebTestWebsite.ActionFilters;
using CSF.Screenplay.WebTestWebsite.Models;

namespace CSF.Screenplay.WebTestWebsite.Controllers
{
  public class ReadElementsController : Controller
  {
    [BaseUri]
    public ActionResult Index()
    {
      var model = new ModelBase();
      return View(model);
    }
  }
}
