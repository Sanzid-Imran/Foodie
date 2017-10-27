using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Foodie.Models;

namespace Foodie.Controllers
{
    public class HomeController : Controller
    {
        ResContext db = new ResContext();
        public ActionResult Index()
        {
            var allElement = from s in db.FoodDetails select s;

            return View(allElement);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}