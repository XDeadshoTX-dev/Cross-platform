using Lab5.Controllers.Middleware;
using Lab6.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Lab5.Controllers.Managements;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Lab5
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            DotNetEnv.Env.Load();
            // For debugging purposes
            //using (var context = new BookingContext())
            //{
            //    BookingStatus customer = new BookingStatus { booking_status_code = "CON", booking_status_description = "Confirmed" };
            //    context.BookingStatuses.Add(customer);
            //    context.SaveChanges();
            //}
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

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    policy => policy.WithOrigins("http://localhost:5232")
                                    .AllowAnyMethod()
                                    .AllowAnyHeader());
            });


            AuthManagements authManagements = new AuthManagements();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = $"https://{authManagements.GetAuth0Domain}/";
                options.Audience = authManagements.GetAudience;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = $"https://{authManagements.GetAuth0Domain}/",
                    ValidateAudience = true,
                    ValidAudience = authManagements.GetAudience,
                    ValidateLifetime = true
                };
            });

            builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });

            builder.Services.AddOpenTelemetry()
            .WithTracing(tracerProviderBuilder => tracerProviderBuilder
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("lab6"))
                .AddAspNetCoreInstrumentation()
                .AddSqlClientInstrumentation()
                .AddZipkinExporter(options =>
                {
                    options.Endpoint = new Uri("http://localhost:9411/api/v2/spans");
                }))
            .WithMetrics(metricsProviderBuilder => metricsProviderBuilder
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("lab6"))
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddRuntimeInstrumentation()
                .AddPrometheusExporter());


            var app = builder.Build();

            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors("AllowSpecificOrigin");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<TokenAuthenticationMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseOpenTelemetryPrometheusScrapingEndpoint();
            app.MapRazorPages();

            app.Run();

        }
    }
}
