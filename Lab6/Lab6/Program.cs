using Lab5.Controllers.Middleware;
using Lab6.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Lab5.Controllers.Managements;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Microsoft.Identity.Client;
using System.Diagnostics;
using System.Text.Json;

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

            app.Use(async (context, next) =>
            {
                var tracer = app.Services.GetRequiredService<TracerProvider>().GetTracer("lab6");

                using (var span = tracer.StartActiveSpan("request_pipeline"))
                {
                    span.SetAttribute("user_id", "12345");
                    span.SetAttribute("transaction_id", "TX-6789");

                    using (var longRunningSpan = tracer.StartActiveSpan("long_running_process"))
                    {
                        longRunningSpan.SetAttribute("description", "Simulating a long task");
                        await Task.Delay(5000);
                    }

                    await next.Invoke();

                    var metrics = CollectMetrics();
                    await SendMetricsToElasticsearch(metrics);
                }
            });

            Metrics CollectMetrics()
            {
                var gcCount = GC.CollectionCount(0);
                var usedMemory = GC.GetTotalMemory(false);
                var cpuUsage = GetCpuUsage();

                return new Metrics
                {
                    GcCount = gcCount,
                    UsedMemory = usedMemory,
                    CpuUsage = cpuUsage,
                    Timestamp = DateTime.UtcNow
                };
            }
            double GetCpuUsage()
            {
                using (var process = Process.GetCurrentProcess())
                {
                    return (process.TotalProcessorTime.TotalMilliseconds / Environment.ProcessorCount) * 100 / (Environment.TickCount / 1000);
                }
            }
            async Task SendMetricsToElasticsearch(Metrics metrics)
            {
                using var client = new HttpClient();

                var username = Environment.GetEnvironmentVariable("ElasticSearchUsername");
                var password = Environment.GetEnvironmentVariable("ElasticSearchPassword");

                var byteArray = Encoding.ASCII.GetBytes($"{username}:{password}");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                var elasticsearchUrl = "http://localhost:9200/my-index/_doc";
                var json = JsonSerializer.Serialize(metrics);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(elasticsearchUrl, content);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error sending metrics to Elasticsearch: {response.StatusCode}");
                }
            }

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
        public class Metrics
        {
            public int GcCount { get; set; }
            public long UsedMemory { get; set; }
            public double CpuUsage { get; set; }
            public DateTime Timestamp { get; set; }
        }
    }
}
