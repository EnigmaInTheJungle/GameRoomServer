using GameRoomsAPI.Helpers;
using GameRoomsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameRoomsAPI.DAO
{
    public class RoomDao
    {
        public event Action RoomDeleted;
        public event Action PlayersNumberChanged;

        public List<Room> All()
        {
            List<Room> rooms = null;
            using (RoomContext db = new RoomContext())
            {
                rooms = db.Rooms.ToList();
            }
            return rooms;
        }

        public Room Create(Room receivedRoom)
        {
            Room room = new Room(receivedRoom);
            using (RoomContext db = new RoomContext())
            {
                db.Rooms.Add(room);
                db.SaveChanges();
                room.StartServer(room.Id);
                db.SaveChanges();
            }
            return room;
        }

        public Room Update(Room receivedRoom)
        {
            Room returnedRoom = null;
            using (RoomContext db = new RoomContext())
            {
                Room updatedRoom = db.Rooms.Where(c => c.Id == receivedRoom.Id).FirstOrDefault();
                updatedRoom.Update(receivedRoom);
                returnedRoom = updatedRoom;
                db.SaveChanges();
            }
            return returnedRoom;
        }

        public Room Delete(int id)
        {
            Room returnedRoom = null;
            using (RoomContext db = new RoomContext())
            {
                Room deletedRoom = db.Rooms.Where(c => c.Id == id).FirstOrDefault();
                returnedRoom = deletedRoom;
                db.Rooms.Remove(deletedRoom);
                db.SaveChanges();          
            }
            //RoomDeleted();
            return returnedRoom;
        }

        public void DeleteAll()
        {
            using (RoomContext db = new RoomContext())
            {
                db.Rooms.RemoveRange(db.Rooms);
                db.SaveChanges();
            }
        }

        public Room Get(int id)
        {
            Room room = null;
            using (RoomContext db = new RoomContext())
            {
                room = db.Rooms.Where(c => c.Id == id).FirstOrDefault();
            }
            return room;
        }

        public Room AddUser(int id, User user)
        {
            Room room = null;
            using (RoomContext db = new RoomContext())
            {
                room = db.Rooms.Where(c => c.Id == id).FirstOrDefault();
                room.AddUser(user);
                db.SaveChanges();                
            }
            //PlayersNumberChanged();
            return room;
        }

        public Room RemoveUser(int id, User user)
        {
            Room room = null;
            using (RoomContext db = new RoomContext())
            {
                room = db.Rooms.Where(c => c.Id == id).FirstOrDefault();
                room.DeleteUser(user);
                db.SaveChanges();               
            }
            //PlayersNumberChanged();
            return room;
        }
    }
}