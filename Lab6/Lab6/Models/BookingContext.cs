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

        private readonly int db_choice = 6;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            switch (db_choice)
            {
                case 1: // LocalDB connection string
                    optionsBuilder.UseSqlServer(@"Server=(LocalDB)\\MSSQLLocalDB;Database=Lab6;Trusted_Connection=True;TrustServerCertificate=True;");
                    break;
                case 2: // Microsoft SQL Server connection string
                    optionsBuilder.UseSqlServer(@"Server=(local);Database=Lab6;Trusted_Connection=True;TrustServerCertificate=True;");
                    break;
                case 3: // PostgreSQL connection string
                    string username = Environment.GetEnvironmentVariable("PostgreSQLUsername");
                    string password = Environment.GetEnvironmentVariable("PostgreSQLPassword");
                    optionsBuilder.UseNpgsql(@$"Host=localhost;Username={username};Password={password};Database=Lab6");
                    break;
                case 4: // SQLite connection string
                    optionsBuilder.UseSqlite("Data Source=Lab6.db");
                    break;
                case 5: // In-memory database
                    optionsBuilder.UseInMemoryDatabase("Lab6");
                    break;
                case 6:
                    string passwordAzure = Environment.GetEnvironmentVariable("MicrosoftAzureSQLServerPassword");
                    optionsBuilder.UseSqlServer(@$"Server=tcp:laboratory12.database.windows.net,1433;Initial Catalog=Lab6;Persist Security Info=False;User ID=laboratory12;Password={passwordAzure};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                    break;
            }
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
                new Manufacturer { manufacturer_code = "TOYO", manufacturer_name = "Toyota", manufacturer_details = "Japanese car manufacturer" },
                new Manufacturer { manufacturer_code = "HONDA", manufacturer_name = "Honda", manufacturer_details = "Japanese car manufacturer" },
                new Manufacturer { manufacturer_code = "NISSAN", manufacturer_name = "Nissan", manufacturer_details = "Japanese car manufacturer" },
                new Manufacturer { manufacturer_code = "BMW", manufacturer_name = "BMW", manufacturer_details = "German car manufacturer" },
                new Manufacturer { manufacturer_code = "MERCEDES", manufacturer_name = "Mercedes-Benz", manufacturer_details = "German car manufacturer" },
                new Manufacturer { manufacturer_code = "AUDI", manufacturer_name = "Audi", manufacturer_details = "German car manufacturer" },
                new Manufacturer { manufacturer_code = "VOLVO", manufacturer_name = "Volvo", manufacturer_details = "Swedish car manufacturer" },
                new Manufacturer { manufacturer_code = "HYUNDAI", manufacturer_name = "Hyundai", manufacturer_details = "South Korean car manufacturer" },
                new Manufacturer { manufacturer_code = "KIA", manufacturer_name = "Kia", manufacturer_details = "South Korean car manufacturer" }
            );

            modelBuilder.Entity<Model>().HasData(
              new Model { model_code = "MOD123", daily_hire_rate = 50.00M, model_name = "Model X" },
              new Model { model_code = "MOD456", daily_hire_rate = 60.00M, model_name = "Model Y" },
              new Model { model_code = "MOD789", daily_hire_rate = 70.00M, model_name = "Model Z" },
              new Model { model_code = "MOD012", daily_hire_rate = 45.00M, model_name = "Model A" },
              new Model { model_code = "MOD345", daily_hire_rate = 55.00M, model_name = "Model B" },
              new Model { model_code = "MOD678", daily_hire_rate = 65.00M, model_name = "Model C" },
              new Model { model_code = "MOD901", daily_hire_rate = 75.00M, model_name = "Model D" },
              new Model { model_code = "MOD234", daily_hire_rate = 40.00M, model_name = "Model E" },
              new Model { model_code = "MOD567", daily_hire_rate = 50.00M, model_name = "Model F" },
              new Model { model_code = "MOD890", daily_hire_rate = 60.00M, model_name = "Model G" }
            );

            modelBuilder.Entity<VehicleCategory>().HasData(
                new VehicleCategory { vehicle_category_code = "SEDAN", vehicle_category_description = "Sedan" },
                new VehicleCategory { vehicle_category_code = "SUV", vehicle_category_description = "SUV" },
                new VehicleCategory { vehicle_category_code = "TRUCK", vehicle_category_description = "Truck" },
                new VehicleCategory { vehicle_category_code = "VAN", vehicle_category_description = "Van" },
                new VehicleCategory { vehicle_category_code = "COUPE", vehicle_category_description = "Coupe" },
                new VehicleCategory { vehicle_category_code = "CTIBL", vehicle_category_description = "Convertibl" },
                new VehicleCategory { vehicle_category_code = "HBACK", vehicle_category_description = "Hatchback" },
                new VehicleCategory { vehicle_category_code = "WAGON", vehicle_category_description = "Wagon" },
                new VehicleCategory { vehicle_category_code = "MVAN", vehicle_category_description = "Minivan" },
                new VehicleCategory { vehicle_category_code = "SPRTS", vehicle_category_description = "Sports Car" }
            );

            modelBuilder.Entity<Vehicle>().HasData(
               new Vehicle { reg_number = "ABC1234567", manufacturer_code = "FORD", model_code = "MOD123", vehicle_category_code = "SEDAN", current_mileage = 50000, daily_hire_rate = 55.00M, date_mot_due = new DateTime(2025, 5, 1) },
               new Vehicle { reg_number = "XYZ9876543", manufacturer_code = "TOYO", model_code = "MOD456", vehicle_category_code = "SUV", current_mileage = 30000, daily_hire_rate = 65.00M, date_mot_due = new DateTime(2024, 11, 15) },
               new Vehicle { reg_number = "JKL1237890", manufacturer_code = "HONDA", model_code = "MOD678", vehicle_category_code = "COUPE", current_mileage = 20000, daily_hire_rate = 70.00M, date_mot_due = new DateTime(2025, 2, 20) },
               new Vehicle { reg_number = "MNO4561234", manufacturer_code = "BMW", model_code = "MOD901", vehicle_category_code = "SPRTS", current_mileage = 10000, daily_hire_rate = 120.00M, date_mot_due = new DateTime(2024, 12, 15) },
               new Vehicle { reg_number = "DEF4567890", manufacturer_code = "MERCEDES", model_code = "MOD234", vehicle_category_code = "CTIBL", current_mileage = 15000, daily_hire_rate = 80.00M, date_mot_due = new DateTime(2025, 1, 10) },
               new Vehicle { reg_number = "GHI9876543", manufacturer_code = "AUDI", model_code = "MOD567", vehicle_category_code = "WAGON", current_mileage = 25000, daily_hire_rate = 85.00M, date_mot_due = new DateTime(2025, 6, 1) },
               new Vehicle { reg_number = "JKL1234567", manufacturer_code = "HYUNDAI", model_code = "MOD890", vehicle_category_code = "HBACK", current_mileage = 18000, daily_hire_rate = 50.00M, date_mot_due = new DateTime(2025, 3, 15) },
               new Vehicle { reg_number = "OPQ9876543", manufacturer_code = "KIA", model_code = "MOD012", vehicle_category_code = "VAN", current_mileage = 22000, daily_hire_rate = 60.00M, date_mot_due = new DateTime(2024, 12, 30) },
               new Vehicle { reg_number = "RST1234567", manufacturer_code = "NISSAN", model_code = "MOD345", vehicle_category_code = "TRUCK", current_mileage = 8000, daily_hire_rate = 90.00M, date_mot_due = new DateTime(2025, 4, 22) },
               new Vehicle { reg_number = "UVW9876543", manufacturer_code = "VOLVO", model_code = "MOD789", vehicle_category_code = "MVAN", current_mileage = 12000, daily_hire_rate = 75.00M, date_mot_due = new DateTime(2025, 7, 8) }
           );

            modelBuilder.Entity<BookingStatus>().HasData(
                new BookingStatus { booking_status_code = "PND", booking_status_description = "Pending" },
                new BookingStatus { booking_status_code = "CMP", booking_status_description = "Completed" },
                new BookingStatus { booking_status_code = "CNL", booking_status_description = "Cancelled" },
                new BookingStatus { booking_status_code = "RNT", booking_status_description = "Rented" }, // Active rental
                new BookingStatus { booking_status_code = "OVD", booking_status_description = "Overdue" }, // Past due date
                new BookingStatus { booking_status_code = "PPD", booking_status_description = "Prepaid" }, // Paid in advance
                new BookingStatus { booking_status_code = "CNF", booking_status_description = "Confirmed" },
                new BookingStatus { booking_status_code = "CHI", booking_status_description = "CheckedIn" },
                new BookingStatus { booking_status_code = "CHK", booking_status_description = "CheckedOut" },
                new BookingStatus { booking_status_code = "DIS", booking_status_description = "Disputed" } // For payment or other disputes
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer { customer_id = 1, customer_name = "John Doe", customer_details = "Frequent renter", gender = "M", email_address = "john@example.com", phone_number = "123456789", address_line_1 = "123 Main St", address_line_2 = "Apt 101", address_line_3 = "", town = "Sampletown", county = "Samplecounty", country = "Sampleland" },
                new Customer { customer_id = 2, customer_name = "Jane Smith", customer_details = "Occasional renter", gender = "F", email_address = "jane@example.com", phone_number = "987654321", address_line_1 = "456 Side St", address_line_2 = "Suite 202", address_line_3 = "", town = "Sampletown", county = "Samplecounty", country = "Sampleland" },
                new Customer { customer_id = 3, customer_name = "Alice Brown", customer_details = "Business renter", gender = "F", email_address = "alice@example.com", phone_number = "555555555", address_line_1 = "789 Park Ave", address_line_2 = "Floor 3", address_line_3 = "", town = "Big City", county = "Samplecounty", country = "Sampleland" },
                new Customer { customer_id = 4, customer_name = "Bob Green", customer_details = "Tourist", gender = "M", email_address = "bob@example.com", phone_number = "111111111", address_line_1 = "321 Hill Rd", address_line_2 = "", address_line_3 = "", town = "Hilltown", county = "Samplecounty", country = "Sampleland" },
                new Customer { customer_id = 5, customer_name = "Charlie Johnson", customer_details = "Long-term renter", gender = "M", email_address = "charlie@example.com", phone_number = "222222222", address_line_1 = "654 Elm St", address_line_2 = "Building A", address_line_3 = "", town = "Elmwood", county = "Samplecounty", country = "Sampleland" },
                new Customer { customer_id = 6, customer_name = "Diana Prince", customer_details = "VIP customer", gender = "F", email_address = "diana@example.com", phone_number = "333333333", address_line_1 = "321 Oak St", address_line_2 = "Apartment 5", address_line_3 = "", town = "Gotham", county = "Samplecounty", country = "Sampleland" },
                new Customer { customer_id = 7, customer_name = "Ethan Hunt", customer_details = "Adventurer", gender = "M", email_address = "ethan@example.com", phone_number = "444444444", address_line_1 = "789 Pine St", address_line_2 = "Unit 10", address_line_3 = "", town = "Pineville", county = "Samplecounty", country = "Sampleland" },
                new Customer { customer_id = 8, customer_name = "Fiona Gallagher", customer_details = "Family renter", gender = "F", email_address = "fiona@example.com", phone_number = "555555555", address_line_1 = "222 Maple St", address_line_2 = "", address_line_3 = "", town = "Mapleton", county = "Samplecounty", country = "Sampleland" },
                new Customer { customer_id = 9, customer_name = "George Clark", customer_details = "Weekend renter", gender = "M", email_address = "george@example.com", phone_number = "666666666", address_line_1 = "333 Birch St", address_line_2 = "Floor 2", address_line_3 = "", town = "Birchtown", county = "Samplecounty", country = "Sampleland" },
                new Customer { customer_id = 10, customer_name = "Hannah Adams", customer_details = "Seasonal renter", gender = "F", email_address = "hannah@example.com", phone_number = "777777777", address_line_1 = "555 Cedar St", address_line_2 = "", address_line_3 = "", town = "Cedarville", county = "Samplecounty", country = "Sampleland" }
            );

            //modelBuilder.Entity<Booking>().HasData(
            //    new Booking { booking_id = 1, booking_status_code = "PND", customer_id = 1, reg_number = "ABC1234567", date_from = new DateTime(2024, 12, 1), date_to = new DateTime(2024, 12, 10), confirmation_letter_sent_yn = "Y", payment_received_yn = "N" },
            //    new Booking { booking_id = 2, booking_status_code = "PND", customer_id = 2, reg_number = "XYZ9876543", date_from = new DateTime(2024, 11, 1), date_to = new DateTime(2024, 11, 7), confirmation_letter_sent_yn = "Y", payment_received_yn = "Y" },
            //    new Booking { booking_id = 3, booking_status_code = "CNL", customer_id = 3, reg_number = "JKL1237890", date_from = new DateTime(2024, 11, 10), date_to = new DateTime(2024, 11, 17), confirmation_letter_sent_yn = "Y", payment_received_yn = "Y" },
            //    new Booking { booking_id = 4, booking_status_code = "RNT", customer_id = 3, reg_number = "MNO4561234", date_from = new DateTime(2024, 10, 1), date_to = new DateTime(2024, 10, 5), confirmation_letter_sent_yn = "N", payment_received_yn = "N" },
            //    new Booking { booking_id = 5, booking_status_code = "OVD", customer_id = 5, reg_number = "DEF4567890", date_from = new DateTime(2024, 12, 15), date_to = new DateTime(2024, 12, 20), confirmation_letter_sent_yn = "Y", payment_received_yn = "N" },
            //    new Booking { booking_id = 6, booking_status_code = "PPD", customer_id = 6, reg_number = "GHI9876543", date_from = new DateTime(2024, 11, 20), date_to = new DateTime(2024, 11, 25), confirmation_letter_sent_yn = "Y", payment_received_yn = "Y" },
            //    new Booking { booking_id = 7, booking_status_code = "CNF", customer_id = 7, reg_number = "JKL1234567", date_from = new DateTime(2024, 11, 15), date_to = new DateTime(2024, 11, 22), confirmation_letter_sent_yn = "Y", payment_received_yn = "Y" },
            //    new Booking { booking_id = 8, booking_status_code = "CHI", customer_id = 8, reg_number = "OPQ9876543", date_from = new DateTime(2024, 10, 10), date_to = new DateTime(2024, 10, 12), confirmation_letter_sent_yn = "N", payment_received_yn = "N" },
            //    new Booking { booking_id = 9, booking_status_code = "CHK", customer_id = 9, reg_number = "RST1234567", date_from = new DateTime(2024, 12, 5), date_to = new DateTime(2024, 12, 12), confirmation_letter_sent_yn = "Y", payment_received_yn = "N" },
            //    new Booking { booking_id = 10, booking_status_code = "DIS", customer_id = 10, reg_number = "UVW9876543", date_from = new DateTime(2024, 11, 25), date_to = new DateTime(2024, 12, 2), confirmation_letter_sent_yn = "Y", payment_received_yn = "Y" }
            //);
            modelBuilder.Entity<Booking>().HasData(GenerateBookings(10000));
        }
        private Booking[] GenerateBookings(int count)
        {
            var bookings = new List<Booking>();
            var random = new Random();

            for (int i = 1; i <= count; i++)
            {
                var booking = new Booking
                {
                    booking_id = i,
                    booking_status_code = GetRandomBookingStatusCode(random),
                    customer_id = random.Next(1, 11),
                    reg_number = GetRandomVehicleRegNumber(random),
                    date_from = DateTime.Now.AddDays(random.Next(0, 30)),
                    date_to = DateTime.Now.AddDays(random.Next(31, 60)),
                    confirmation_letter_sent_yn = random.Next(0, 2) == 0 ? "Y" : "N",
                    payment_received_yn = random.Next(0, 2) == 0 ? "Y" : "N"
                };

                bookings.Add(booking);
            }

            return bookings.ToArray();
        }

        private string GetRandomBookingStatusCode(Random random)
        {
            string[] statusCodes = { "PND", "CMP", "CNL", "RNT", "OVD", "PPD", "CNF", "CHI", "CHK", "DIS" };
            return statusCodes[random.Next(statusCodes.Length)];
        }

        private string GetRandomVehicleRegNumber(Random random)
        {
            string[] vehicleRegNumbers = { "ABC1234567", "XYZ9876543", "JKL1237890", "MNO4561234", "DEF4567890",
                                            "GHI9876543", "JKL1234567", "OPQ9876543", "RST1234567", "UVW9876543" };
            return vehicleRegNumbers[random.Next(vehicleRegNumbers.Length)];
        }
    }
}
