using GameRoomsAPI.DAO;
using GameRoomsAPI.DataBases.DAO;
using GameRoomsAPI.Helpers;
using GameRoomsAPI.Models;
using GameRoomsAPI.WebSockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GameRoomsAPI
{
    public class RouteConfig
    {
        static RoomDao roomDAO = new RoomDao();
        public static void RegisterRoutes(RouteCollection routes)
        {         
            UserDao userDao = new UserDao();
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            WebSocket.Start();
            WebSocket.WS.AddWebSocketService<UsersSocket>("/Users");
            WebSocket.WS.AddWebSocketService<RoomsSocket>("/Rooms", () => new RoomsSocket(roomDAO));
            roomDAO.DeleteAll();
            userDao.DeleteAll();
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
