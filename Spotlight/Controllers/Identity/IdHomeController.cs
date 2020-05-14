using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Spotlight.Controllers.Identity
{
    [Authorize(Roles = "Organizacija")]
    [Route("Identity/[controller]/[action]/{id?}")]
    public class IdHomeController : Controller
    {
        [Authorize]
        public ViewResult Index()
        {
            return View("~/Views/Identity/Home/Index.cshtml",new Dictionary<string, object>
                { ["Placeholder"] = "Placeholder" });
        }
    }
}