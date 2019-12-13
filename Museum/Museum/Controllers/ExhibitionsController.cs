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
            ViewBag.IDExhibition = Exhibition;
            ViewBag.Rooms = db.Rooms.ToList();

            List<GetCurExhibition_Result> view = GetCurrentExhibitionView(Exhibition);

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

            return View("ExhibitionsView");
        }

        public ActionResult ChangeExhibitionView(int id)
        {
            var e = exhibitions.Find(x => x.IDExhibition == id);

            return View("ChangeExhibitionView", e);
        }

        public ActionResult ChangeExhibition(int id, string name, string begtime, string endtime,
            string country, string city, string place, string person)
        {
            var prmid = new System.Data.SqlClient.SqlParameter("@IDExhibition", System.Data.SqlDbType.Int);
            prmid.Value = id;

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

            var prmplace = new System.Data.SqlClient.SqlParameter("@Place", System.Data.SqlDbType.NVarChar);
            prmplace.Value = place;

            var prmperson = new System.Data.SqlClient.SqlParameter("@PersonInCharge", System.Data.SqlDbType.NVarChar);
            prmperson.Value = person;

            if(prmbegtime.Value.ToString() != "")
            {
                db.Database.ExecuteSqlCommand(
                "UPDATE Exhibitions SET " +
                "DateStart = @DateStart, DateStop = @DateStop, Name = @Name," +
                "Country = @Country, City = @City, Place = @Place, " +
                "PersonInCharge = @PersonInCharge WHERE IDExhibition = @IDExhibition"
                , prmbegtime, prmendtime, prmname, prmcountry, prmcity, prmplace, prmperson, prmid);
            }
            else
            {
                db.Database.ExecuteSqlCommand(
                "UPDATE Exhibitions SET " +
                "Name = @Name, Country = @Country, City = @City, Place = @Place, " +
                "PersonInCharge = @PersonInCharge WHERE IDExhibition = @IDExhibition"
                , prmname, prmcountry, prmcity, prmplace, prmperson, prmid);
            }

            return View("ExhibitionsView");
        }

        public ActionResult DeleteExhibition (int id)
        {
            var prmid = new System.Data.SqlClient.SqlParameter("@IDExhibition", System.Data.SqlDbType.Int);
            prmid.Value = id;

            db.Database.ExecuteSqlCommand("DELETE FROM Exhibitions WHERE IDExhibition = @IDExhibition", prmid);

            return View("ExhibitionsView");
        }

        public ActionResult OutPutExhibitions()
        {
            return PartialView("_ExhibitionsViewList", exhibitions);
        }

        public ActionResult ExhibitionsView()
        {
            return View(exhibitions);
        }

        private List<GetCurExhibition_Result> GetCurrentExhibitionView(int Exhibition)
        {
            List<GetCurExhibition_Result> resulset = null;

            var prmExhibition = new System.Data.SqlClient.SqlParameter("@p_Exhibition", System.Data.SqlDbType.Int);
            prmExhibition.Value = Exhibition;

            var result = db.Database.SqlQuery<GetCurExhibition_Result>("GetCurExhibition @p_Exhibition", prmExhibition).ToList();

            resulset = result;

            return resulset;
        }

        public ActionResult AddExhibitInExhibitionView(int IdExhibition)
        {
            var exhibits_in_museum = db.Database.SqlQuery<GetExhibitsInMuseum_Result>("GetExhibitsInMuseum").ToList();

            ViewBag.IDExhibition = IdExhibition;
            ViewBag.NameExhibition = db.Exhibitions.ToList().Find(s => s.IDExhibition == IdExhibition).Name;

            return View("AddExhibitInExhibitionView", exhibits_in_museum);
        }

        public ActionResult AddExhibitInExhibition(int IdExhibit, int IdExhibition)
        {
            var prmexhibition = new System.Data.SqlClient.SqlParameter("@IDExhibition", System.Data.SqlDbType.Int);
            prmexhibition.Value = IdExhibition;

            var prmexhibit = new System.Data.SqlClient.SqlParameter("@IDExhibit", System.Data.SqlDbType.Int);
            prmexhibit.Value = IdExhibit;

            db.Database.ExecuteSqlCommand(
                "UPDATE Exhibits SET " +
                "IDExhibition = @IDExhibition, IDRoom = NULL " +
                "WHERE IDExhibit = @IDExhibit"
                , prmexhibition, prmexhibit);

            return View("ExhibitionsView");
        }

        public ActionResult ReturnExhibitFromExhibition(int IdExhibit, int IdRoom)
        {
            var prmroom = new System.Data.SqlClient.SqlParameter("@IDRoom", System.Data.SqlDbType.Int);
            prmroom.Value = IdRoom;

            var prmexhibit = new System.Data.SqlClient.SqlParameter("@IDExhibit", System.Data.SqlDbType.Int);
            prmexhibit.Value = IdExhibit;

            db.Database.ExecuteSqlCommand(
                "UPDATE Exhibits SET " +
                "IDExhibition = NULL , IDRoom = @IDRoom " +
                "WHERE IDExhibit = @IDExhibit"
                , prmroom, prmexhibit);

            return View("ExhibitionsView");
        }

        // GET: Exhibitions
        public ActionResult Index()
        {
            return View();
        }
    }
}