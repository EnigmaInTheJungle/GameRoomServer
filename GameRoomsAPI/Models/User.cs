using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameRoomsAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Online { get; set; }
        public string SessionId { get; set; }

        public User()
        {

        }

        public User(User user)
        {
            Name = user.Name;
            Online = user.Online;
            SessionId = user.SessionId;
        }

        public void Update(User user)
        {
            Name = user.Name;
            Online = user.Online;
            SessionId = user.SessionId;
        }

        public User(string name, bool online, string sessionId)
        {
            Name = name;
            Online = online;
            SessionId = sessionId;
        }
    }
}