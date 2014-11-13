namespace Charity.Web.Areas.Donors.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Charity.Data.Models;
    using Charity.Services;
    using Charity.Web.Areas.Donors.Models;
    using Charity.Web.Infrastructure.Identity;
    using Charity.Common;

    [Authorize]
    public class FoodDonationsController : Controller
    {
        private readonly FoodDonationService foodDonationService;
        private readonly FoodCategoryService foodCategoryService;
        private readonly DonorProfileService donorProfileService;
        private readonly CityService cityService;
        private readonly ICurrentUser currentUserProvider;

        public FoodDonationsController(
            FoodDonationService foodDonationService,
            FoodCategoryService foodCategoryService,
            DonorProfileService donorProfileService,
            CityService cityService, 
            ICurrentUser currentUserProvider
            )
        {
            this.foodDonationService = foodDonationService;
            this.foodCategoryService = foodCategoryService;
            this.donorProfileService = donorProfileService;
            this.cityService = cityService;
            this.currentUserProvider = currentUserProvider;
        }

        public ActionResult Index()
        {
            var foodDonations = this.foodDonationService.All().Project().To<FoodDonationListViewModel>();
            return View(foodDonations.AsEnumerable());
        }

        public ActionResult MyDonations()
        {
            ApplicationUser user = this.currentUserProvider.Get();
            Donor donor = this.donorProfileService.GetByApplicationUserId(user.Id);
            string currentDonorId = donor.Id.ToString();
            
            var foodDonations = this.foodDonationService.All().Where(f => f.DonorId == currentDonorId).Project().To<FoodDonationListViewModel>();
            
            return View(foodDonations.AsEnumerable());
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

            return View(model);
        }

        [Authorize(Roles = GlobalConstants.DonorRoleName)]
        public ActionResult Create()
        {
            ViewBag.FoodCategoryId = new SelectList(this.foodCategoryService.GetAll(), "Id", "Name");
            return View();
        }
       
        [HttpPost]
        [Authorize(Roles = GlobalConstants.DonorRoleName)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FoodDonationRegisterModel model)
        {
            FoodDonation foodDonation = Mapper.Map<FoodDonationRegisterModel, FoodDonation>(model);
            
            ApplicationUser user = this.currentUserProvider.Get();
            Donor donor = this.donorProfileService.GetByApplicationUserId(user.Id);
            
            foodDonation.DonorId = donor.Id.ToString();

            if (ModelState.IsValid)
            {
                this.foodDonationService.Add(foodDonation);
                return RedirectToAction("Index");
            }

            ViewBag.FoodCategoryId = new SelectList(this.foodCategoryService.GetAll(), "Id", "Name", foodDonation.FoodCategoryId);
            return View(foodDonation);
        }

        // GET: Donors/FoodDonations/Edit/5
        public ActionResult Edit(int? id)
        {
            ApplicationUser user = this.currentUserProvider.Get();
            Donor donor = this.donorProfileService.GetByApplicationUserId(user.Id);
            FoodDonation foodDonation = this.foodDonationService.GetById((int)id);

            if (donor.Id.ToString() != foodDonation.DonorId)
            {
                return RedirectToAction("Index");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            

            if (foodDonation == null)
            {
                return HttpNotFound();
            }

            IEnumerable<FoodCategory> foodCategories = this.foodCategoryService.GetAll();

            ViewBag.FoodCategoryId = new SelectList(foodCategories, "Id", "Name", foodDonation.FoodCategoryId);

            FoodDonationEditModel model = Mapper.Map<FoodDonation, FoodDonationEditModel>(foodDonation);

            return View(model);
        }

        // POST: Donors/FoodDonations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FoodDonationEditModel model)
        {
            FoodDonation foodDonation = this.foodDonationService.GetById(model.Id);

            Mapper.Map<FoodDonationEditModel, FoodDonation>(model, foodDonation);

            if (ModelState.IsValid)
            {
                this.foodDonationService.Update(foodDonation);
                return RedirectToAction("MyDonations");
            }

            IEnumerable<FoodCategory> foodCategories = this.foodCategoryService.GetAll();
            ViewBag.FoodCategoryId = new SelectList(foodCategories, "Id", "Name", foodDonation.FoodCategoryId);
            return View(foodDonation);
        }

        // GET: Donors/FoodDonations/Delete/5
        public ActionResult Delete(int? id)
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

            var model = Mapper.Map<FoodDonation, FoodDonationViewModel>(foodDonation);

            return View(model);
        }

        // POST: Donors/FoodDonations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FoodDonation foodDonation = this.foodDonationService.GetById(id);
            this.foodDonationService.Delete(foodDonation);
            return RedirectToAction("MyDonations");
        }

        private IEnumerable<SelectListItem> GetFoodCategories()
        {
            var foodCategories = this.foodCategoryService.GetAll()
                             .Select(f => new SelectListItem
                             {
                                 Value = f.Id.ToString(),
                                 Text = f.Name
                             });

            return new SelectList(foodCategories, "Value", "Text");
        }
    }
}
