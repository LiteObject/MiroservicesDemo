namespace MiroservicesDemo
{
    using System;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using MiroservicesDemo.Order.Data;

    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var serviceScope = host.Services.CreateScope())
            {                
                var services = serviceScope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>();

                try
                {
                    var orderDb = services.GetRequiredService<OrderDbContext>();

                    if (orderDb.Database.IsSqlServer())
                    {
                        logger.LogInformation("Calling Database.Migrate() to apply database changes");
                        orderDb.Database.Migrate();
                    }

                    logger.LogInformation("Call services from main");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred.");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
