using HomeKart.Data;
using HomeKart.Models;
using Microsoft.AspNetCore.Mvc;

namespace HomeKart.Controllers
{
    public class RegisterController : Controller
    {

        private readonly ApplicationDbContext _db;

        public RegisterController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(RegisterVM obj)
        {
            _db.Registers.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index", "SignIn", new { value = 5 });
        }
    }
}
