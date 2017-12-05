using GameRoomsAPI.Helpers;
using GameRoomsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameRoomsAPI.DataBases.DAO
{
    public class UserDao
    {
        public List<User> All()
        {
            List<User> users = null;
            using (UserContext db = new UserContext())
            {
                users = db.Users.ToList();
            }
            return users;
        }

        public User Create(User receivedUser)
        {
            User user = new User(receivedUser);
            using (UserContext db = new UserContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
            return user;
        }

        public User Update(User receivedUser)
        {
            User returnedUser = null;
            using (UserContext db = new UserContext())
            {
                User updatedUser = db.Users.Where(c => c.Id == receivedUser.Id).FirstOrDefault();
                updatedUser.Update(receivedUser);
                returnedUser = updatedUser;
                db.SaveChanges();
            }
            return returnedUser;
        }

        public User Delete(int id)
        {
            User returnedUser = null;
            using (UserContext db = new UserContext())
            {
                User deletedUser = db.Users.Where(c => c.Id == id).FirstOrDefault();
                returnedUser = deletedUser;
                db.Users.Remove(deletedUser);
                db.SaveChanges();
            }
            return returnedUser;
        }

        public void DeleteAll()
        {
            using (UserContext db = new UserContext())
            {
                db.Users.RemoveRange(db.Users);
                db.SaveChanges();
            }
        }

        public User Get(int id)
        {
            User user = null;
            using (UserContext db = new UserContext())
            {
                user = db.Users.Where(c => c.Id == id).FirstOrDefault();
            }
            return user;
        }

        public void SetUserStatus(int id, bool online)
        {
            User user = null;
            using (UserContext db = new UserContext())
            {
                user = db.Users.Where(c => c.Id == id).FirstOrDefault();
                user.Online = online;
                db.SaveChanges();
            }
        }
    }
}