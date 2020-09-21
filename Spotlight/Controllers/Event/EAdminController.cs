using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spotlight.Models.Event;

namespace Spotlight.Controllers.Event
{
    [Authorize]
    [Route("Identity/[controller]/[action]/{id?}")]
    public class EAdminController : Controller
    {
        private IEventRepository repository;
        public EAdminController(IEventRepository repo)
        {
            repository = repo;
        }
        public ViewResult Index()
        {
            return View("~/Views/Event/AdminIndex.cshtml", repository.Events.OrderByDescending(m => m.Date));
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

        public ViewResult Create() => View("~/Views/Event/Edit.cshtml", new Models.Event.Event());
      
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