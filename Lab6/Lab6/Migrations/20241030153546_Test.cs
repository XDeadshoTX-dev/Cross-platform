using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lab6.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
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
                    { "CMP", "Completed" },
                    { "PND", "Pending" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "customer_id", "address_line_1", "address_line_2", "address_line_3", "country", "county", "customer_details", "customer_name", "email_address", "gender", "phone_number", "town" },
                values: new object[,]
                {
                    { 1, "123 Main St", "6567 City", "Test City", "Sampleland", "Samplecounty", "Frequent renter", "John Doe", "john@example.com", "M", "123456789", "Sampletown" },
                    { 2, "456 Side St", "15 city", "Berlin test", "Sampleland", "Samplecounty", "Occasional renter", "Jane Smith", "jane@example.com", "F", "987654321", "Sampletown" }
                });

            migrationBuilder.InsertData(
                table: "Manufacturers",
                columns: new[] { "manufacturer_code", "manufacturer_details", "manufacturer_name" },
                values: new object[,]
                {
                    { "FORD", "American car manufacturer", "Ford" },
                    { "TOYO", "Japanese car manufacturer", "Toyota" }
                });

            migrationBuilder.InsertData(
                table: "Models",
                columns: new[] { "model_code", "daily_hire_rate", "model_name" },
                values: new object[,]
                {
                    { "MOD123", 50.00m, "Model X" },
                    { "MOD456", 60.00m, "Model Y" }
                });

            migrationBuilder.InsertData(
                table: "VehicleCategories",
                columns: new[] { "vehicle_category_code", "vehicle_category_description" },
                values: new object[,]
                {
                    { "SEDAN", "Sedan" },
                    { "SUV", "SUV" }
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "reg_number", "current_mileage", "daily_hire_rate", "date_mot_due", "manufacturer_code", "model_code", "vehicle_category_code" },
                values: new object[,]
                {
                    { "ABC1234567", 50000, 55.00m, new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "FORD", "MOD123", "SEDAN" },
                    { "XYZ9876543", 30000, 65.00m, new DateTime(2024, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "TOYO", "MOD456", "SUV" }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "booking_id", "booking_status_code", "confirmation_letter_sent_yn", "customer_id", "date_from", "date_to", "payment_received_yn", "reg_number" },
                values: new object[,]
                {
                    { 1, "PND", "Y", 1, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "N", "ABC1234567" },
                    { 2, "CMP", "Y", 2, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Y", "XYZ9876543" }
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
