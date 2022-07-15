using Microsoft.AspNetCore.Mvc;

namespace HomeKart.Controllers
{
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
