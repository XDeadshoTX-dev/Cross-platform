using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lab6.Models
{
    public class Booking
    {
        [Key]
        public int booking_id { get; set; }

        [Column(TypeName = "char(3)")]
        public string booking_status_code { get; set; }
        public virtual BookingStatus BookingStatus { get; set; }

        public int customer_id { get; set; }
        public virtual Customer Customer { get; set; }

        [Column(TypeName = "char(10)")]
        public string reg_number { get; set; }
        public virtual Vehicle Vehicle { get; set; }

        [Column(TypeName = "date")]
        public DateTime date_from { get; set; }

        [Column(TypeName = "date")]
        public DateTime date_to { get; set; }

        [Column(TypeName = "char(1)")]
        public string confirmation_letter_sent_yn { get; set; }

        [Column(TypeName = "char(1)")]
        public string payment_received_yn { get; set; }
    }
    public class Vehicle
    {
        [Key]
        [Column(TypeName = "char(10)")]
        public string reg_number { get; set; }

        [Column(TypeName = "char(10)")]
        public string manufacturer_code { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }


        [Column(TypeName = "char(10)")]
        public string model_code { get; set; }
        public virtual Model Model { get; set; }

        [Column(TypeName = "char(5)")]
        public string vehicle_category_code { get; set; }
        public virtual VehicleCategory VehicleCategory { get; set; }

        public int current_mileage { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal daily_hire_rate { get; set; }

        [Column(TypeName = "date")]
        public DateTime date_mot_due { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
    public class VehicleCategory
    {
        [Key]
        [Column(TypeName = "char(5)")]
        public string vehicle_category_code { get; set; }

        [Column(TypeName = "char(10)")]
        public string vehicle_category_description { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
    public class Model
    {
        [Key]
        [Column(TypeName = "char(10)")]
        public string model_code { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal daily_hire_rate { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string model_name { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
    public class Manufacturer
    {
        [Key]
        [Column(TypeName = "char(10)")]
        public string manufacturer_code { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string manufacturer_name { get; set; }

        [Column(TypeName = "varchar(2000)")]
        public string manufacturer_details { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }

    }
    public class BookingStatus
    {
        [Key]
        [Column(TypeName = "char(3)")]
        public string booking_status_code { get; set; }

        [Column(TypeName = "char(10)")]
        public string booking_status_description { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }

    }
    public class Customer
    {
        [Key]
        public int customer_id { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string customer_name { get; set; }

        [Column(TypeName = "varchar(2000)")]
        public string customer_details { get; set; }

        [Column(TypeName = "char(1)")]
        public string gender { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string email_address { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string phone_number { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string address_line_1 { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string address_line_2 { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string address_line_3 { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string town { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string county { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string country { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
