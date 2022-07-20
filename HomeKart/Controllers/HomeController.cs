using HomeKart.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HomeKart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.LogChk = HttpContext.Session.GetString("isLogged");
            ViewBag.LogOut = null;
            if (ViewBag.LogChk != null)
            {
                ViewBag.Name = HttpContext.Session.GetString("usrName");
                return View();
            }
            if (Request.Cookies["LogOut"] != null)
            {
                ViewBag.LogOut = "Please log in to continue";
            }
            return View();
        }

        public IActionResult Privacy()
        {
            ViewBag.LogChk = HttpContext.Session.GetString("isLogged");
            if (ViewBag.LogChk != null)
            {
                ViewBag.Name = HttpContext.Session.GetString("usrName");
                return View();
            }
            return View();
        }

        public IActionResult About()
        {
            ViewBag.LogChk = HttpContext.Session.GetString("isLogged");
            if (ViewBag.LogChk != null)
            {
                ViewBag.Name = HttpContext.Session.GetString("usrName");
                return View();
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}