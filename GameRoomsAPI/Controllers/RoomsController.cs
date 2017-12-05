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
using GameRoomsAPI.DAO;

namespace GameRoomsAPI.Controllers
{
    public class RoomsController : Controller
    {
        RoomDao roomDAO = new RoomDao();
        public ActionResult Index()
        {
            return Json(roomDAO.All(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(Room receivedRoom)
        {
            return Json(roomDAO.Create(receivedRoom));
        }

        [HttpPost]
        public ActionResult Update(Room receivedRoom)
        {  
            return Json(roomDAO.Update(receivedRoom));
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            return Json(roomDAO.Delete(id));
        }
    }
}