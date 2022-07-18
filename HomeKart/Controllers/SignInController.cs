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
                    HttpContext.Session.SetInt32("usrId", (int)vm.Id);
                    HttpContext.Session.SetString("usrName", vm.Name.ToString());
                    HttpContext.Session.SetString("isLogged", "Yes");
                }
            }
            if (IsUser == true)
            {
                return RedirectToAction("Index", "Role");
            }

            ViewBag.Error = 1;
            return View("Index");

        }

        public IActionResult SignOut()
        {
            HttpContext.Session.Remove("isLogged");
            return RedirectToAction("Index", "Home");
        }
    }
}
