using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CSF.Screenplay.Selenium.TestWebApp;

public class DelayedOpeningController : Controller
{
    [HttpGet, Route("DelayedOpening")]
    public async Task<ViewResult> Index(int delaySeconds = 2)
    {
        await Task.Delay(TimeSpan.FromSeconds(delaySeconds));
        return View(delaySeconds);
    }
}