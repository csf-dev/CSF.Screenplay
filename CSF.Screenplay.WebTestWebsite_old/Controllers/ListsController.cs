using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CSF.Screenplay.WebTestWebsite.ActionFilters;
using CSF.Screenplay.WebTestWebsite.Models;

namespace CSF.Screenplay.WebTestWebsite.Controllers
{
  public class ListsController : Controller
  {
    [BaseUri]
    public ActionResult Index()
    {
      return View(new ModelBase());
    }
  }
}
