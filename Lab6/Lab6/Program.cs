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
            using (var context = new BookingContext())
            {
                var customer = new Customer
                {
                    customer_name = "John Doe",
                    customer_details = "Frequent renter",
                    gender = "M",
                    email_address = "john@example.com",
                    phone_number = "123456789",
                    address_line_1 = "123 Main St",
                    address_line_2 = "Apt 1",
                    address_line_3 = "Some area",
                    town = "Sampletown",
                    county = "Samplecounty",
                    country = "Sampleland"
                };

                context.Customers.Add(customer);
                context.SaveChanges();
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
