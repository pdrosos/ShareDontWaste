namespace Charity.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Charity.Services.Common;
    using Charity.Web.Models.FoodDonations;
    using PagedList;

    public class FoodDonationsController : Controller
    {
        private readonly IFoodDonationService foodDonationService;
        private readonly IFoodCategoryService foodCategoryService;

        public FoodDonationsController(IFoodDonationService foodDonationService, IFoodCategoryService foodCategoryService)
        {
            this.foodDonationService = foodDonationService;
            this.foodCategoryService = foodCategoryService;
        }

        // GET: FoodDonations
        public ActionResult Index(int? page)
        {
            int pageSize = 3;
            int pageIndex = (page ?? 1);
            
            var allDonations = this.foodDonationService.All().OrderByDescending(d => d.CreatedOn).Project().To<FoodDonationViewModel>();

            var donationsPagedList = allDonations.ToPagedList(pageIndex, pageSize);
            var foodCategories = this.foodCategoryService.GetAll().AsEnumerable();

            var viewModel = new FoodDonationListViewModel();            
            viewModel.FoodCategories = foodCategories;
            viewModel.FoodDonations = donationsPagedList;

            return View(viewModel);
        }
    }
}