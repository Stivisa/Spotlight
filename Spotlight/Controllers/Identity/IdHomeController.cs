using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Spotlight.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Spotlight.Models.ViewModels;

namespace Spotlight.Controllers.Identity
{
    [Authorize]
    [Route("Identity/[controller]/[action]/{id?}")]
    public class IdHomeController : Controller
    {
        private UserManager<AppUser> userManager;
        private AppIdentityDbContext context;

        public IdHomeController(AppIdentityDbContext con, UserManager<AppUser> usrMgr)
        {
            userManager = usrMgr;
            context = con;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await userManager.GetUserAsync(User);
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.CurrentUserName = currentUser.UserName;
            }
            var message = await context.Messages.ToListAsync();

            return View("~/Views/Identity/Home/Index.cshtml", new MessageListViewModel
            {
                Messages = message ,              
                messageText = ""
            });
            //return View("~/Views/Identity/Home/Index.cshtml", message);
        }

        public async Task<IActionResult> Create(Models.ViewModels.MessageListViewModel model)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine("LALALA" + model.messageText);
                //message.UserName = User.Identity.Name;
                var sender = await userManager.GetUserAsync(User);
                //message.UserName = sender.UserName;
                //message.UserID = sender.Id;

                Message message = new Message
                {
                    UserName = User.Identity.Name,
                    UserID = sender.Id,
                    Text = model.messageText
            };

            //Console.WriteLine("LALALA" + message.Text);              

            await context.Messages.AddAsync(message);
                await context.SaveChangesAsync();
                return RedirectToAction("Index", "IdHome"); //refresh dont workw
                                                            // return Ok();
            }
            else
            {
                return StatusCode(400);
            }
        }
    }
}