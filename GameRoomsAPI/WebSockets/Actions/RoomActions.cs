using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameRoomsAPI.WebSockets.Actions
{
    public static class RoomActions
    {
        public const string GET_ROOMS = "getRooms";
        public const string CREATE_ROOM = "createRoom";
        public const string GET_CREATED_ROOM = "getCreatedRoom";
        public const string DELETE_ROOM = "deleteRoom";
        public const string UPDATE_ROOM = "updateRoom";
        public const string UPDATE_ROOMS = "updateRooms";
    }
}