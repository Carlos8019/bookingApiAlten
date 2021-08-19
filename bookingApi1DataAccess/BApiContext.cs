using Microsoft.EntityFrameworkCore;
using System;
using bookingApi1DataAccess.Models;
namespace bookingApi1DataAccess
{
    /*
    Cette class permet gesitoner l'access à la base de donnes vers Entity Framework avec DBContext
    */
    public class BApiContext:DbContext
    {
        //Constructor pour initialiser le context
        public BApiContext(DbContextOptions<BApiContext> options):base(options)
        {
            
        }   
        //Represente chaque table en tant qu'une class
        public DbSet<Clients> Clients {get;set;}
        public DbSet<Reservations> Reservations { get; set; }
        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<CalendarAvailability> CalendarAvailabilities { get; set; }
    }
}