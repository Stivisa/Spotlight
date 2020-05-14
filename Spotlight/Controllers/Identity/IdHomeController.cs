using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Spotlight.Controllers.Identity
{
    [Route("Identity/[controller]/[action]/{id?}")]
    public class IdHomeController : Controller
    {
        public ViewResult Index()
        {
            return View("~/Views/Identity/Home/Index.cshtml",new Dictionary<string, object>
                { ["Placeholder"] = "Placeholder" });
        }
    }
}