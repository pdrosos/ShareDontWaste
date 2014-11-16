namespace Charity.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Charity.Data.Models;
    using Charity.Services.Common;
    using Charity.Common;
    using Charity.Web.Infrastructure.Identity;
    using Charity.Web.Models.FoodDonations;
    using PagedList;

    public class FoodDonationsController : Controller
    {
        private readonly IFoodDonationService foodDonationService;
        private readonly IFoodRequestService foodRequestService;
        private readonly IFoodCategoryService foodCategoryService;
        private readonly IRecipientProfileService recipientProfileService;
        private readonly ICurrentUser currentUserProvider;
        
        public FoodDonationsController(
            IFoodDonationService foodDonationService,
            IFoodRequestService foodRequestService,
            IFoodCategoryService foodCategoryService,
            IRecipientProfileService recipientProfileService,
            ICurrentUser currentUserProvider)
        {
            this.foodDonationService = foodDonationService;
            this.foodRequestService = foodRequestService;
            this.foodCategoryService = foodCategoryService;
            this.recipientProfileService = recipientProfileService;
            this.currentUserProvider = currentUserProvider;
        }
        
        public ActionResult Index(int? page)
        {
            int pageSize = 12;
            int pageIndex = (page ?? 1);

            var allDonations = this.foodDonationService.List().Project().To<FoodDonationViewModel>();

            var donationsPagedList = allDonations.ToPagedList(pageIndex, pageSize);
            var foodCategories = this.foodCategoryService.GetAll().AsEnumerable();

            var viewModel = new FoodDonationListViewModel();            
            viewModel.FoodCategories = foodCategories;
            viewModel.FoodDonations = donationsPagedList;

            return View(viewModel);
        }

        public ActionResult ByCategory(int id, int? page)
        {
            int pageSize = 12;
            int pageIndex = (page ?? 1);

            var allDonations = this.foodDonationService.ListByCategory(id).Project().To<FoodDonationViewModel>();

            var donationsPagedList = allDonations.ToPagedList(pageIndex, pageSize);
            var foodCategories = this.foodCategoryService.GetAll().AsEnumerable();

            var viewModel = new FoodDonationListViewModel();
            viewModel.FoodCategories = foodCategories;
            viewModel.FoodDonations = donationsPagedList;
            
            ViewBag.category = this.foodCategoryService.GetById(id);

            return View("Index", viewModel);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            FoodDonation foodDonation = this.foodDonationService.GetById((int)id);

            if (foodDonation == null)
            {
                return HttpNotFound();
            }

            FoodDonationViewModel model = Mapper.Map<FoodDonation, FoodDonationViewModel>(foodDonation);

            
            if (User.IsInRole(GlobalConstants.RecipientRoleName))
            {
                ApplicationUser user = this.currentUserProvider.Get();
                Recipient recipient = this.recipientProfileService.GetByApplicationUserId(user.Id);
                var existingRequest = this.foodRequestService.GetByDonationIdAndRecipientId((int)id, recipient.Id);

                if (existingRequest == null)
                {
                    ViewBag.showRequestForm = true;
                }
            }
            
            return View(model);
        }
    }
}