using GameRoomsAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Web.Mvc;
using GameRoomsAPI.Models;
using Newtonsoft.Json.Linq;
using System.Data.Entity;

namespace GameRoomsAPI.Controllers
{
    public class RoomsController : Controller
    {
        // GET: Rooms
        public ActionResult Index()
        {
            List<Room> rooms = null;
            using (RoomContext db = new RoomContext())
            {
                rooms = db.Rooms.ToList();
            }
            return Json(rooms, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(Room receivedRoom)
        {
            Room room = new Room(receivedRoom);
            using (RoomContext db = new RoomContext())
            {
                db.Rooms.Add(room);
                db.SaveChanges();
                room.StartServer(room.Id);
                db.SaveChanges();
            }
            
            return Json(room);
        }

        [HttpPost]
        public ActionResult Update(Room receivedRoom)
        {
            Room returnedRoom = null;
            using (RoomContext db = new RoomContext())
            {
                Room updatedRoom = db.Rooms.Where(c => c.Id == receivedRoom.Id).FirstOrDefault();
                updatedRoom.Update(receivedRoom);
                returnedRoom = updatedRoom;
                db.SaveChanges();
            }

            return Json(returnedRoom);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Room returnedRoom = null;
            using (RoomContext db = new RoomContext())
            {
                Room deletedRoom = db.Rooms.Where(c => c.Id == id).FirstOrDefault();
                returnedRoom = deletedRoom;
                db.Rooms.Remove(deletedRoom);
                db.SaveChanges();
            }

            return Json(returnedRoom);
        }
    }
}