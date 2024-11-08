using Lab5.Controllers.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

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

            Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
            {
                IndexFormat = "logs-{0:yyyy.MM.dd}",
                AutoRegisterTemplate = true,
                AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog,
            })
            .CreateLogger();
            builder.Host.UseSerilog();

            var app = builder.Build();

            //app.Use(async (context, next) =>
            //{
            //    var tracer = app.Services.GetRequiredService<TracerProvider>().GetTracer("lab5");

            //    using (var span = tracer.StartActiveSpan("request_pipeline"))
            //    {
            //        span.SetAttribute("user_id", "12345");
            //        span.SetAttribute("transaction_id", "TX-6789");

            //        using (var longRunningSpan = tracer.StartActiveSpan("long_running_process"))
            //        {
            //            longRunningSpan.SetAttribute("description", "Simulating a long task");
            //            await Task.Delay(5000);
            //        }

            //        await next.Invoke();

            //        var metrics = CollectMetrics();
            //        await SendMetricsToElasticsearch(metrics);
            //    }
            //});

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


            //app.UseOpenTelemetryPrometheusScrapingEndpoint();
            //app.UseSerilogRequestLogging();
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
