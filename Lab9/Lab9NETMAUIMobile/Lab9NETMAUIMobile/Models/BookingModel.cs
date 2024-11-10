using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9NETMAUIMobile.Models
{
    public class GetBookingModel
    {
        public int CustomerId { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }

    public class GetBookingResponse
    {
        public int booking_id { get; set; }
        public string booking_status_code { get; set; }
        public int customer_id { get; set; }
        public string reg_number { get; set; }
        public DateTime date_from { get; set; }
        public DateTime date_to { get; set; }
        public string confirmation_letter_sent_yn { get; set; }
        public string payment_received_yn { get; set; }
    }
    public class SendBookingModel
    {
        public string booking_status_code { get; set; }
        public int customer_id { get; set; }
        public string reg_number { get; set; }
        public DateTime date_from { get; set; }
        public DateTime date_to { get; set; }
        public string confirmation_letter_sent_yn { get; set; }
        public string payment_received_yn { get; set; }
    }
}
