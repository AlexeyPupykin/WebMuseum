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

        List<GetAllExhibits_Result> exhibits = null;
        List<Room> rooms = null;
        List<Models.Type> types = null;
        List<Creator> creators = null;
        List<Style> styles = null;

        public ExhibitsController()
        {
            rooms = db.Rooms.ToList();
            types = db.Types.ToList();
            creators = db.Creators.ToList();
            styles = db.Styles.ToList();

            ViewBag.Rooms = rooms;
            ViewBag.Types = types;
            ViewBag.Creators = creators;
            ViewBag.Styles = styles;
        }

        public ActionResult ExhibitsView()
        {
            var result = db.Database.SqlQuery<GetAllExhibits_Result>("GetAllExhibits").ToList();

            return View(result);
        }

        public ActionResult AddExhibitView(string name, string description, string date,
            string style, string creator, string type, string room)
        {
            var prmname = new System.Data.SqlClient.SqlParameter("@Name", System.Data.SqlDbType.NVarChar);
            prmname.Value = name;

            var prmdescription = new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar);
            prmdescription.Value = description;

            var prmdate = new System.Data.SqlClient.SqlParameter("@DateOfCreation", System.Data.SqlDbType.NVarChar);
            prmdate.Value = date;

            var prmstyle = new System.Data.SqlClient.SqlParameter("@IDStyle", System.Data.SqlDbType.Int);
            prmstyle.Value = Convert.ToInt32(style);

            var prmcreator = new System.Data.SqlClient.SqlParameter("@IDCreator", System.Data.SqlDbType.Int);
            prmcreator.Value = Convert.ToInt32(creator);

            var prmtype = new System.Data.SqlClient.SqlParameter("@IDType", System.Data.SqlDbType.Int);
            prmtype.Value = Convert.ToInt32(type);

            var prmroom = new System.Data.SqlClient.SqlParameter("@IDRoom", System.Data.SqlDbType.Int);
            prmroom.Value = Convert.ToInt32(room);

            db.Database.ExecuteSqlCommand(
                "INSERT INTO Exhibits " +
                "(IDRoom, IDType, IDCreator, IDStyle, Name, Description, DateOfCreation)" +
                "VALUES" +
                "(@IDRoom, @IDType, @IDCreator, @IDStyle, @Name, @Description, @DateOfCreation)"
                , prmroom, prmtype, prmcreator, prmstyle, prmname, prmdescription, prmdate
            );

            exhibits = db.Database.SqlQuery<GetAllExhibits_Result>("GetAllExhibits").ToList();

            return View("ExhibitsView", exhibits);
        }

        public ActionResult ChangeExhibitView (int id)
        {
            var e = db.Database.SqlQuery<GetAllExhibits_Result>("GetAllExhibits").ToList().Find(x => x.IDExhibit == id);

            ViewBag.CurStyle = db.Styles.ToList().Find(s => s.Name == 
            db.Database.SqlQuery<GetAllExhibits_Result>("GetAllExhibits").ToList().Find(x => x.IDExhibit == id).Style).IDStyle;

            ViewBag.CurCreator = db.Creators.ToList().Find(s => s.Name ==
            db.Database.SqlQuery<GetAllExhibits_Result>("GetAllExhibits").ToList().Find(x => x.IDExhibit == id).Creator).IDCreator;

            ViewBag.CurType = db.Types.ToList().Find(s => s.Name ==
            db.Database.SqlQuery<GetAllExhibits_Result>("GetAllExhibits").ToList().Find(x => x.IDExhibit == id).Type).IDType;

            if(db.Database.SqlQuery<GetAllExhibits_Result>("GetAllExhibits").ToList().Find(x => x.IDExhibit == id).Room == null)
            {
                ViewBag.CurPlace = db.Exhibitions.ToList().Find(s => s.IDExhibition ==
            db.Database.SqlQuery<GetAllExhibits_Result>("GetAllExhibits").ToList().Find(x => x.IDExhibit == id).Exhibition).Name;
                ViewBag.Info = "OnExhibition";
            }
            else
            {
                ViewBag.CurPlace = db.Rooms.ToList().Find(s => s.IDRoom ==
            db.Database.SqlQuery<GetAllExhibits_Result>("GetAllExhibits").ToList().Find(x => x.IDExhibit == id).Room).Name;
                ViewBag.Info = "InRoom";
            }

            ViewBag.Types = types;
            ViewBag.Creators = creators;
            ViewBag.Styles = styles;

            return View("ChangeExhibitView", e);
        }

        public ActionResult ChangeExhibit (int id, string name, string description, string date,
            string style, string creator, string type, string place, string info_place)
        {
            var prmid = new System.Data.SqlClient.SqlParameter("@IDExhibit", System.Data.SqlDbType.Int);
            prmid.Value = id;

            var prmname = new System.Data.SqlClient.SqlParameter("@Name", System.Data.SqlDbType.NVarChar);
            prmname.Value = name;

            var prmdescription = new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar);
            prmdescription.Value = description;

            var prmdate = new System.Data.SqlClient.SqlParameter("@DateOfCreation", System.Data.SqlDbType.NVarChar);
            prmdate.Value = date;

            var prmstyle = new System.Data.SqlClient.SqlParameter("@IDStyle", System.Data.SqlDbType.Int);
            prmstyle.Value = Convert.ToInt32(style);

            var prmcreator = new System.Data.SqlClient.SqlParameter("@IDCreator", System.Data.SqlDbType.Int);
            prmcreator.Value = Convert.ToInt32(creator);

            var prmtype = new System.Data.SqlClient.SqlParameter("@IDType", System.Data.SqlDbType.Int);
            prmtype.Value = Convert.ToInt32(type);

            if(info_place == "Зал")
            {
                var prmroom = new System.Data.SqlClient.SqlParameter("@IDRoom", System.Data.SqlDbType.Int);
                prmroom.Value = Convert.ToInt32(place);

                db.Database.ExecuteSqlCommand(
                "UPDATE Exhibits SET " +
                "Name = @Name, Description = @Description, DateOfCreation = @DateOfCreation," +
                "IDStyle = @IDStyle, IDCreator = @IDCreator, IDType = @IDType, " +
                "IDRoom = @IDRoom WHERE IDExhibit = @IDExhibit"
                , prmname, prmdescription, prmdate, prmstyle, prmcreator, prmtype, prmroom, prmid);
            }
            else
            {
                var prmexhibition = new System.Data.SqlClient.SqlParameter("@IDExhibition", System.Data.SqlDbType.Int);
                prmexhibition.Value = Convert.ToInt32(place);

                db.Database.ExecuteSqlCommand(
                "UPDATE Exhibits SET " +
                "Name = @Name, Description = @Description, DateOfCreation = @DateOfCreation," +
                "IDStyle = @IDStyle, IDCreator = @IDCreator, IDType = @IDType, " +
                "IDExhibition = @IDExhibition WHERE IDExhibit = @IDExhibit"
                , prmname, prmdescription, prmdate, prmstyle, prmcreator, prmtype, prmexhibition, prmid);
            }

            exhibits = db.Database.SqlQuery<GetAllExhibits_Result>("GetAllExhibits").ToList();

            return View("ExhibitsView", exhibits);
        }

        public ActionResult DeleteExhibit (int id)
        {
            var prmid = new System.Data.SqlClient.SqlParameter("@IDExhibit", System.Data.SqlDbType.Int);
            prmid.Value = id;

            db.Database.ExecuteSqlCommand("DELETE FROM Exhibits WHERE IDExhibit = @IDExhibit", prmid);

            exhibits = db.Database.SqlQuery<GetAllExhibits_Result>("GetAllExhibits").ToList();

            return View("ExhibitsView", exhibits);
        }
        // GET: Exhibits
        public ActionResult Index()
        {
            return View();
        }
    }
}