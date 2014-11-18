namespace Charity.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.UI;
    using AutoMapper.QueryableExtensions;
    using Charity.Services.Common;
    using Charity.Web.Models;
    using Charity.Web.Models.Donors;
    using Charity.Web.Models.FoodDonations;

    public class HomeController : Controller
    {
        private readonly IDonorProfileService donorProfileService;
        private readonly IFoodDonationService foodDonationService;

        public HomeController(
            IDonorProfileService donorProfileService, 
            IFoodDonationService foodDonationService)
        {
            this.donorProfileService = donorProfileService;
            this.foodDonationService = foodDonationService;
        }

        public ActionResult Index()
        {
            // TODO: Move this in config
            var donorsCount = 10;
            var latestDonationsCount = 8;

            if (this.HttpContext.Cache["HomePageDonors"] == null)
            {
                var mostActiveDonors = this.donorProfileService.GetMostActiveDonors(donorsCount)
                    .Project().To<DonorViewModel>();

                this.HttpContext.Cache.Add("HomePageDonors", mostActiveDonors.ToList(), null, DateTime.Now.AddMinutes(5), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Default, null);
            }

            if (this.HttpContext.Cache["HomePageDonations"] == null)
            {
                var latestDonations = this.foodDonationService.GetLatestDonations(latestDonationsCount)
                    .Project().To<FoodDonationViewModel>();

                this.HttpContext.Cache.Add("HomePageDonations", latestDonations.ToList(), null, DateTime.Now.AddMinutes(5), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Default, null);
            }

            var viewModel = new HomePageViewModel();
            viewModel.MostActiveDonors = (List<DonorViewModel>)this.HttpContext.Cache["HomePageDonors"];
            viewModel.LatestDonations = (List<FoodDonationViewModel>)this.HttpContext.Cache["HomePageDonations"]; ;

            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}