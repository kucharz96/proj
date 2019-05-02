using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Projekt1.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
           
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult AdminIndex()
        {
            ViewBag.Message = "This can be viewed only by users in Admin role only";
            return View();
        }
        public ActionResult Autocomplete(string term)
        {
            var items = new[] { "Audi", "Ferrari", "Porsche", "Audi Sport", "BMW" };
            var filteredItems = items.Where(
                item => item.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0
                );
            return Json(filteredItems, JsonRequestBehavior.AllowGet);
        }
    }
}