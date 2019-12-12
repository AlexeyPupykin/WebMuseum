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

        public ActionResult AddCreator(string name, string country, string lifetime)
        {
            var prmname = new System.Data.SqlClient.SqlParameter("@Name", System.Data.SqlDbType.NVarChar);
            prmname.Value = name;

            var prmcountry = new System.Data.SqlClient.SqlParameter("@Country", System.Data.SqlDbType.NVarChar);
            prmcountry.Value = country;

            var prmlifetime = new System.Data.SqlClient.SqlParameter("@Lifetime", System.Data.SqlDbType.NVarChar);
            prmlifetime.Value = lifetime;

            db.Database.ExecuteSqlCommand(
                "INSERT INTO Creators VALUES" +
                "(@Name, @Country, @Lifetime)",
                prmname, prmcountry, prmlifetime);

            return View("CreatorsView");
        }

        // GET: Creators
        public ActionResult OutPutCreators()
        {
            return PartialView("_CreatorsViewList", creators);
        }
    }
}