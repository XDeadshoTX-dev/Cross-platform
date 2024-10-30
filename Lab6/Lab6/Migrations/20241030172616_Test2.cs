using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lab6.Migrations
{
    /// <inheritdoc />
    public partial class Test2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_BookingStatuses_booking_status_code",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Customers_customer_id",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Vehicles_reg_number",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Manufacturers_manufacturer_code",
                table: "Vehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Models_model_code",
                table: "Vehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_VehicleCategories_vehicle_category_code",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_manufacturer_code",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_model_code",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_vehicle_category_code",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_booking_status_code",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_customer_id",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_reg_number",
                table: "Bookings");

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "booking_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "booking_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BookingStatuses",
                keyColumn: "booking_status_code",
                keyValue: "CMP");

            migrationBuilder.DeleteData(
                table: "BookingStatuses",
                keyColumn: "booking_status_code",
                keyValue: "PND");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "customer_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "customer_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "reg_number",
                keyValue: "ABC1234567");

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "reg_number",
                keyValue: "XYZ9876543");

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "manufacturer_code",
                keyValue: "FORD");

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "manufacturer_code",
                keyValue: "TOYO");

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "model_code",
                keyValue: "MOD123");

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "model_code",
                keyValue: "MOD456");

            migrationBuilder.DeleteData(
                table: "VehicleCategories",
                keyColumn: "vehicle_category_code",
                keyValue: "SEDAN");

            migrationBuilder.DeleteData(
                table: "VehicleCategories",
                keyColumn: "vehicle_category_code",
                keyValue: "SUV");

            migrationBuilder.AddColumn<string>(
                name: "VehicleCategoryvehicle_category_code",
                table: "Vehicles",
                type: "char(5)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "manufacturer_code1",
                table: "Vehicles",
                type: "char(10)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "model_code1",
                table: "Vehicles",
                type: "char(10)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BookingStatusbooking_status_code",
                table: "Bookings",
                type: "char(3)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Vehiclereg_number",
                table: "Bookings",
                type: "char(10)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "customer_id1",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_manufacturer_code1",
                table: "Vehicles",
                column: "manufacturer_code1");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_model_code1",
                table: "Vehicles",
                column: "model_code1");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VehicleCategoryvehicle_category_code",
                table: "Vehicles",
                column: "VehicleCategoryvehicle_category_code");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_BookingStatusbooking_status_code",
                table: "Bookings",
                column: "BookingStatusbooking_status_code");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_customer_id1",
                table: "Bookings",
                column: "customer_id1");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_Vehiclereg_number",
                table: "Bookings",
                column: "Vehiclereg_number");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_BookingStatuses_BookingStatusbooking_status_code",
                table: "Bookings",
                column: "BookingStatusbooking_status_code",
                principalTable: "BookingStatuses",
                principalColumn: "booking_status_code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Customers_customer_id1",
                table: "Bookings",
                column: "customer_id1",
                principalTable: "Customers",
                principalColumn: "customer_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Vehicles_Vehiclereg_number",
                table: "Bookings",
                column: "Vehiclereg_number",
                principalTable: "Vehicles",
                principalColumn: "reg_number",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Manufacturers_manufacturer_code1",
                table: "Vehicles",
                column: "manufacturer_code1",
                principalTable: "Manufacturers",
                principalColumn: "manufacturer_code");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Models_model_code1",
                table: "Vehicles",
                column: "model_code1",
                principalTable: "Models",
                principalColumn: "model_code");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_VehicleCategories_VehicleCategoryvehicle_category_code",
                table: "Vehicles",
                column: "VehicleCategoryvehicle_category_code",
                principalTable: "VehicleCategories",
                principalColumn: "vehicle_category_code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_BookingStatuses_BookingStatusbooking_status_code",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Customers_customer_id1",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Vehicles_Vehiclereg_number",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Manufacturers_manufacturer_code1",
                table: "Vehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Models_model_code1",
                table: "Vehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_VehicleCategories_VehicleCategoryvehicle_category_code",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_manufacturer_code1",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_model_code1",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_VehicleCategoryvehicle_category_code",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_BookingStatusbooking_status_code",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_customer_id1",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_Vehiclereg_number",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "VehicleCategoryvehicle_category_code",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "manufacturer_code1",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "model_code1",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "BookingStatusbooking_status_code",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Vehiclereg_number",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "customer_id1",
                table: "Bookings");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_BookingStatuses_booking_status_code",
                table: "Bookings",
                column: "booking_status_code",
                principalTable: "BookingStatuses",
                principalColumn: "booking_status_code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Customers_customer_id",
                table: "Bookings",
                column: "customer_id",
                principalTable: "Customers",
                principalColumn: "customer_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Vehicles_reg_number",
                table: "Bookings",
                column: "reg_number",
                principalTable: "Vehicles",
                principalColumn: "reg_number",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Manufacturers_manufacturer_code",
                table: "Vehicles",
                column: "manufacturer_code",
                principalTable: "Manufacturers",
                principalColumn: "manufacturer_code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Models_model_code",
                table: "Vehicles",
                column: "model_code",
                principalTable: "Models",
                principalColumn: "model_code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_VehicleCategories_vehicle_category_code",
                table: "Vehicles",
                column: "vehicle_category_code",
                principalTable: "VehicleCategories",
                principalColumn: "vehicle_category_code",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
