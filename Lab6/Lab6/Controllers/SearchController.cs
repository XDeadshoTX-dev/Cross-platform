using Lab6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics.Eventing.Reader;
using System.Text;

namespace Lab6.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<string> GetBookingInformation([FromBody] BookingRequest request)
        {
            using (var context = new BookingContext())
            {
                var date = new DateTime(request.year, request.month, request.day);
                var _list = await context.Bookings.Include(b => b.Vehicle).Include(b => b.Customer).Where(b => b.customer_id == request.customerId && b.date_from.Date == date.Date).ToListAsync();
                var response = JsonConvert.SerializeObject(_list.Select(b => new
                {
                    booking_id = b.booking_id,
                    booking_status_code = b.booking_status_code,
                    customer_id = b.customer_id,
                    reg_number = b.reg_number,
                    date_from = b.date_from,
                    date_to = b.date_to,
                    confirmation_letter_sent_yn = b.confirmation_letter_sent_yn,
                    payment_received_yn = b.payment_received_yn
                }));

                return response;
            }
        }
        public class BookingRequest
        {
            public int customerId { get; set; }
            public int day { get; set; }
            public int month { get; set; }
            public int year { get; set; }
        }
        [HttpPost]
        public async Task<string> VehicleCategoryInformation([FromBody] VehicleCategoryRequest request)
        {
            using(var context = new BookingContext())
            {
                var _list = await context.VehicleCategories.Where(v => v.vehicle_category_code == request.vehicle_category_code).ToListAsync();
                var response = JsonConvert.SerializeObject(_list.Select(b => new
                {
                    vehicle_category_description = b.vehicle_category_description
                }));
                return response;
            }
        }
        public class VehicleCategoryRequest
        {
            public string vehicle_category_code { get; set; }
        }
        [HttpPost]
        public async Task<string> ModelInformation([FromBody] ModelInformationRequest request)
        {
            using (var context = new BookingContext())
            {
                var _list = await context.Models.Where(m => m.model_code == request.model_code).ToListAsync();
                var response = JsonConvert.SerializeObject(_list.Select(b => new
                {
                    model_name = b.model_name,
                    daily_hire_rate = b.daily_hire_rate
                }));
                return response;
            }
        }
        public class ModelInformationRequest
        {
            public string model_code { get; set; }
        }
    }
}
