using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lab6.Migrations
{
    /// <inheritdoc />
    public partial class DataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookingStatuses",
                columns: table => new
                {
                    booking_status_code = table.Column<string>(type: "char(3)", nullable: false),
                    booking_status_description = table.Column<string>(type: "char(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingStatuses", x => x.booking_status_code);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    customer_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customer_name = table.Column<string>(type: "varchar(255)", nullable: false),
                    customer_details = table.Column<string>(type: "varchar(2000)", nullable: false),
                    gender = table.Column<string>(type: "char(1)", nullable: false),
                    email_address = table.Column<string>(type: "varchar(255)", nullable: false),
                    phone_number = table.Column<string>(type: "varchar(50)", nullable: false),
                    address_line_1 = table.Column<string>(type: "varchar(255)", nullable: false),
                    address_line_2 = table.Column<string>(type: "varchar(255)", nullable: false),
                    address_line_3 = table.Column<string>(type: "varchar(255)", nullable: false),
                    town = table.Column<string>(type: "varchar(50)", nullable: false),
                    county = table.Column<string>(type: "varchar(30)", nullable: false),
                    country = table.Column<string>(type: "varchar(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.customer_id);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    manufacturer_code = table.Column<string>(type: "char(10)", nullable: false),
                    manufacturer_name = table.Column<string>(type: "varchar(50)", nullable: false),
                    manufacturer_details = table.Column<string>(type: "varchar(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.manufacturer_code);
                });

            migrationBuilder.CreateTable(
                name: "Models",
                columns: table => new
                {
                    model_code = table.Column<string>(type: "char(10)", nullable: false),
                    daily_hire_rate = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    model_name = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.model_code);
                });

            migrationBuilder.CreateTable(
                name: "VehicleCategories",
                columns: table => new
                {
                    vehicle_category_code = table.Column<string>(type: "char(5)", nullable: false),
                    vehicle_category_description = table.Column<string>(type: "char(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleCategories", x => x.vehicle_category_code);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    reg_number = table.Column<string>(type: "char(10)", nullable: false),
                    manufacturer_code = table.Column<string>(type: "char(10)", nullable: false),
                    model_code = table.Column<string>(type: "char(10)", nullable: false),
                    vehicle_category_code = table.Column<string>(type: "char(5)", nullable: false),
                    current_mileage = table.Column<int>(type: "int", nullable: false),
                    daily_hire_rate = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    date_mot_due = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.reg_number);
                    table.ForeignKey(
                        name: "FK_Vehicles_Manufacturers_manufacturer_code",
                        column: x => x.manufacturer_code,
                        principalTable: "Manufacturers",
                        principalColumn: "manufacturer_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vehicles_Models_model_code",
                        column: x => x.model_code,
                        principalTable: "Models",
                        principalColumn: "model_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vehicles_VehicleCategories_vehicle_category_code",
                        column: x => x.vehicle_category_code,
                        principalTable: "VehicleCategories",
                        principalColumn: "vehicle_category_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    booking_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    booking_status_code = table.Column<string>(type: "char(3)", nullable: false),
                    customer_id = table.Column<int>(type: "int", nullable: false),
                    reg_number = table.Column<string>(type: "char(10)", nullable: false),
                    date_from = table.Column<DateTime>(type: "date", nullable: false),
                    date_to = table.Column<DateTime>(type: "date", nullable: false),
                    confirmation_letter_sent_yn = table.Column<string>(type: "char(1)", nullable: false),
                    payment_received_yn = table.Column<string>(type: "char(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.booking_id);
                    table.ForeignKey(
                        name: "FK_Bookings_BookingStatuses_booking_status_code",
                        column: x => x.booking_status_code,
                        principalTable: "BookingStatuses",
                        principalColumn: "booking_status_code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "Customers",
                        principalColumn: "customer_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Vehicles_reg_number",
                        column: x => x.reg_number,
                        principalTable: "Vehicles",
                        principalColumn: "reg_number",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BookingStatuses",
                columns: new[] { "booking_status_code", "booking_status_description" },
                values: new object[,]
                {
                    { "CHI", "CheckedIn" },
                    { "CHK", "CheckedOut" },
                    { "CMP", "Completed" },
                    { "CNF", "Confirmed" },
                    { "CNL", "Cancelled" },
                    { "DIS", "Disputed" },
                    { "OVD", "Overdue" },
                    { "PND", "Pending" },
                    { "PPD", "Prepaid" },
                    { "RNT", "Rented" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "customer_id", "address_line_1", "address_line_2", "address_line_3", "country", "county", "customer_details", "customer_name", "email_address", "gender", "phone_number", "town" },
                values: new object[,]
                {
                    { 1, "123 Main St", "Apt 101", "", "Sampleland", "Samplecounty", "Frequent renter", "John Doe", "john@example.com", "M", "123456789", "Sampletown" },
                    { 2, "456 Side St", "Suite 202", "", "Sampleland", "Samplecounty", "Occasional renter", "Jane Smith", "jane@example.com", "F", "987654321", "Sampletown" },
                    { 3, "789 Park Ave", "Floor 3", "", "Sampleland", "Samplecounty", "Business renter", "Alice Brown", "alice@example.com", "F", "555555555", "Big City" },
                    { 4, "321 Hill Rd", "", "", "Sampleland", "Samplecounty", "Tourist", "Bob Green", "bob@example.com", "M", "111111111", "Hilltown" },
                    { 5, "654 Elm St", "Building A", "", "Sampleland", "Samplecounty", "Long-term renter", "Charlie Johnson", "charlie@example.com", "M", "222222222", "Elmwood" },
                    { 6, "321 Oak St", "Apartment 5", "", "Sampleland", "Samplecounty", "VIP customer", "Diana Prince", "diana@example.com", "F", "333333333", "Gotham" },
                    { 7, "789 Pine St", "Unit 10", "", "Sampleland", "Samplecounty", "Adventurer", "Ethan Hunt", "ethan@example.com", "M", "444444444", "Pineville" },
                    { 8, "222 Maple St", "", "", "Sampleland", "Samplecounty", "Family renter", "Fiona Gallagher", "fiona@example.com", "F", "555555555", "Mapleton" },
                    { 9, "333 Birch St", "Floor 2", "", "Sampleland", "Samplecounty", "Weekend renter", "George Clark", "george@example.com", "M", "666666666", "Birchtown" },
                    { 10, "555 Cedar St", "", "", "Sampleland", "Samplecounty", "Seasonal renter", "Hannah Adams", "hannah@example.com", "F", "777777777", "Cedarville" }
                });

            migrationBuilder.InsertData(
                table: "Manufacturers",
                columns: new[] { "manufacturer_code", "manufacturer_details", "manufacturer_name" },
                values: new object[,]
                {
                    { "AUDI", "German car manufacturer", "Audi" },
                    { "BMW", "German car manufacturer", "BMW" },
                    { "FORD", "American car manufacturer", "Ford" },
                    { "HONDA", "Japanese car manufacturer", "Honda" },
                    { "HYUNDAI", "South Korean car manufacturer", "Hyundai" },
                    { "KIA", "South Korean car manufacturer", "Kia" },
                    { "MERCEDES", "German car manufacturer", "Mercedes-Benz" },
                    { "NISSAN", "Japanese car manufacturer", "Nissan" },
                    { "TOYO", "Japanese car manufacturer", "Toyota" },
                    { "VOLVO", "Swedish car manufacturer", "Volvo" }
                });

            migrationBuilder.InsertData(
                table: "Models",
                columns: new[] { "model_code", "daily_hire_rate", "model_name" },
                values: new object[,]
                {
                    { "MOD012", 45.00m, "Model A" },
                    { "MOD123", 50.00m, "Model X" },
                    { "MOD234", 40.00m, "Model E" },
                    { "MOD345", 55.00m, "Model B" },
                    { "MOD456", 60.00m, "Model Y" },
                    { "MOD567", 50.00m, "Model F" },
                    { "MOD678", 65.00m, "Model C" },
                    { "MOD789", 70.00m, "Model Z" },
                    { "MOD890", 60.00m, "Model G" },
                    { "MOD901", 75.00m, "Model D" }
                });

            migrationBuilder.InsertData(
                table: "VehicleCategories",
                columns: new[] { "vehicle_category_code", "vehicle_category_description" },
                values: new object[,]
                {
                    { "COUPE", "Coupe" },
                    { "CTIBL", "Convertibl" },
                    { "HBACK", "Hatchback" },
                    { "MVAN", "Minivan" },
                    { "SEDAN", "Sedan" },
                    { "SPRTS", "Sports Car" },
                    { "SUV", "SUV" },
                    { "TRUCK", "Truck" },
                    { "VAN", "Van" },
                    { "WAGON", "Wagon" }
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "reg_number", "current_mileage", "daily_hire_rate", "date_mot_due", "manufacturer_code", "model_code", "vehicle_category_code" },
                values: new object[,]
                {
                    { "ABC1234567", 50000, 55.00m, new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "FORD", "MOD123", "SEDAN" },
                    { "DEF4567890", 15000, 80.00m, new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "MERCEDES", "MOD234", "CTIBL" },
                    { "GHI9876543", 25000, 85.00m, new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AUDI", "MOD567", "WAGON" },
                    { "JKL1234567", 18000, 50.00m, new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "HYUNDAI", "MOD890", "HBACK" },
                    { "JKL1237890", 20000, 70.00m, new DateTime(2025, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "HONDA", "MOD678", "COUPE" },
                    { "MNO4561234", 10000, 120.00m, new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "BMW", "MOD901", "SPRTS" },
                    { "OPQ9876543", 22000, 60.00m, new DateTime(2024, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "KIA", "MOD012", "VAN" },
                    { "RST1234567", 8000, 90.00m, new DateTime(2025, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "NISSAN", "MOD345", "TRUCK" },
                    { "UVW9876543", 12000, 75.00m, new DateTime(2025, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "VOLVO", "MOD789", "MVAN" },
                    { "XYZ9876543", 30000, 65.00m, new DateTime(2024, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "TOYO", "MOD456", "SUV" }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "booking_id", "booking_status_code", "confirmation_letter_sent_yn", "customer_id", "date_from", "date_to", "payment_received_yn", "reg_number" },
                values: new object[,]
                {
                    { 1, "PND", "Y", 1, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "N", "ABC1234567" },
                    { 2, "CMP", "Y", 2, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Y", "XYZ9876543" },
                    { 3, "CNL", "Y", 3, new DateTime(2024, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Y", "JKL1237890" },
                    { 4, "RNT", "N", 4, new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "N", "MNO4561234" },
                    { 5, "OVD", "Y", 5, new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "N", "DEF4567890" },
                    { 6, "PPD", "Y", 6, new DateTime(2024, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Y", "GHI9876543" },
                    { 7, "CNF", "Y", 7, new DateTime(2024, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Y", "JKL1234567" },
                    { 8, "CHI", "N", 8, new DateTime(2024, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "N", "OPQ9876543" },
                    { 9, "CHK", "Y", 9, new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "N", "RST1234567" },
                    { 10, "DIS", "Y", 10, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Y", "UVW9876543" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_booking_status_code",
                table: "Bookings",
                column: "booking_status_code");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_customer_id",
                table: "Bookings",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_reg_number",
                table: "Bookings",
                column: "reg_number");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_manufacturer_code",
                table: "Vehicles",
                column: "manufacturer_code");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_model_code",
                table: "Vehicles",
                column: "model_code");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_vehicle_category_code",
                table: "Vehicles",
                column: "vehicle_category_code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "BookingStatuses");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Manufacturers");

            migrationBuilder.DropTable(
                name: "Models");

            migrationBuilder.DropTable(
                name: "VehicleCategories");
        }
    }
}
