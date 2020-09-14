using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Spotlight.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Spotlight.Controllers.Identity
{
    //[Authorize(Roles = "Organizacija")]
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

            return View("~/Views/Identity/Home/Index.cshtml", message);
        }

        public async Task<IActionResult> Create(Message message)
        {
            if (ModelState.IsValid)
            {
                message.UserName = User.Identity.Name;
                var sender = await userManager.GetUserAsync(User);
                //message.UserName = sender.UserName;
                message.UserID = sender.Id;

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