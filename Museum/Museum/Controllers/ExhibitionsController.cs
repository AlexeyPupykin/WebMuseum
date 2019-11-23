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

        public ActionResult AddExhibitionView(string name, DateTime begtime, DateTime endtime, 
            string country, string city, string place, string person)
        {
            var prmname = new System.Data.SqlClient.SqlParameter("@Name", System.Data.SqlDbType.NVarChar);
            prmname.Value = name;
            
            var prmbegtime = new System.Data.SqlClient.SqlParameter("@DateStart", System.Data.SqlDbType.DateTime);
            prmbegtime.Value = begtime;

            var prmendtime = new System.Data.SqlClient.SqlParameter("@DateStop", System.Data.SqlDbType.DateTime);
            prmendtime.Value = endtime;

            var prmcountry = new System.Data.SqlClient.SqlParameter("@Country", System.Data.SqlDbType.NVarChar);
            prmcountry.Value = country;

            var prmcity = new System.Data.SqlClient.SqlParameter("@City", System.Data.SqlDbType.NVarChar);
            prmcity.Value = city;

            var prmplace= new System.Data.SqlClient.SqlParameter("@Place", System.Data.SqlDbType.NVarChar);
            prmplace.Value = place;

            var prmperson = new System.Data.SqlClient.SqlParameter("@PersonInCharge", System.Data.SqlDbType.NVarChar);
            prmperson.Value = person;

            db.Database.ExecuteSqlCommand(
                "INSERT INTO Exhibitions VALUES(@DateStart, @DateStop, @Name, @Country," +
                "@City, @Place, @PersonInCharge)", prmbegtime, prmendtime, prmname, 
                prmcountry, prmcity, prmplace, prmperson );

            exhibitions = db.Exhibitions.ToList();

            return PartialView("_ExhibitionsViewList", exhibitions);
        }

        public ActionResult OutPutExhibitions()
        {
            return PartialView("_ExhibitionsViewList", exhibitions);
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