using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Museum.Models;

namespace Museum.Controllers
{
    public class RoomsController : Controller
    {
        private MuseumDBEntities db = new MuseumDBEntities();

        List<Room> rooms = null;

        public RoomsController()
        {
            rooms = db.Rooms.ToList();
        }

        public ActionResult CurrentRoomView(int Room, string Name)
        {
            ViewBag.Title = Name;

            List<GetCurrentRoom_Result> view = GetCurrentRoomView(Room);

            return View(view);
        }

        public ActionResult RoomsView()
        {
            return View(rooms);
        }

        private List<GetCurrentRoom_Result> GetCurrentRoomView(int Room)
        {
            List<GetCurrentRoom_Result> resultset = null;

            var prmIdRoom = new System.Data.SqlClient.SqlParameter("@p_Room", System.Data.SqlDbType.Int);
            prmIdRoom.Value = Room;

            var result = db.Database.SqlQuery<GetCurrentRoom_Result>("GetCurrentRoom @p_Room", prmIdRoom).ToList();

            resultset = result;

            return resultset;
        }

        // GET: Rooms
        public ActionResult Index()
        {
            return View();
        }
    }
}