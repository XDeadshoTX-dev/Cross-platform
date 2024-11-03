using Lab5.Controllers.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Text;

namespace Lab5
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            DotNetEnv.Env.Load();

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            builder.Services.AddOpenTelemetry()
            .WithTracing(tracerProviderBuilder => tracerProviderBuilder
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("lab5"))
                .AddAspNetCoreInstrumentation()
                .AddSqlClientInstrumentation()
                .AddZipkinExporter(options =>
                {
                    options.Endpoint = new Uri("http://localhost:9411/api/v2/spans");
                }))
            .WithMetrics(metricsProviderBuilder => metricsProviderBuilder
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("lab5"))
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddRuntimeInstrumentation()
                .AddPrometheusExporter());

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

            app.UseOpenTelemetryPrometheusScrapingEndpoint();
            app.MapRazorPages();
             
            app.Run();

        }
    }
}
