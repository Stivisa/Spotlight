using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotlight.Models.Event
{
    public interface IEventRepository
    {
        IQueryable<Event> Events { get; }
        void SaveEvent(Event newevent);

        Event DeleteEvent(int eventID);
    }
}
