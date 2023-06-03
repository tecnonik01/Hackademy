using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hackademy.Domain.Entity;
using Microsoft.EntityFrameworkCore;
namespace Hackademy.Infrastructure
{
    public class HackademyContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TechLab> TechLabs { get; set; }
        public DbSet<Event> Events { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

      
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-QT02HI6;Initial Catalog=HackademyDB; Trusted_Connection=True;");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

    }
}
