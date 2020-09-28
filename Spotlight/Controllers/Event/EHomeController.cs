using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Spotlight.Models.Event;
using Spotlight.Models.ViewModels;

namespace Spotlight.Controllers.Event
{
    [Route("Event/[controller]/[action]/{id?}")]
    public class EHomeController : Controller
    {
        private IEventRepository repository;
        public int PageSize = 3;

        public EHomeController(IEventRepository repo)
        {
            repository = repo;
        }
        [HttpGet]
        public ViewResult Index(string category, int currentPage = 1)
        {
            //Console.WriteLine("OOO: " + category);
            return View("~/Views/Event/Index.cshtml", new ItemsListViewModel {
                Events = repository.Events.Where(p => category == null || p.Category == category).OrderByDescending(m => m.Date).Skip((currentPage - 1)* PageSize).Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = currentPage,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ?
                        repository.Events.Count() :
                        repository.Events.Where(e =>
                            e.Category == category).Count()
                },
                CurrentCategory = category
            });
        }
    }
}