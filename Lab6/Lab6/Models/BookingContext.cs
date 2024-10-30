using System.Collections.Generic;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Lab6.Models
{
    public class BookingContext : DbContext
    {
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleCategory> VehicleCategories { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<BookingStatus> BookingStatuses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        private readonly string _connectionString = @"Server=(localdb)\mssqllocaldb;Database=Lab6;Trusted_Connection=True;ConnectRetryCount=0";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
