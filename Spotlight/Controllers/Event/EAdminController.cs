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
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Spotlight.Controllers.Event
{  
    [Authorize]
    [Route("Identity/[controller]/[action]/{id?}")]
    public class EAdminController : Controller
    {
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly IEventRepository repository;
        private readonly UserManager<AppUser> userManager;
        public EAdminController(IEventRepository repo, UserManager<AppUser> usrMgr, IWebHostEnvironment hostEnvironment)
        {
            userManager = usrMgr;
            repository = repo;
            this.hostEnvironment = hostEnvironment;
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
        public async Task<IActionResult> Edit(Models.Event.Event newevent)
        {
            if (ModelState.IsValid)
            {
                if (newevent.ImageFile != null)
                {
                    string wwwRootPath = hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(newevent.ImageFile.FileName);
                    string extension = Path.GetExtension(newevent.ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmss") + extension;
                    newevent.ImageName = fileName;
                    string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await newevent.ImageFile.CopyToAsync(fileStream);
                    }
                }

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
                if (newevent.ImageFile != null)
                {
                    string wwwRootPath = hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(newevent.ImageFile.FileName);
                    string extension = Path.GetExtension(newevent.ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmss") + extension;
                    newevent.ImageName = fileName;
                    string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await newevent.ImageFile.CopyToAsync(fileStream);
                    }
                }

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
            Models.Event.Event temp = repository.Events.FirstOrDefault(p => p.EventID == eventId);
            if (temp.ImageName != null)
            {
                var imagePath = Path.Combine(hostEnvironment.WebRootPath, "image", temp.ImageName);
                if (System.IO.File.Exists(imagePath))
                    System.IO.File.Delete(imagePath);
            }

            Models.Event.Event deletedEvent = repository.DeleteEvent(eventId);
            if (deletedEvent != null)
            {
                TempData["message"] = $"{deletedEvent.Header} was deleted";
            }
            return RedirectToAction("Index", "EAdmin");
        }
    }
}