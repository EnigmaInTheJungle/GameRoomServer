using GameRoomsAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GameRoomsAPI.Helpers
{
    public class RoomContext : DbContext
    {
        public RoomContext()
            : base("DbConnection")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<RoomContext>(null);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Room> Rooms { get; set; }
    }
}