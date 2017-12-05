using GameRoomsAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GameRoomsAPI.Helpers
{
    public class UserContext : DbContext
    {
        public UserContext()
            : base("DbConnection")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<UserContext>(null);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
    }
}