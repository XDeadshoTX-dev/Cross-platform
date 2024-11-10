using Grpc.Core;
using Lab6.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Lab6.Controllers
{
    [ApiController]
    [Route("api/add")]
    public class AddController : Controller
    {
        BookingContext context;
        public AddController() 
        {
            context = new BookingContext();
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("AddBookingInformation")]
        public async Task<string> AddBookingInformation([FromBody] BookingModelRequest request)
        {
            try
            {
                context.Bookings.Add(new Booking
                {
                    booking_status_code = request.booking_status_code,
                    customer_id = request.customer_id,
                    reg_number = request.reg_number,
                    date_from = request.date_from,
                    date_to = request.date_to,
                    confirmation_letter_sent_yn = request.confirmation_letter_sent_yn,
                    payment_received_yn = request.payment_received_yn
                });
                await context.SaveChangesAsync();
                return JsonSerializer.Serialize(new
                {
                    response = "OK"
                });
            }
            catch (Exception ex)
            {
                return JsonSerializer.Serialize(new
                {
                    response = $"Error: {ex.Message}. InnerException: {ex.InnerException.Message}"
                });
            }
        }
        public class BookingModelRequest
        {
            public string booking_status_code { get; set; }
            public int customer_id { get; set; }
            public string reg_number { get; set; }
            public DateTime date_from { get; set; }
            public DateTime date_to { get; set; }
            public string confirmation_letter_sent_yn { get; set; }
            public string payment_received_yn { get; set; }
        }
        [HttpPost("AddModelInformation")]
        public async Task<string> AddModelInformation([FromBody] ModelInformationRequest request)
        {
            try
            {
                context.Models.Add(new Model
                {
                    model_code = request.model_code,
                    daily_hire_rate = request.daily_hire_rate,
                    model_name = request.model_name
                });
                await context.SaveChangesAsync();
                return JsonSerializer.Serialize(new
                {
                    response = "OK"
                });
            }
            catch (Exception ex)
            {
                return JsonSerializer.Serialize(new
                {
                    response = $"Error: {ex.Message}. InnerException: {ex.InnerException.Message}"
                });
            }
        }
        public class ModelInformationRequest
        {
            public string model_code { get; set; }
            public decimal daily_hire_rate { get; set; }
            public string model_name { get; set; }
        }
        [HttpPost("AddVehicleCategoryInformation")]
        public async Task<string> AddVehicleCategoryInformation([FromBody] VehicleCategoryModelRequest request)
        {
            try
            {
                context.VehicleCategories.Add(new VehicleCategory
                {
                    vehicle_category_code = request.vehicle_category_code,
                    vehicle_category_description = request.vehicle_category_description
                });
                await context.SaveChangesAsync();
                return JsonSerializer.Serialize(new
                {
                    response = "OK"
                });
            }
            catch(Exception ex)
            {
                return JsonSerializer.Serialize(new
                {
                    response = $"Error: {ex.Message}. InnerException: {ex.InnerException.Message}"
                });
            }
        }
        public class VehicleCategoryModelRequest
        {
            public string vehicle_category_code { get; set; }
            public string vehicle_category_description { get; set; }
        }
    }
}
