using GameRoomsAPI.DataBases.DAO;
using GameRoomsAPI.Models;
using GameRoomsAPI.WebSockets.Actions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace GameRoomsAPI.WebSockets
{
    public class UsersSocket : WebSocketBehavior
    {
        static Random rng = new Random();
        UserDao userDao = new UserDao();
        protected override void OnOpen()
        {
            User newUser = new User("Player " + rng.Next(1,9999), true, Sessions.IDs.LastOrDefault());
            userDao.Create(newUser);
            BroadcastOnlineUsers();
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            JToken token = JObject.Parse(e.Data);
            string actionType = (string)token.SelectToken("type");

            if (actionType == UserActions.GET_ONLINE_USERS)
                SendOnlineUsers();

        }

        private void SendOnlineUsers()
        {
            Send(JsonConvert.SerializeObject(new { type = "updateUsers", users = userDao.All().Where(p => p.Online)}));
        }
        private void BroadcastOnlineUsers()
        {
            Sessions.Broadcast(JsonConvert.SerializeObject(new { type = "updateUsers", users = userDao.All().Where(p => p.Online) }));
        }

        protected override void OnClose(CloseEventArgs e)
        {
            List<User> offlineUsers = new List<User>();
            foreach(User user in userDao.All())
            {
                if(Sessions.IDs.Contains(user.SessionId) != true)
                {
                    offlineUsers.Add(user);
                    userDao.SetUserStatus(user.Id, false);
                }
            }
            BroadcastOnlineUsers();
        }

        protected override void OnError(ErrorEventArgs e)
        {          
            base.OnError(e);
        }
    }
}