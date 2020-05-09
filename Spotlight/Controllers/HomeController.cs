using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Spotlight.Models;

namespace Spotlight.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private INewsPostRepository newsRepository;

        public HomeController(ILogger<HomeController> logger, INewsPostRepository repo)
        {
            _logger = logger;
            newsRepository = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Listings()
        {
            return View();
        }

        public IActionResult News()
        {
            return View(newsRepository.AllNewsPosts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
