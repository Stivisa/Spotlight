using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Spotlight.Models.Identity;
namespace Spotlight.Hubs
{
    public class ChatHub : Hub
    {
	/* Ime metoda _PosaljiPoruku_, kao i parametar koji se salje _PrimiPoruku_ se 
         * koristi u okviru javascript koda prilikom poziva metoda.
         */
        public async Task SendMessage(Message message)
        {
            await Clients.All.SendAsync("receivedMessage",message);
        }
    }
}
