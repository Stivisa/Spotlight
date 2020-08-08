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
        private IListingRepository listingRepository;

        public HomeController(ILogger<HomeController> logger, INewsPostRepository newsRepo, IListingRepository listingRepo)
        {
            _logger = logger;
            newsRepository = newsRepo;
            listingRepository = listingRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Listings()
        {
            return View(listingRepository.AllListings.OrderBy(post => post.TimeOfPosting).Reverse());
        }

        public IActionResult News()
        {
            return View(newsRepository.AllNewsPosts.OrderBy(post => post.TimeOfPosting).Reverse());
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

        [HttpPost]
        public IActionResult AddNewPost(NewsPost newPost)
        {
            newPost.TimeOfPosting = DateTime.Now;
            //TODO: add new post to repository
            newsRepository.AddNews(newPost);

            return RedirectToAction("News");
        }
    }
}
