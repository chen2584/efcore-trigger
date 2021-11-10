using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyConsole.Entities;
using MyConsole.Triggers;

namespace MyConsole
{
    class Program
    {
        static IServiceProvider GetServiceProvider()
        {
            var services = new ServiceCollection();
            services.AddDbContext<MyContext>(options =>
            {
                options.UseNpgsql("Server=localhost;Port=5432;Database=revenue_lab;User Id=postgres;Password=abcABC123;");
                options.UseTriggers(triggerOptions => {
                    triggerOptions.AddTrigger<RevenueSaveTrigger>();
                });
            });

            return services.BuildServiceProvider();
        }

        static void Main(string[] args)
        {
            var serviceProvider = GetServiceProvider();

            using var myContext = serviceProvider.GetService<MyContext>();
            myContext.Database.EnsureCreated();

            var revenue = new Revenue
            {
                UserId = 1,
                Amount = 10.301f,
                CreateDate = DateTime.UtcNow
            };
            myContext.Revenues.Add(revenue);
            myContext.SaveChanges();
            // var revenue = myContext.Revenues.FirstOrDefault();
            // revenue.Amount = 100.1f;
            // myContext.SaveChanges();

            Console.WriteLine("Completed!");
        }
    }
}
