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
        public static void RegisterRoutes(RouteCollection routes)
        {
            RoomDao roomDAO = new RoomDao();
            UserDao userDao = new UserDao();
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            WebSocket.Start();
            WebSocket.WS.AddWebSocketService<UsersSocket>("/Users");
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
