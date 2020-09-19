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
        public int PageSize = 4;

        public EHomeController(IEventRepository repo)
        {
            repository = repo;
        }
        public ViewResult Index(int currentPage = 1)
        {
            return View("~/Views/Event/Index.cshtml", new ItemsListViewModel {
                Events = repository.Events.OrderByDescending(m => m.Date).Skip((currentPage - 1)* PageSize).Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = currentPage,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Events.Count()
                }
            });
        }
    }
}