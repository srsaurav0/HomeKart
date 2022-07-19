using Microsoft.AspNetCore.Mvc;

namespace HomeKart.Controllers
{
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("isLogged") != "Yes")
            {
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddSeconds(5);
                Response.Cookies.Append("LogOut", "Out", options);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Name = HttpContext.Session.GetString("usrName");
            return View();
        }
    }
}
