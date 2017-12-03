using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameRoomsAPI.Models
{
    public class User
    {
        static int count = 0;

        public int Id { get; set; }
        public string Name { get; set; }

        public User()
        {

        }

        public User(string name)
        {
            Name = name;
            Id = ++count;
        }
    }
}