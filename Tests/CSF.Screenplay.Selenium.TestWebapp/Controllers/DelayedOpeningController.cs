using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

public class DelayedOpeningController : Controller
{
    [HttpGet, Route("DelayedOpening")]
    public async Task<ViewResult> Index()
    {
        await Task.Delay(TimeSpan.FromSeconds(2));
        return View();
    }
}