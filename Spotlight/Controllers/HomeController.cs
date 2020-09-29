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

        public int pageSize = 3;

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
            var Donations = listingRepository.DonationRecords;
            var CountDon = Donations.Count();
            var don1 = Donations.Where(e => e.value < 100).Count();
            var don2 = Donations.Where(e => e.value >= 100 && e.value < 500).Count();
            var don3 = Donations.Where(e => e.value >= 500 && e.value < 1000).Count();
            var don4 = Donations.Where(e => e.value > 1000).Count();
            int[] chart = { CountDon, don1, don2, don3, don4 };
            return View(chart);
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

        public IActionResult AboutUs(int currentPage = 1)
        {
            return View("~/Views/Home/AboutUs.cshtml");
        }

        public IActionResult Statistics(int currentPage = 1)
        {
            var Donations = listingRepository.DonationRecords;
            var CountDon = Donations.Count();
            var don1 = Donations.Where(e => e.value < 100).Count();
            var don2 = Donations.Where(e => e.value >= 100 && e.value < 500).Count();
            var don3 = Donations.Where(e => e.value >= 500 && e.value < 1000).Count();
            var don4 = Donations.Where(e => e.value > 1000).Count();

            var jan = Donations.Where(e => e.When.Month == 1).Count();
            var feb = Donations.Where(e => e.When.Month == 2).Count();
            var mar = Donations.Where(e => e.When.Month == 3).Count();
            var apr = Donations.Where(e => e.When.Month == 4).Count();
            var maj = Donations.Where(e => e.When.Month == 5).Count();
            var jun = Donations.Where(e => e.When.Month == 6).Count();
            var jul = Donations.Where(e => e.When.Month == 7).Count();
            var aug = Donations.Where(e => e.When.Month == 8).Count();
            var sep = Donations.Where(e => e.When.Month == 9).Count();
            var oct = Donations.Where(e => e.When.Month == 10).Count();
            var nov = Donations.Where(e => e.When.Month == 11).Count();
            var dec = Donations.Where(e => e.When.Month == 12).Count();

            int[] chart = { CountDon, don1, don2, don3, don4, jan, feb, mar, apr, maj, jun, jul, aug, sep, oct, nov, dec };

            return View(chart);
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

        [Authorize(Roles = "Admins")]
        [HttpPost]
        async public Task<IActionResult> EditNewsPost(NewsPost newPost)
        {
            newPost.TimeOfPosting = DateTime.Now;
            var user = await userManager.GetUserAsync(User);
            newPost.UserID = user.Id;

            newsRepository.UpdateNewsPost(newPost);

            return RedirectToAction("News");
        }

        [Authorize(Roles = "Admins")]
        [HttpPost]
        public IActionResult DeleteNewsPost(NewsPost post)
        {
            newsRepository.DeleteNewsPost(post);

            return RedirectToAction("News");
        }

        [Authorize]
        [HttpPost]
        async public Task<IActionResult> AddNewListing(Listing newListing)
        {
            newListing.TimeOfPosting = DateTime.Now;
            var user = await userManager.GetUserAsync(User);
            newListing.UserID = user.Id;

            listingRepository.AddListing(newListing);

            return RedirectToAction("Listings");
        }

        [Authorize(Roles = "Admins")]
        [HttpPost]
        async public Task<IActionResult> EditListing(Listing newListing)
        {
            newListing.TimeOfPosting = DateTime.Now;
            var user = await userManager.GetUserAsync(User);
            newListing.UserID = user.Id;

            listingRepository.UpdateListing(newListing);

            return RedirectToAction("Listings");
        }

        [Authorize(Roles = "Admins")]
        [HttpPost]
        public IActionResult DeleteListing(Listing listing)
        {
            listingRepository.DeleteListing(listing);

            return RedirectToAction("Listings");
        }

        [HttpPost]
        public IActionResult MakeDonation(Donation d)
        {
            listingRepository.AddDonation(d);
            
            listingRepository.AddDonationRecord(d.value);

            return RedirectToAction("Listings");
        }
    }
}
