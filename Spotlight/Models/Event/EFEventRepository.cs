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
    }
}