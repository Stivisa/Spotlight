using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Spotlight.Models.Event
{
    public static class EventSeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            EventDbContext context = app.ApplicationServices
            .GetRequiredService<EventDbContext>();
            context.Database.Migrate();
            if (!context.Events.Any())
            {
                context.Events.AddRange(
                new Event
                {
                    Header = "Kayak",
                    Description = "A boat for one person",
                    Category = "Watersports",
                    Date = DateTime.Now
                },
                new Event
                {
                    Header = "Lifejacket",
                    Description = "Protective and fashionable",
                    Category = "Watersports",
                    Date = DateTime.Now
                },
                new Event
                {
                    Header = "Soccer Ball",
                    Description = "FIFA-approved size and weight",
                    Category = "Soccer",
                    Date = new DateTime(2020, 12, 31, 5, 10, 20)
            },
                new Event
                {
                    Header = "Corner Flags",
                    Description = "Give your playing field a professional touch",
                    Category = "Soccer",
                    Date = new DateTime(2019, 12, 31, 5, 10, 20)
                },
                new Event
                {
                    Header = "Stadium",
                    Description = "Flat-packed 35,000-seat stadium",
                    Category = "Soccer",
                    Date = new DateTime(2018, 12, 31, 5, 10, 20)
                },
                new Event
                {
                    Header = "Thinking Cap",
                    Description = "Improve brain efficiency by 75%",
                    Category = "Chess",
                    Date = new DateTime(2017, 12, 31, 5, 10, 20)
                },
                new Event
                {
                    Header = "Unsteady Chair",
                    Description = "Secretly give your opponent a disadvantage",
                    Category = "Chess",
                    Date = new DateTime(2016, 12, 31, 5, 10, 20)
                }
                );
                context.SaveChanges();
            }
        }
    }
}
