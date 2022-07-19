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
            if (HttpContext.Session.GetString("isLogged") == null)
            {
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddSeconds(5);
                Response.Cookies.Append("LogOut", "Out", options);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Add = Request.Cookies["Add"];
            ViewBag.Name = HttpContext.Session.GetString("usrName");
            IEnumerable<OwnerVM> objPropertyList = _db.Properties.Where(x => x.userId == (int)HttpContext.Session.GetInt32("usrId"));
            return View(objPropertyList);
        }

        //GET
        public IActionResult AddProperty()
        {
            if (HttpContext.Session.GetString("isLogged") == null)
            {
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddSeconds(5);
                Response.Cookies.Append("LogOut", "Out", options);
                return RedirectToAction("Index", "Home");
            }
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
            if (HttpContext.Session.GetString("isLogged") == null)
            {
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddSeconds(5);
                Response.Cookies.Append("LogOut", "Out", options);

                return RedirectToAction("Index", "Home");
            }

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
            if (HttpContext.Session.GetString("isLogged") == null)
            {
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddSeconds(5);
                Response.Cookies.Append("LogOut", "Out", options);

                return RedirectToAction("Index", "Home");
            }

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
