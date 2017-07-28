using System;
using System.Web.Mvc;

namespace CSF.Screenplay.WebTestWebsite.Controllers
{
  public class ControllerBase : Controller
  {
    protected virtual void PopulateModel(Models.ModelBase model)
    {
      if(model == null)
        throw new ArgumentNullException(nameof(model));

      model.BaseUri = new Uri("http://localhost:8080/");
    }
  }
}
