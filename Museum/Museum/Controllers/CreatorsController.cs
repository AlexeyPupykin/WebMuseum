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

        public ActionResult ChangeCreatorView(int id)
        {
            var c = creators.Find(x => x.IDCreator == id);

            return View("ChangeCreatorView", c);
        }

        public ActionResult ChangeCreator(int id, string name, string country, string lifetime)
        {
            var prmid = new System.Data.SqlClient.SqlParameter("@IDCreator", System.Data.SqlDbType.Int);
            prmid.Value = id;

            var prmname = new System.Data.SqlClient.SqlParameter("@Name", System.Data.SqlDbType.NVarChar);
            prmname.Value = name;

            var prmcountry = new System.Data.SqlClient.SqlParameter("@Country", System.Data.SqlDbType.NVarChar);
            prmcountry.Value = country;

            var prmlifetime = new System.Data.SqlClient.SqlParameter("@Lifetime", System.Data.SqlDbType.NVarChar);
            prmlifetime.Value = lifetime;

            db.Database.ExecuteSqlCommand(
                "UPDATE Creators SET " +
                "Name = @Name, Counrty = @Country, Lifetime = @LIfetime " +
                "WHERE IDCreator = @IDCreator"
                , prmname, prmcountry, prmlifetime, prmid);

            return View("CreatorsView");
        }

        public ActionResult DeleteCreator(int id)
        {
            var prmid = new System.Data.SqlClient.SqlParameter("@IDCreator", System.Data.SqlDbType.Int);
            prmid.Value = id;

            db.Database.ExecuteSqlCommand("DELETE FROM Creators WHERE IDCreator = @IDCreator", prmid);

            return View("CreatorsView");
        }

        // GET: Creators
        public ActionResult OutPutCreators()
        {
            return PartialView("_CreatorsViewList", creators);
        }
    }
}