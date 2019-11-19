using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Museum.Models;

namespace Museum.Controllers
{
    public class CreatorsController : Controller
    {
        private MuseumDBEntities db = new MuseumDBEntities();

        List<Creator> creators = null;

        public CreatorsController()
        {
            creators = db.Creators.ToList();
        }

        public ActionResult CreatorsView()
        {
            return View();
        }

        // GET: Creators
        public ActionResult OutPutCreators()
        {
            return PartialView("_CreatorsViewList", creators);
        }
    }
}