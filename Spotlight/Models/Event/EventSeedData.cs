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
                    Header = "Event1",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry." +
                    " Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book." +
                    " It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.",
                    Category = "Hitno",
                    Date = DateTime.Now
                },
                new Event
                {
                    Header = "Event2",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry." +
                    " Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book." +
                    " It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.",
                    Category = "Lecenje",
                    Date = DateTime.Now
                },
                new Event
                {
                    Header = "Event3",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry." +
                    " Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book." +
                    " It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.",
                    Category = "Dom",
                    Date = new DateTime(2020, 12, 31, 5, 10, 20)
            },
                new Event
                {
                    Header = "Event4",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry." +
                    " Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book." +
                    " It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.",
                    Category = "Hitno",
                    Date = new DateTime(2019, 12, 31, 5, 10, 20)
                },
                new Event
                {
                    Header = "Event5",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry." +
                    " Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book." +
                    " It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.",
                    Category = "Hitno",
                    Date = new DateTime(2018, 12, 31, 5, 10, 20)
                },
                new Event
                {
                    Header = "Event6",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry." +
                    " Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book." +
                    " It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.",
                    Category = "Hitno",
                    Date = new DateTime(2017, 12, 31, 5, 10, 20)
                },
                new Event
                {
                    Header = "Event7",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry." +
                    " Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book." +
                    " It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.",
                    Category = "Hitno",
                    Date = new DateTime(2016, 12, 31, 5, 10, 20)
                }
                );
                context.SaveChanges();
            }
        }
    }
}
