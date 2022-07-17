using HomeKart.Data;
using HomeKart.Models;
using Microsoft.AspNetCore.Mvc;


namespace HomeKart.Controllers
{
    public class SignInController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SignInController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(int value)
        {
            if (value == 5)
            {
                ViewBag.OnCreate = "Congratulations!" +
                "Your account was created successfully.";
            }
            return View();
        }

        [HttpPost]
        public IActionResult CheckUser(RegisterVM user)
        {
            List<RegisterVM> userList = new List<RegisterVM>();
            userList = _db.Registers.ToList();
            bool IsUser = false;
            foreach (var vm in userList)
            {
                if (user.Email == vm.Email && user.Password == vm.Password)
                {
                    IsUser = true;
                    TempData["userId"] = vm.Id;
                    HttpContext.Session.SetInt32("usrId", (int)vm.Id);
                }
            }
            if (IsUser == true)
            {
                return RedirectToAction("Index", "Role");
            }

            ViewBag.Error = 1;
            return View("Index");

        }
    }
}
