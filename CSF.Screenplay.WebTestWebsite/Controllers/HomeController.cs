﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using CSF.Screenplay.WebTestWebsite.Models;

namespace CSF.Screenplay.WebTestWebsite.Controllers.Controllers
{
  public class HomeController : ControllerBase
  {
    public ActionResult Index(int? delay)
    {
      if(delay.GetValueOrDefault() > 0)
        Thread.Sleep(TimeSpan.FromSeconds(delay.GetValueOrDefault()));
      
      var model = new ModelBase();
      PopulateModel(model);
      model.LoadingPause = delay.GetValueOrDefault();
      return View(model);
    }
  }
}
