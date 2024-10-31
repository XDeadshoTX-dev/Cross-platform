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
        public async Task<string> GetBookingInformation(int id)
        {
            using (var context = new BookingContext())
            {
                var _list = await context.Bookings.Include(b => b.Vehicle).Include(b => b.Customer).FirstOrDefaultAsync(b => b.booking_id == id);
                var response = JsonConvert.SerializeObject(new
                {
                    customer_id = _list.customer_id,
                    customer_name = _list.Customer.customer_name,
                    customer_details = _list.Customer.customer_details,
                    reg_number = _list.reg_number,
                    manufacturer_code = _list.Vehicle.manufacturer_code,
                    model_code = _list.Vehicle.model_code
                });
                return response;
            }
        }
        [HttpPost]
        public async Task<string> VehicleCategoryInformation(string id)
        {
            using(var context = new BookingContext())
            {
                var _list = await context.VehicleCategories.FirstOrDefaultAsync(v => v.vehicle_category_code == id);
                var response = JsonConvert.SerializeObject(new
                {
                    vehicle_category_description = _list.vehicle_category_description
                });
                return response;
            }
        }
        [HttpPost]
        public async Task<string> ModelInformation(string id)
        {
            using (var context = new BookingContext())
            {
                var _list = await context.Models.FirstOrDefaultAsync(m => m.model_code == id);
                var response = JsonConvert.SerializeObject(new
                {
                    model_name = _list.model_name,
                    daily_hire_rate = _list.daily_hire_rate
                });
                return response;
            }
        }
    }
}
