using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spotlight.Models.Identity;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Spotlight.Controllers.Identity
{
    [Route("Identity/[controller]/[action]/{id?}")]
    public class IdAdminController : Controller
    {
        private UserManager<AppUser> userManager;
        public IdAdminController(UserManager<AppUser> usrMgr)
        {
            userManager = usrMgr;
        }
        public ViewResult Index() => View("~/Views/Identity/Admin/Index.cshtml",userManager.Users);
    }
}
