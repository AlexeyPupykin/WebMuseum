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
            var result = db.Database.SqlQuery<GetExhibits_Result>("GetExhibits").ToList();

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

            exhibits = db.Database.SqlQuery<GetExhibits_Result>("GetExhibits").ToList();

            return View("ExhibitsView", exhibits);
        }
        // GET: Exhibits
        public ActionResult Index()
        {
            return View();
        }
    }
}