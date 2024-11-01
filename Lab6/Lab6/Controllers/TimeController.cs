using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System;
using TimeZoneConverter;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace Lab6.Controllers
{
    [ApiController]
    [Route("api/search")]
    [Authorize]
    public class TimeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("ConvertToUkraineTime")]
        public string ConvertToUkraineTime([FromBody] DateConversionRequest request)
        {
            try
            {
                if (!DateTimeOffset.TryParse(request.inputDate, null, DateTimeStyles.AssumeUniversal, out var parsedDate))
                {
                    return "Invalid date format. Please provide a valid date and time.";
                }

                var ukraineTimeZone = TZConvert.GetTimeZoneInfo("Europe/Kyiv");
                var ukraineTime = TimeZoneInfo.ConvertTime(parsedDate, ukraineTimeZone);

                var formattedUkraineTime = ukraineTime.ToString("yyyy-MM-dd HH:mm:ss zzz");

                var response = JsonConvert.SerializeObject(new
                {
                    convertedDate = formattedUkraineTime
                });
                return response;
            }
            catch (Exception ex)
            {
                return $"An error occurred: {ex.Message}";
            }
        }

        public class DateConversionRequest
        {
            public string inputDate { get; set; }
        }
    }
}
