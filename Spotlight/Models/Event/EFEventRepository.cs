using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotlight.Models.Event
{
    public class EFEventRepository : IEventRepository
    {
        private EventDbContext context;
        public EFEventRepository(EventDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Event> Events => context.Events;

        public void SaveEvent(Event newevent)
        {
            if (newevent.EventID == 0)
            {
                context.Events.Add(newevent);
            }
            else
            {
                Event dbEntry = context.Events
                .FirstOrDefault(p => p.EventID == newevent.EventID);
                if (dbEntry != null)
                {
                    dbEntry.Header = newevent.Header;
                    dbEntry.Description = newevent.Description;                  
                    dbEntry.Category = newevent.Category;
                    dbEntry.Date = DateTime.Now;
                    dbEntry.ImageName = newevent.ImageName;
                }
            }
            context.SaveChanges();
        }
        public Event DeleteEvent(int eventID)
        {
            Event dbEntry = context.Events
            .FirstOrDefault(p => p.EventID == eventID);
            if (dbEntry != null)
            {
                context.Events.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}