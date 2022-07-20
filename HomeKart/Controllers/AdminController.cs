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
            if (Request.Cookies["NotAdm"] != null)
            {
                ViewBag.NotAdm = "Invalid username or password!";
            }
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
                HttpContext.Session.SetString("admLogged", "Yes");
                return RedirectToAction("DataBase");
            }

            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddSeconds(5);
            Response.Cookies.Append("NotAdm", "Yes", options);

            return View("Index", "Admin");
        }

        public IActionResult SignOutAdmin()
        {
            HttpContext.Session.Remove("admLogged");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult DataBase()
        {
            if (HttpContext.Session.GetString("admLogged") == null)
            {
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddSeconds(5);
                Response.Cookies.Append("LogOut", "Out", options);
                return RedirectToAction("Index", "Home");
            }
            var tables = new TableVM
            {
                AdminTab = _db.Admins.ToList(),
                UserTab = _db.Registers.ToList(),
                PropertyTab = _db.Properties.ToList()
            };

            if (Request.Cookies["DeleteUsr"] != null)
            {
                ViewBag.AddProp = "A user and all his/her properties were deleted successfully!";
            }

            if (Request.Cookies["DeletePr"] != null)
            {
                ViewBag.AddProp = "A property was deleted successfully!";
            }

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

            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddSeconds(5);
            Response.Cookies.Append("DeleteUsr", "Added", options);

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

            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddSeconds(5);
            Response.Cookies.Append("DeletePr", "Added", options);

            return RedirectToAction("DataBase");
        }
    }
}
