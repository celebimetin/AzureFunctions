using HttpTriggerFunctionApp.Models;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(HttpTriggerFunctionApp.Startup))]
namespace HttpTriggerFunctionApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var constr = Environment.GetEnvironmentVariable("SqlConnectionString");
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(constr);
            });
        }
    }
}