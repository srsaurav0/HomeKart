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
            ViewBag.Name = HttpContext.Session.GetString("usrName");
            IEnumerable<OwnerVM> objPropertyList = _db.Properties.Where(x => x.userId == (int)HttpContext.Session.GetInt32("usrId"));
            return View(objPropertyList);
        }

        //GET
        public IActionResult AddProperty()
        {
            ViewBag.Name = HttpContext.Session.GetString("usrName");
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProperty(OwnerVM obj)
        {

            if (ModelState.IsValid)
            {
                obj.userId = (int)HttpContext.Session.GetInt32("usrId");
                _db.Properties.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult UpdateProperty(int? id)
        {
            ViewBag.Name = HttpContext.Session.GetString("usrName");
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var propertyFromDb = _db.Properties.Find(id);

            if (propertyFromDb == null)
            {
                return NotFound();
            }
            return View(propertyFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateProperty(OwnerVM obj)
        {

            if (ModelState.IsValid)
            {
                obj.userId = (int)HttpContext.Session.GetInt32("usrId");
                _db.Properties.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult DeleteProperty(int? id)
        {
            ViewBag.Name = HttpContext.Session.GetString("usrName");
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var propertyFromDb = _db.Properties.Find(id);

            if (propertyFromDb == null)
            {
                return NotFound();
            }
            return View(propertyFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? id)
        {

            var obj = _db.Properties.Find(id);
            _db.Properties.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
