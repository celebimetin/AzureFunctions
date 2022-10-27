using AzureFunctionApp.Models;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(AzureFunctionApp.Startup))]
namespace AzureFunctionApp
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