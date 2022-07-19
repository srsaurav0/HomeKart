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
            if (HttpContext.Session.GetString("isLogged") == null)
            {
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddSeconds(5);
                Response.Cookies.Append("LogOut", "Out", options);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Name = HttpContext.Session.GetString("usrName");
            FilterVM obj = new FilterVM();
            obj.PropertyTab = _db.Properties;
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Filter(FilterVM obj)
        {
            if (obj.City == null && (obj.Upper == 0 || obj.Upper == null) && (obj.Lower == 0 || obj.Lower == null))
            {
                return RedirectToAction("Index", "Renter");
            }
            if (obj.City != null)
            {
                if (obj.Upper != 0 && obj.Lower != 0)
                {
                    FilterVM objNew = new FilterVM();
                    objNew.PropertyTab = _db.Properties.Where(x => x.Rent_Amount <= obj.Upper && x.Rent_Amount >= obj.Lower && x.Address == obj.City);
                    return View("Index", objNew);
                }
                if (obj.Upper != 0)
                {
                    FilterVM objNew = new FilterVM();
                    objNew.PropertyTab = _db.Properties.Where(x => x.Rent_Amount <= obj.Upper && x.Address == obj.City);
                    return View("Index", objNew);
                }
                if (obj.Lower != 0)
                {
                    FilterVM objNew = new FilterVM();
                    objNew.PropertyTab = _db.Properties.Where(x => x.Rent_Amount >= obj.Lower && x.Address == obj.City);
                    return View("Index", objNew);
                }
                else
                {
                    FilterVM objNew = new FilterVM();
                    objNew.PropertyTab = _db.Properties.Where(x => x.Address == obj.City);
                    return View("Index", objNew);
                }
            }
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
