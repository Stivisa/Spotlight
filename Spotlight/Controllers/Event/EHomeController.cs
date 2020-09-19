using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Spotlight.Models.Event;

namespace Spotlight.Controllers.Event
{
    [Route("Event/[controller]/[action]/{id?}")]
    public class EHomeController : Controller
    {
        private IEventRepository repository;

        public EHomeController(IEventRepository repo)
        {
            repository = repo;
        }
        public IActionResult Index()
        {
            return View("~/Views/Event/Index.cshtml",repository.Events);
        }
    }
}