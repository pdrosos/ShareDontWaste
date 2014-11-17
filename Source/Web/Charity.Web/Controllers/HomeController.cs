namespace Charity.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Charity.Services.Common;
    using Charity.Web.Models.FoodDonations;
    using Charity.Web.Models.Donors;
    using Charity.Web.Models;
    using System.Web.UI;

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

        [OutputCache(Duration = 60, Location = OutputCacheLocation.Client)]
        public ActionResult Index()
        {
            // TODO: Move this in config
            var donorsCount = 10;
            var latestDonationsCount = 6;

            var mostActiveDonors = this.donorProfileService.GetMostActiveDonors(donorsCount)
                .Project().To<DonorViewModel>();

            var latestDonations = this.foodDonationService.GetLatestDonations(latestDonationsCount)
                .Project().To<FoodDonationViewModel>();

            var viewModel = new HomePageViewModel();
            viewModel.MostActiveDonors = mostActiveDonors;
            viewModel.LatestDonations = latestDonations;

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