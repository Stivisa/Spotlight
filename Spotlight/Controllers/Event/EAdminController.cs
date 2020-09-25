using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spotlight.Models.Event;
using Spotlight.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace Spotlight.Controllers.Event
{
    [Authorize]
    [Route("Identity/[controller]/[action]/{id?}")]
    public class EAdminController : Controller
    {
        private IEventRepository repository;
        private UserManager<AppUser> userManager;
        public EAdminController(IEventRepository repo, UserManager<AppUser> usrMgr)
        {
            userManager = usrMgr;
            repository = repo;
        }
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            var events = repository.Events.Where(e => e.UserName == user.UserName).OrderByDescending(m => m.Date);

            if (await userManager.IsInRoleAsync(user, "Admins")) 
            {
                events = repository.Events.OrderByDescending(m => m.Date);
            }

            return View("~/Views/Event/AdminIndex.cshtml", events);
        }

        public ViewResult Edit(int eventId) =>
            View("~/Views/Event/Edit.cshtml", repository.Events
            .FirstOrDefault(p => p.EventID == eventId));

        [HttpPost]
        public IActionResult Edit(Models.Event.Event newevent)
        {
            if (ModelState.IsValid)
            {
                repository.SaveEvent(newevent);
                TempData["message"] = $"{newevent.Header} has been saved";
                return RedirectToAction("Index", "EAdmin");
            }
            else
            {
                // there is something wrong with the data values
                return View("~/Views/Event/Edit.cshtml",newevent);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Models.Event.Event newevent)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                newevent.UserID = user.Id;
                newevent.UserName = User.Identity.Name;
                repository.SaveEvent(newevent);
                TempData["message"] = $"{newevent.Header} has been saved";
                return RedirectToAction("Index", "EAdmin");
            }
            else
            {
                // there is something wrong with the data values
                return View("~/Views/Event/Create.cshtml", newevent);
            }
        }

        public ViewResult Create() => View("~/Views/Event/Create.cshtml", new Models.Event.Event());
      
        public IActionResult Delete(int eventId)
        {
            Models.Event.Event deletedProduct = repository.DeleteEvent(eventId);
            if (deletedProduct != null)
            {
                TempData["message"] = $"{deletedProduct.Header} was deleted";
            }
            return RedirectToAction("Index", "EAdmin");
        }
    }
}