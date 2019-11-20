using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Museum.Models;

namespace Museum.Controllers
{
    public class ExhibitionsController : Controller
    {
        private MuseumDBEntities db = new MuseumDBEntities();

        List<Exhibition> exhibitions = null;

        public ExhibitionsController()
        {
            exhibitions = db.Exhibitions.ToList();
        }

        public ActionResult ExhibitionsView()
        {
            return View(exhibitions);
        }

        // GET: Exhibitions
        public ActionResult Index()
        {
            return View();
        }
    }
}