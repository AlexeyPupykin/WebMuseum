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

        public ActionResult RoomsView()
        {
            return View(rooms);
        }

        // GET: Rooms
        public ActionResult Index()
        {
            return View();
        }
    }
}