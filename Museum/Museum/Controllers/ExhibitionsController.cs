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
        public ActionResult CurrentExhibitionView(int Exhibition, string Name)
        {
            ViewBag.Title = Name;

            List<GetCurrentExhibition_Result> view = GetCurrentExhibitionView(Exhibition);

            return View(view);
        }
        public ActionResult ExhibitionsView()
        {
            return View(exhibitions);
        }

        private List<GetCurrentExhibition_Result> GetCurrentExhibitionView(int Exhibition)
        {
            List<GetCurrentExhibition_Result> resulset = null;

            var prmExhibition = new System.Data.SqlClient.SqlParameter("@p_Exhibition", System.Data.SqlDbType.Int);
            prmExhibition.Value = Exhibition;

            var result = db.Database.SqlQuery<GetCurrentExhibition_Result>("GetCurrentExhibition @p_Exhibition", prmExhibition).ToList();

            resulset = result;

            return resulset;
        }

        // GET: Exhibitions
        public ActionResult Index()
        {
            return View();
        }
    }
}