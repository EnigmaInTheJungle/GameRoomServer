using GameRoomsAPI.DAO;
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
    public class RoomsSocket : WebSocketBehavior
    {
        static Random rng = new Random();
        RoomDao roomDao;

        public RoomsSocket(RoomDao dao)
        {
            roomDao = dao;
            roomDao.PlayersNumberChanged += new Action(() => { SendRooms(true); });
            roomDao.RoomDeleted += new Action(() => { SendRooms(true); });
        }

        protected override void OnOpen()
        {
            SendRooms();
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            JToken token = JObject.Parse(e.Data);
            string actionType = (string)token.SelectToken("type");

            if (actionType == RoomActions.CREATE_ROOM)
                CreateRoom(token.SelectToken("room"));
            else if (actionType == RoomActions.GET_ROOMS)
                SendRooms();

        }

        private void CreateRoom(JToken room)
        {
            Room receivedRoom = room.ToObject<Room>();
            Room createdRoom = roomDao.Create(receivedRoom);
            Send(JsonConvert.SerializeObject(new { type = RoomActions.GET_CREATED_ROOM, room = createdRoom }));
            SendRooms(true);
        }

        private void SendRooms(bool broadcast = false)
        {
            List<Room> roomList = roomDao.All();
            if (broadcast)
                Sessions.Broadcast(JsonConvert.SerializeObject(new { type = RoomActions.UPDATE_ROOMS, rooms = roomList }));
            else
                Send(JsonConvert.SerializeObject(new { type = RoomActions.UPDATE_ROOMS, rooms = roomList }));
        }

        protected override void OnClose(CloseEventArgs e)
        {
        }

        protected override void OnError(ErrorEventArgs e)
        {          
            base.OnError(e);
        }
    }
}