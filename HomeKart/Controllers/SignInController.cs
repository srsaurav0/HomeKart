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

        public IActionResult Index()
        {
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
                }
            }
            if (IsUser == true)
            {
                return View("../Role/Index");
            }
            ViewBag.Error = 1;
            return View("Index");

        }
    }
}
