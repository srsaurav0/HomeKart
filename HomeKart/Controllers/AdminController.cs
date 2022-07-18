using HomeKart.Data;
using HomeKart.Models;
using Microsoft.AspNetCore.Mvc;

namespace HomeKart.Controllers
{
    public class AdminController : Controller
    {

        private readonly ApplicationDbContext _db;

        public AdminController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CheckAdmin(AdminVM admin)
        {
            List<AdminVM> adminList = new List<AdminVM>();
            adminList = _db.Admins.ToList();
            bool IsAdmin = false;
            foreach (var vm in adminList)
            {
                if (admin.Email == vm.Email && admin.Password == vm.Password)
                {
                    IsAdmin = true;
                }
            }
            if (IsAdmin == true)
            {
                return RedirectToAction("DataBase");
            }
            ViewBag.Error = 2;
            return View("Index", "Admin");
        }

        public IActionResult DataBase()
        {
            var tables = new TableVM
            {
                AdminTab = _db.Admins.ToList(),
                UserTab = _db.Registers.ToList(),
                PropertyTab = _db.Properties.ToList()
            };

            return View(tables);
        }


        //GET
        public IActionResult DeleteUser(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var userFromDb = _db.Registers.Find(id);

            if (userFromDb == null)
            {
                return NotFound();
            }
            return View(userFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteUsr(int? id)
        {
            var obj = _db.Registers.Find(id);
            foreach (var objProp in _db.Properties)
            {
                if (objProp.userId == id)
                {
                    _db.Properties.Remove(objProp);
                }
            }
            _db.Registers.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("DataBase");
        }


        //GET
        public IActionResult DeleteProperty(int? id)
        {
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
            return RedirectToAction("DataBase");
        }
    }
}
