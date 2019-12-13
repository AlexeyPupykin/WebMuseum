using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Museum.Models;

namespace Museum.Controllers
{
    public class HomeController : Controller
    {
        private MuseumDBEntities db = new MuseumDBEntities();

        List<Style> styles = null;
        List<Models.Type> types = null;

        public HomeController()
        {
            styles = db.Styles.ToList();
            types = db.Types.ToList();
        }

        public ActionResult Index()
        {
            ViewBag.Styles = styles;
            ViewBag.Types = types;

            return View();
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