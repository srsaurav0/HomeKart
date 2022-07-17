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
    }
}
