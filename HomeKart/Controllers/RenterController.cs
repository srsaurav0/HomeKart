using HomeKart.Data;
using HomeKart.Models;
using Microsoft.AspNetCore.Mvc;

namespace HomeKart.Controllers
{
    public class RenterController : Controller
    {
        private readonly ApplicationDbContext _db;

        public RenterController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            ViewBag.Name = HttpContext.Session.GetString("usrName");
            FilterVM obj = new FilterVM();
            obj.PropertyTab = _db.Properties;
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Filter(FilterVM obj)
        {
            if (obj.Upper != 0 && obj.Lower != 0)
            {
                FilterVM objNew = new FilterVM();
                objNew.PropertyTab = _db.Properties.Where(x => x.Rent_Amount <= obj.Upper && x.Rent_Amount >= obj.Lower);
                return View("Index", objNew);
            }
            if (obj.Upper != 0)
            {
                FilterVM objNew = new FilterVM();
                objNew.PropertyTab = _db.Properties.Where(x => x.Rent_Amount <= obj.Upper);
                return View("Index", objNew);
            }
            if (obj.Lower != 0)
            {
                FilterVM objNew = new FilterVM();
                objNew.PropertyTab = _db.Properties.Where(x => x.Rent_Amount >= obj.Lower);
                return View("Index", objNew);
            }
            return View("Index", "Renter");
        }
    }
}
