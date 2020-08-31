using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete
{
    class UserContext : DbContext
    {
        public List<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=WISGNURUN113\SQLEXPRESS; Database=User;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User { id = 1, name = "Azra", lastname = "Yerli" });
            modelBuilder.Entity<User>().HasData(new User { id = 2, name = "Hazal", lastname = "Yerli" });
            modelBuilder.Entity<User>().HasData(new User { id = 3, name = "Parla", lastname = "Yerli" });
        }
    }
}
   