using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.DATA;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CREATING CASTROL SERVER
           var host =  CreateHostBuilder(args).Build();
           using var scope = host.Services.CreateScope();
           var context = scope.ServiceProvider.GetRequiredService<StoreContext>();
           var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
           try
           {
                context.Database.Migrate();
                Dbinitializer.Initialize(context);
           }
           catch(Exception ex)
           {
            logger.LogError(ex,"Problem migrating data");
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
