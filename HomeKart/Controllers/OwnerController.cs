using HomeKart.Data;
using HomeKart.Models;
using Microsoft.AspNetCore.Mvc;

namespace HomeKart.Controllers
{
    public class OwnerController : Controller
    {
        private readonly ApplicationDbContext _db;

        public OwnerController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<OwnerVM> objPropertyList = _db.Properties;
            return View(objPropertyList);
        }

        public IActionResult AddProperty()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProperty(OwnerVM obj)
        {
            if (ModelState.IsValid)
            {
                _db.Properties.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}
