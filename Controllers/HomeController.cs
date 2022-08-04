using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RandomPasscodeGenerator.Models;
using Microsoft.AspNetCore.Http; // needed to use session in the controller
using System.Text;

namespace RandomPasscodeGenerator.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }


    [HttpGet("")]
    public IActionResult Index()
    {
        if(HttpContext.Session.GetInt32("Count") == null)
        {
            HttpContext.Session.SetInt32("Count", 0);
        }
        ViewBag.Count = HttpContext.Session.GetInt32("Count");
        ViewBag.RandomPassword = HttpContext.Session.GetString("passRandom");
        return View("Index");
    }

    [HttpGet("generate")]
    public IActionResult Generate()
    {
        string alphanumericString = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        Random randomIndex = new Random();
        string passwordString = "";
        for(int i = 0; i < 15; i++){
            passwordString += (alphanumericString[randomIndex.Next(0, alphanumericString.Length)]);
        }

        HttpContext.Session.SetString("passRandom", passwordString);
        HttpContext.Session.SetInt32("Count", (int)HttpContext.Session.GetInt32("Count") +1);

        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
