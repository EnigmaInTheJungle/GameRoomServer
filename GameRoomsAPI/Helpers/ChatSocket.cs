using GameRoomsAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace GameRoomsAPI.Helpers
{
    class ChatSocket : WebSocketBehavior
    {
        int _roomId = 0;
        protected override void OnOpen()
        {
            if(_roomId == 0)
                _roomId = Convert.ToInt32(Context.RequestUri.AbsolutePath.Split('/').Last());

            Room room = null;
            using (RoomContext db = new RoomContext())
            {
                room = db.Rooms.Where(c => c.Id == _roomId).FirstOrDefault();
                room.AddUser("sds");
                db.SaveChanges();
            }
            Sessions.Broadcast(JsonConvert.SerializeObject(new {type = "userJoined", players = room.CurrentPlayers}));
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            //JToken token = JObject.Parse(e.Data);
            Sessions.Broadcast(JsonConvert.SerializeObject(new { type = "messageSended", message = e.Data }));  
        }

        protected override void OnClose(CloseEventArgs e)
        {
            if (_roomId == 0)
                _roomId = Convert.ToInt32(Context.RequestUri.AbsolutePath.Split('/').Last());

            Room room = null;
            using (RoomContext db = new RoomContext())
            {
                room = db.Rooms.Where(c => c.Id == _roomId).FirstOrDefault();
                if (room != null)
                {
                    room.DeleteUser("sds");
                    if (room.CurrentPlayers == 0)
                        db.Rooms.Remove(room);
                    db.SaveChanges();
                }
            }
            Sessions.Broadcast(JsonConvert.SerializeObject(new { type = "userJoined", players = room.CurrentPlayers }));
        }

        protected override void OnError(ErrorEventArgs e)
        {
            base.OnError(e);
        }
    }
}
