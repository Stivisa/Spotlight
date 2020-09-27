using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Spotlight.Models;
using Spotlight.Models.Listings;
using Spotlight.Models.ViewModels;
using Spotlight.Controllers.Event;
using Microsoft.AspNetCore.Identity;
using Spotlight.Models.Identity;

namespace Spotlight.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private INewsPostRepository newsRepository;
        private IListingRepository listingRepository;
        private UserManager<AppUser> userManager;

        public int pageSize = 4;

        public HomeController(ILogger<HomeController> logger, INewsPostRepository newsRepo, 
                              IListingRepository listingRepo, UserManager<AppUser> usrMgr)
        {
            _logger = logger;
            newsRepository = newsRepo;
            listingRepository = listingRepo;
            userManager = usrMgr;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Listings(int currentPage = 1)
        {
            return View(new ListingViewModel {
                Listings = listingRepository.AllListings.OrderByDescending(l => l.TimeOfPosting)
                                                        .Skip((currentPage - 1) * pageSize)
                                                        .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = currentPage,
                    ItemsPerPage = pageSize,
                    TotalItems = listingRepository.AllListings.Count()
                },
            });;
        }

        public IActionResult News(int currentPage = 1)
        {
            return View(new NewsPostViewModel
            {
                NewsPosts = newsRepository.AllNewsPosts.OrderByDescending(n => n.TimeOfPosting)
                                                       .Skip((currentPage - 1) * pageSize)
                                                       .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = currentPage,
                    ItemsPerPage = pageSize,
                    TotalItems = newsRepository.AllNewsPosts.Count()
                },
            }); ;
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

        [Authorize(Roles = "Admins")]
        [HttpPost]
        async public Task<IActionResult> AddNewPost(NewsPost newPost)
        {
            newPost.TimeOfPosting = DateTime.Now;
            var user = await userManager.GetUserAsync(User);
            newPost.UserID = user.Id;

            newsRepository.AddNews(newPost);

            return RedirectToAction("News");
        }

        [HttpPost]
        public IActionResult AddNewListing(Listing newListing)
        {
            newListing.TimeOfPosting = DateTime.Now;
            listingRepository.AddListing(newListing);

            return RedirectToAction("Listings");
        }

        [HttpPost]
        public IActionResult MakeDonation(Donation d)
        {
            listingRepository.AddDonation(d);

            return RedirectToAction("Listings");
        }
    }
}
