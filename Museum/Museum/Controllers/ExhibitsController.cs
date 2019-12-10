using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Museum.Models;

namespace Museum.Controllers
{
    public class ExhibitsController : Controller
    {
        private MuseumDBEntities db = new MuseumDBEntities();

        List<GetExhibits_Result> exhibits = null;

        public ExhibitsController()
        {

        }

        public ActionResult ExhibitsView()
        {
            var result = db.Database.SqlQuery<GetExhibits_Result>("GetExhibits").ToList();

            return View(result);
        }
        // GET: Exhibits
        public ActionResult Index()
        {
            return View();
        }
    }
}