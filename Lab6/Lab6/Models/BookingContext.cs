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
        private readonly string _connectionString = "Server=(local);Database=Lab6;Trusted_Connection=True;TrustServerCertificate=True;";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Manufacturer>()
                .HasMany(m => m.Vehicles)
                .WithOne(v => v.Manufacturer)
                .HasForeignKey(v => v.manufacturer_code);

            modelBuilder.Entity<Model>()
                .HasMany(m => m.Vehicles)
                .WithOne(v => v.Model)
                .HasForeignKey(v => v.model_code);

            modelBuilder.Entity<VehicleCategory>()
                .HasMany(vc => vc.Vehicles)
                .WithOne(v => v.VehicleCategory)
                .HasForeignKey(v => v.vehicle_category_code);

            modelBuilder.Entity<Vehicle>()
                .HasMany(v => v.Bookings)
                .WithOne(b => b.Vehicle)
                .HasForeignKey(b => b.reg_number);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Bookings)
                .WithOne(b => b.Customer)
                .HasForeignKey(b => b.customer_id);

            modelBuilder.Entity<BookingStatus>()
                .HasMany(bs => bs.Bookings)
                .WithOne(b => b.BookingStatus)
                .HasForeignKey(b => b.booking_status_code);

            modelBuilder.Entity<Manufacturer>().HasData(
                new Manufacturer { manufacturer_code = "FORD", manufacturer_name = "Ford", manufacturer_details = "American car manufacturer" },
                new Manufacturer { manufacturer_code = "TOYO", manufacturer_name = "Toyota", manufacturer_details = "Japanese car manufacturer" }
            );

            modelBuilder.Entity<Model>().HasData(
                new Model { model_code = "MOD123", daily_hire_rate = 50.00M, model_name = "Model X" },
                new Model { model_code = "MOD456", daily_hire_rate = 60.00M, model_name = "Model Y" }
            );

            modelBuilder.Entity<VehicleCategory>().HasData(
                new VehicleCategory { vehicle_category_code = "SEDAN", vehicle_category_description = "Sedan" },
                new VehicleCategory { vehicle_category_code = "SUV", vehicle_category_description = "SUV" }
            );

            modelBuilder.Entity<Vehicle>().HasData(
                new Vehicle { reg_number = "ABC1234567", manufacturer_code = "FORD", model_code = "MOD123", vehicle_category_code = "SEDAN", current_mileage = 50000, daily_hire_rate = 55.00M, date_mot_due = new DateTime(2025, 5, 1) },
                new Vehicle { reg_number = "XYZ9876543", manufacturer_code = "TOYO", model_code = "MOD456", vehicle_category_code = "SUV", current_mileage = 30000, daily_hire_rate = 65.00M, date_mot_due = new DateTime(2024, 11, 15) }
            );

            modelBuilder.Entity<BookingStatus>().HasData(
                new BookingStatus { booking_status_code = "PND", booking_status_description = "Pending" },
                new BookingStatus { booking_status_code = "CMP", booking_status_description = "Completed" }
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer { customer_id = 1, customer_name = "John Doe", customer_details = "Frequent renter", gender = "M", email_address = "john@example.com", phone_number = "123456789", address_line_1 = "123 Main St", address_line_2 = "6567 City", address_line_3 = "Test City", town = "Sampletown", county = "Samplecounty", country = "Sampleland" },
                new Customer { customer_id = 2, customer_name = "Jane Smith", customer_details = "Occasional renter", gender = "F", email_address = "jane@example.com", phone_number = "987654321", address_line_1 = "456 Side St", address_line_2 = "15 city", address_line_3 = "Berlin test", town = "Sampletown", county = "Samplecounty", country = "Sampleland" }
            );

            modelBuilder.Entity<Booking>().HasData(
                new Booking { booking_id = 1, booking_status_code = "PND", customer_id = 1, reg_number = "ABC1234567", date_from = new DateTime(2024, 12, 1), date_to = new DateTime(2024, 12, 10), confirmation_letter_sent_yn = "Y", payment_received_yn = "N" },
                new Booking { booking_id = 2, booking_status_code = "CMP", customer_id = 2, reg_number = "XYZ9876543", date_from = new DateTime(2024, 11, 1), date_to = new DateTime(2024, 11, 7), confirmation_letter_sent_yn = "Y", payment_received_yn = "Y" }
            );
        }
    }
}
