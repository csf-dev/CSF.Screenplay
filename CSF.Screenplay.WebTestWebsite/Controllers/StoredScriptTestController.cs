using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CSF.Screenplay.Selenium.StoredScripts;
using CSF.Screenplay.WebTestWebsite.Models;

namespace CSF.Screenplay.WebTestWebsite.Controllers
{
  public class StoredScriptTestController : Controller
  {
    public ActionResult Index(string id)
    {
      var provider = GetScriptProvider(id);
      if(provider == null) return HttpNotFound();

      var model = new JavaScriptTestModel {
        ScriptProvider = provider,
        BaseUri = new Uri($"http://localhost:8080/StoredScriptTest/Index/{id}"),
        TestFilename = $"{id}.tests.js",
      };

      return View(model);
    }

    IProvidesScript GetScriptProvider(string scriptName)
    {
      var providerType = GetScriptProviderType(scriptName);
      if(providerType == null) return null;
      return (IProvidesScript) Activator.CreateInstance(providerType);
    }

    Type GetScriptProviderType(string scriptName)
    {
      var providerBaseType = typeof(IProvidesScript);
      return providerBaseType
        .Assembly
        .GetExportedTypes()
        .FirstOrDefault(x => providerBaseType.IsAssignableFrom(x)
                             && x.Name.Equals(scriptName, StringComparison.InvariantCultureIgnoreCase));
    }
  }
}
