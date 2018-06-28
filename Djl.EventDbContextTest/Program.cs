﻿using System;
using System.Linq;
using System.Reflection;
using Djl.EventDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Djl.EventDbContextTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<EventDbcontext>(builder =>
            {
                builder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EventDbcontext;Trusted_Connection=True;", x =>
                {
                    x.MigrationsAssembly(Assembly.GetAssembly(typeof(EventDbcontext)).GetName().Name);
                });
            });
            var serviceProvider = serviceCollection.BuildServiceProvider(true);
            var dbcontext = serviceProvider.GetRequiredService<EventDbcontext>();
            var messages = dbcontext.MqEventMessages.ToList();
            foreach (var message in messages)
            {
                Console.WriteLine($"{message.Id}");
            }
            Console.ReadKey();
        }
    }
}