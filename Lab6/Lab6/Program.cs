using Lab5.Controllers.Middleware;
using Lab6.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace Lab5
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            DotNetEnv.Env.Load();
            // For debugging purposes
            using (var context = new BookingContext())
            {
                BookingStatus customer = new BookingStatus { booking_status_code = "CON", booking_status_description = "Confirmed" };
                context.BookingStatuses.Add(customer);
                context.SaveChanges();
            }
            using (var context = new BookingContext())
            {
                var bookingStatuses = context.BookingStatuses.ToList();

                Console.WriteLine("List BookingStatus:");
                foreach (var status in bookingStatuses)
                {
                    Console.WriteLine($"Status code: {status.booking_status_code}, Description: {status.booking_status_description}");
                }
            }

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.UseMiddleware<TokenAuthenticationMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.MapRazorPages();
             
            app.Run();

        }
    }
}
