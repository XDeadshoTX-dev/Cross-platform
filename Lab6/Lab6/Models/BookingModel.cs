using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Lab6.Models
{
    public class BookingModel
    {
        public class Booking
        {
            public int booking_id { get; set; }
            public string booking_status_code { get; set; }
            public int customer_id { get; set; }
            public int reg_number { get; set; }
            public DateTime date_from { get; set; }
            public DateTime date_to { get; set; }
            public string confirmation_letter_sent_yn { get; set; }
            public string payment_received_yn { get; set; }
        }
        public class Vehicle
        {
            public string reg_number { get; set; }
            public string manufacturer_code { get; set; }
            public string model_code { get; set; }
            public string vehicle_category_code { get; set; }
            public int current_mileage { get; set; }
            public decimal daily_hire_rate { get; set; }
            public DateTime date_mot_due { get; set; }
        }
        public class VehicleCategory
        {
            public string vehicle_category_code { get; set; }
            public string vehicle_category_description { get; set; }
        }
        public class Model
        {
            public string model_code { get; set; }
            public decimal daily_hire_rate { get; set; }
            public string model_name { get; set; }
        }
        public class Manufacturer
        {
            public string manufacturer_code { get; set; }
            public string manufacturer_name { get; set; }
            public string manufacturer_details { get; set; }
        }
        public class BookingStatus
        {
            public string booking_status_code { get; set; }
            public string booking_status_description { get; set; }
        }
        public class Customer
        {
            public string customer_id { get; set; }
            public string customer_name { get; set; }
            public string customer_details { get; set; }
            public string gender { get; set; }
            public string email_address { get; set; }
            public string phone_number { get; set; }
            public string address_line_1 { get; set; }
            public string address_line_2 { get; set; }
            public string address_line_3 { get; set; }
            public string town { get; set; }
            public string county { get; set; }
            public string country { get; set; }
        }
    }
}
